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

#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID)

using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using UnityEngine;
using SensorsAnalyticsPCSDK.Main;
using SensorsAnalyticsPCSDK.Constant;

namespace SensorsAnalytics.Wrapper
{

    public partial class SensorsAnalyticsWrapper
    {
        private SensorsAnalyticsSDK SDKIntance;

        private void _init(MonoBehaviour mono = null)
        {
            SAConfigOptions configOptions = new SAConfigOptions(serverUrl);

            // 初始化配置，网络策略不支持 PC
            configOptions.FlushNetworkPolicy = SANetworkType.ALL;
            configOptions.EnableLog = this.enableLog;
            configOptions.AutoTrackType = (SAAutoTrackType)this.autoTrackType;

            SDKIntance = new SensorsAnalyticsSDK(configOptions, mono);
            Debug.Log("【Init SensorsAnalyticsPCSDK 】");
        }

        private void _flush()
        {
            SDKIntance.Flush();
        }

        private void _identify(string distinctId)
        {
            SDKIntance.Identify(distinctId);
        }

        private void _login(string loginId)
        {
            SDKIntance.Login(loginId);
        }

        private void _logout()
        {
            SDKIntance.Logout();
        }

        private string _distinctId()
        {
            return SDKIntance.DistinctId();
        }

        private string _loginId()
        {
            return SDKIntance.LoginId();
        }

        private string _anonymousId()
        {
            return SDKIntance.AnonymousId();
        }

        private void _track(string eventName, Dictionary<string, object> dic = null)
        {
            SDKIntance.Track(eventName, dic);
        }

        private void _profileSet(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return;
            }
            SDKIntance.ProfileSet(dic);
        }

        private void _profileSetOnce(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return;
            }
            SDKIntance.ProfileSetOnce(dic);
        }

        #region track timer
        private string _trackTimerStart(string eventName)
        {
            return SDKIntance.TrackTimerStart(eventName);
        }

        private void _trackTimerEnd(string eventName, Dictionary<string, object> properties = null)
        {
            SDKIntance.TrackTimerEnd(eventName, properties);
        }

        private void _trackTimerPause(string eventName)
        {
            SDKIntance.TrackTimerPause(eventName);
        }

        private void _trackTimerResume(string eventName)
        {
            SDKIntance.TrackTimerResume(eventName);
        }


        private void _removeTimer(string eventName)
        {
            SDKIntance.RemoveTimer(eventName);
        }

        private void _clearTrackTimer()
        {
            SDKIntance.ClearTrackTimer();
        }
        #endregion

        #region super property
        private void _registerSuperProperties(Dictionary<string, object> properties)
        {
            SDKIntance.RegisterSuperProperties(properties);
        }

        private void _unregisterSuperProperty(string superPropertyName)
        {
            SDKIntance.UnregisterSuperProperty(superPropertyName);
        }

        private Dictionary<string, object> _getSuperProperties()
        {
            return SDKIntance.GetSuperProperties();
        }

        private void _clearSuperProperties()
        {
            SDKIntance.ClearSuperProperties();
        }
        #endregion

        private void _handleSchemeUrl(string url)
        {
            SALog.Warn("Unity SDK for PC does not support the _handleSchemeUrl interface");
        }

        private void _trackInstallation(Dictionary<string, object> properties = null, bool disableCallback = false)
        {
            SDKIntance.TrackAppInstall(properties, disableCallback);
        }

        private void _setAndroidMaxCacheSize(long maxCacheSize)
        {
            SALog.Warn("Unity SDK for PC does not support the _setAndroidMaxCacheSize interface");
        }

        private void _deleteAll()
        {
            SDKIntance.DeleteAll();
        }

        private void _setPCMaxCacheSize(int maxCount)
        {
            SDKIntance.SetMaxCacheSize(maxCount);
        }

        private void _setFlushBulkSize(int flushBulkSize)
        {
            SDKIntance.SetFlushBulkSize(flushBulkSize);
        }

        private void _setFlushInterval(int flushInteval)
        {
            SDKIntance.SetFlushInterval(flushInteval);
        }

        private void _setFlushNetworkPolicy(int types)
        {
            SDKIntance.SetFlushNetworkPolicy(types);
        }

    }
}

#endif