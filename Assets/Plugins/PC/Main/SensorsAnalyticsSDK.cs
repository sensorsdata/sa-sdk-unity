/*
 * Created by 储强盛 on 2023/03/06.
 * Copyright 2015－2023 Sensors Data Co., Ltd. All rights reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Builder;using SensorsAnalyticsPCSDK.Utils;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Property;
using SensorsAnalyticsPCSDK.Tracker;
using SensorsAnalyticsPCSDK.Database;
using SensorsAnalyticsPCSDK.Network;
using UnityEngine;
using System.Collections;namespace SensorsAnalyticsPCSDK.Main
{
    public class SensorsAnalyticsSDK
    {

        public static SensorsAnalyticsSDK InstanceSDK;

        private readonly SAIdentifier _identifier = new SAIdentifier();
        private readonly SAPresetPropertyObject _presetPropertyObject = new SAPresetPropertyObject();
        private readonly SASuperProperty _superProperty = new SASuperProperty();
        private readonly SATrackTimer _trackTimer = new SATrackTimer();
        private readonly SAEventStore _eventStore;
        private readonly SAFlush _flushStore;
        private readonly SAConfigOptions _configOptions;
        private readonly MonoBehaviour _SDKMono;
        private readonly SATrackInstallation _trackInstallation = new SATrackInstallation();
        private readonly SAAutoTracker _autoTracker;
        // 定时上传任务协程
        private Coroutine _timedFlushCoroutine;

        public SensorsAnalyticsSDK(SAConfigOptions configOptions, MonoBehaviour mono = null)        {            try            {                if (configOptions != null)                {                    SALogger.EnableLog = configOptions.EnableLog;                }                _configOptions = configOptions;                _SDKMono = mono;                InstanceSDK = this;                _eventStore = new SAEventStore(configOptions);                _flushStore = new SAFlush(configOptions.ServerURL, _eventStore, mono);                // 动态加载 SAAutoTracker                GameObject autoTrackGameObject = new GameObject("SAAutoTracker", typeof(SAAutoTracker));                _autoTracker = (SAAutoTracker)autoTrackGameObject.GetComponent(typeof(SAAutoTracker));                UnityEngine.Object.DontDestroyOnLoad(autoTrackGameObject);                _autoTracker.EnableAutoTrack(configOptions.AutoTrackType);                // 启动定时上传               _timedFlushCoroutine = _SDKMono.StartCoroutine(TimedFlush());            }            catch (Exception exception)
            {
                SALogger.Error("SensorsAnalyticsSDK Init Error: " + exception.Message);
            }        }        #region 事件埋点
        /// <summary>        /// track 自定义事件        /// </summary>        /// <param name="eventName">事件名</param>        /// <param name="properties">事件属性</param>
        public void Track(string eventName, Dictionary<string, object> properties)        {            if(string.IsNullOrEmpty(eventName))            {                SALogger.Error("eventName cannot be empty");                return;            }            SACustomEventObject trackObject = new SACustomEventObject(eventName);            TrackEventObject(trackObject, properties);        }

        /// <summary>        /// profile_set 用户信息设置        /// </summary>        /// <param name="properties">设置内容</param>
        public void ProfileSet(Dictionary<string, object> properties)        {            SAProfileEventObject profileObject = new SAProfileEventObject(SAConstant.kSAProfileSet);            TrackEventObject(profileObject, properties);        }

        /// <summary>        /// profile_set_once 用户信息设置        /// </summary>        /// <param name="properties">设置内容</param>
        public void ProfileSetOnce(Dictionary<string, object> properties)        {            SAProfileEventObject profileObject = new SAProfileEventObject(SAConstant.kSAProfileSetOnce);            TrackEventObject(profileObject, properties);        }

        public void Login(string loginId)        {            if (string.IsNullOrEmpty(loginId))            {                SALogger.Error("loginId cannot be empty");                return;            }            SASignUpEventObject signObjct = new SASignUpEventObject(SAConstant.kSAEventNameSignUp);            // 完成登录逻辑            if(_identifier.Login(loginId))            {                TrackEventObject(signObjct, null);            }            else            {                SALogger.Warn("The loginId: " + loginId + " is invalid");            }        }

        public void Logout()        {            _identifier.Logout();        }

        public void Identify(string anonymousId)        {            _identifier.Identify(anonymousId);        }

        public void TrackAppInstall(Dictionary<string, object> properties, bool disableCallback = false)        {            // 先判断是否触发过激活            if(_trackInstallation.HasTrackInstallation(disableCallback))            {                SALogger.LogInfo("The App has tracked Installatio events");                return;            }            // 拼接 UA            string userAgent = SADeviceInfo.UserAgent();            if(!string.IsNullOrEmpty(userAgent))            {                properties[SAConstant.kSAEventPropertyUserAgent] = userAgent;            }            // 采集激活事件            SAPresetEventObject presetObject = new SAPresetEventObject(SAConstant.kSAEventNameAppInstall);            TrackEventObject(presetObject, properties);            // 设置用户属性            SAProfileEventObject profileObject = new SAProfileEventObject(SAConstant.kSAProfileSetOnce);            Dictionary<string, object> profileProperties = _trackInstallation.ProfileProperties();            profileObject.AddProfileProperties(profileProperties);            TrackEventObject(profileObject, properties);            _trackInstallation.TrackInstallation(disableCallback);            // 调用 flush            Flush();        }

        internal void TrackEventObject(SABaseEventObject eventObject, Dictionary<string, object> properties)        {            try            {                // 1. 事件名校验                eventObject.ValidateEventName();                // 2. 设置用户关联信息                eventObject.DistinctId = _identifier.DistinctId();                string anonymousId = _identifier.AnonymousId();                eventObject.AnonymousId = anonymousId;                eventObject.OriginalId = anonymousId;                string loginId = _identifier.LoginId();                if(!string.IsNullOrEmpty(loginId))                {                    eventObject.LoginId = loginId;                }                // 3. 添加属性：预置属性、App 版本、设备 Id -> 公共属性 -> 事件时长                eventObject.AddEventProperties(_presetPropertyObject.Properties());                // 公共属性                eventObject.AddSuperProperties(_superProperty.CurrentSuperProperties());                // 事件时长                float eventDuration = _trackTimer.EventDurationFromEventId(eventObject.EventId, eventObject.CurrentSystemUpTime);                eventObject.AddDurationProperty(eventDuration);                // 4. 自定义属性需要校验                eventObject.AddCustomProperties(properties);                            // 设置 DeviceId、是否首日，防止被覆盖                Dictionary<string, object> importantProperties = _presetPropertyObject.UncoverableProperties(eventObject.Type);                eventObject.AddEventProperties(importantProperties);                // 事件入库               int recordCount = _eventStore.InsertEventInfo(eventObject.JsonObject());                // 事件日志                SALogger.LogInfo("【track event】:\n", eventObject.JsonObject());                // 到达条数，上传数据                if(recordCount >= _configOptions.FlushBulkSize)                {                    Flush();                }            }            catch (Exception ex)
            {
                SALogger.Error("SensorsAnalyticsSDK TrackEventObject Error: ", ex.Message);
            }        }        public void Flush()        {            // 目前 PC 上只支持 NONE 和 ALL，只要网络策略不是 NONE，都会尝试上报            SANetworkType networkType = SADeviceInfo.NetworkType();            if((_configOptions.FlushNetworkPolicy & networkType) == 0)            {                SALogger.Warn($"The current FlushNetworkPolicy is {_configOptions.FlushNetworkPolicy.ToString()}. The network cannot be flush");                return;            }            _flushStore.FlushEventRecords();        }        internal void FlushImmediately()        {            SANetworkType networkType = SADeviceInfo.NetworkType();            if ((_configOptions.FlushNetworkPolicy & networkType) == 0)            {                SALogger.Warn($"The current FlushNetworkPolicy is {_configOptions.FlushNetworkPolicy.ToString()}. The network cannot be flush");                return;            }            _flushStore.FlushImmediately();        }        public void DeleteAll()        {            _eventStore.DeleteAllRecords();        }        #endregion        #region 事件计时        public string TrackTimerStart(string eventName)        {            if(!SAValidator.ValidateKey(eventName))            {                return null;            }            int currentSysUpTime = Environment.TickCount;            string eventId = _trackTimer.GenerateEventIdByEventName(eventName);            _trackTimer.TrackTimerStart(eventId, currentSysUpTime);            return null;        }        public void TrackTimerEnd(string eventName, Dictionary<string, object> properties = null)        {            if (string.IsNullOrEmpty(eventName))            {                SALogger.Error("eventName cannot be empty");                return;            }            SACustomEventObject trackObject = new SACustomEventObject(eventName);            TrackEventObject(trackObject, properties);        }        public void TrackTimerPause(string eventName)        {            if (string.IsNullOrEmpty(eventName))            {                SALogger.Error("eventName cannot be empty");                return;            }            int currentSysUpTime = Environment.TickCount;            _trackTimer.TrackTimerPause(eventName, currentSysUpTime);        }        public void TrackTimerResume(string eventName)        {            if (string.IsNullOrEmpty(eventName))            {                SALogger.Error("eventName cannot be empty");                return;            }            int currentSysUpTime = Environment.TickCount;            _trackTimer.TrackTimerResume(eventName, currentSysUpTime);        }        public void RemoveTimer(string eventName)        {            if (string.IsNullOrEmpty(eventName))            {                SALogger.Error("eventName cannot be empty");                return;            }            _trackTimer.TrackTimerRemove(eventName);        }        public void ClearTrackTimer()        {            _trackTimer.ClearAllEventTimers();        }        // 前后台切换，内部调用        internal void PauseAllEventTimers()        {            int currentSysUpTime = Environment.TickCount;            _trackTimer.PauseAllEventTimers(currentSysUpTime);        }        internal void ResumeAllEventTimers()        {            int currentSysUpTime = Environment.TickCount;            _trackTimer.ResumeAllEventTimers(currentSysUpTime);        }        #endregion        #region 公共属性相关        public void RegisterSuperProperties(Dictionary<string, object>superProperties)        {            _superProperty.RegisterSuperProperties(superProperties);        }        public void UnregisterSuperProperty(string propertyName)        {            _superProperty.UnregisterSuperProperty(propertyName);        }        public Dictionary<string, object>GetSuperProperties()        {                        return _superProperty.CurrentSuperProperties();        }        public void ClearSuperProperties()        {            _superProperty.ClearSuperProperties();        }        #endregion        #region SDK 配置
        public void SetMaxCacheSize(int maxCount)        {            _configOptions.MaxCacheSize = maxCount;        }        public void SetFlushBulkSize(int flushBulkSize)        {            _configOptions.FlushBulkSize = flushBulkSize;        }        public void SetFlushInterval(int flushInteval)        {            Flush();            // 暂停之前的协程            _SDKMono.StopCoroutine(_timedFlushCoroutine);            _configOptions.FlushInterval = flushInteval / 1000;            // 重新创建协程，更新定时任务            _timedFlushCoroutine = _SDKMono.StartCoroutine(TimedFlush());        }                // 定时上传        private IEnumerator TimedFlush()        {
            while (true)
            {
                yield return new WaitForSeconds(_configOptions.FlushInterval);
                Flush();
            }
        }        public void SetFlushNetworkPolicy(int type)        {            _configOptions.FlushNetworkPolicy = (SANetworkType)type;        }        #endregion
     }
}

