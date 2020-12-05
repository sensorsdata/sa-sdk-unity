/*
 * Created by zhangwei on 2020/8/6.
 * Copyright 2015－2020 Sensors Data Inc.
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
using SensorDataAnalytics.Utils;
using SensorsAnalytics.Wrapper;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SensorsAnalytics
{

    public enum NetworkType
    {
        TYPE_NONE = 0,
        TYPE_2G = 1,
        TYPE_3G = 1 << 1,
        TYPE_4G = 1 << 2,
        TYPE_WIFI = 1 << 3,
        TYPE_5G = 1 << 4,
        TYPE_ALL = 0xff
    }

    public class SensorsDataAPI : MonoBehaviour
    {
        public readonly static string ANDROID_VERSION = "4.4.3";
        public readonly static string IOS_VERSION = "2.1.17";
        public readonly static string UNITY_VERSION = "1.0.1";
        /// <summary>
        /// 当前 Unity SDK 版本
        /// </summary>
        public readonly static string SDK_VERSION = UNITY_VERSION + "_" + ANDROID_VERSION + "_" + IOS_VERSION;
        [Header("SensorsData Unity SDK Config")]
        [HideInInspector]
        public string serverUrl = "请输入数据接收地址...";
        [HideInInspector]
        public bool isEnableLog = false;

        [HideInInspector]
        public int autoTrackType = 1 | 1 << 1;
        [HideInInspector]
        public int networkType = 1 | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 4;


        #region internal use

        private static SensorsDataAPI saInstance;
        private static ReaderWriterLockSlim lockObj = new ReaderWriterLockSlim();
        private static SensorsAnalyticsWrapper analyticsWrapper;
        private static volatile bool isFirstEvent = true;

        void Awake()
        {
            SALog.Debug("sensorsdataapi awake.");
            if (saInstance == null)
            {
                DontDestroyOnLoad(gameObject);
                saInstance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            analyticsWrapper = new SensorsAnalyticsWrapper(serverUrl, isEnableLog, autoTrackType, networkType);
        }
        #endregion


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 上报一条事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="properties">事件属性</param>
        public static void Track(string eventName, Dictionary<string, object> properties = null)
        {
            if (isFirstEvent)
            {
                isFirstEvent = false;
                if(properties == null)
                {
                    properties = new Dictionary<string, object>();
                }
                List<string> list = new List<string>();
                list.Add("unity:" + SDK_VERSION);
                properties.Add("$lib_plugin_version", list);
            }
            analyticsWrapper.Track(eventName, properties);
        }

        /// <summary>
        /// 触发一次数据上报逻辑，会将缓存中的数据发往服务端
        /// </summary>
        public static void Flush()
        {
            analyticsWrapper.Flush();
        }

        /// <summary>
        /// 设置当前用户的 loginId
        /// </summary>
        /// <param name="loginId">登录 ID</param>
        public static void Login(string loginId)
        {
            analyticsWrapper.Login(loginId);
        }

        /// <summary>
        /// 清空当前用户的 login
        /// </summary>
        public static void Logout()
        {
            analyticsWrapper.Logout();
        }

        /// <summary>
        /// 设置用户的一个或多个 profile，profile 如果存在，则覆盖，否则就新创建
        /// </summary>
        /// <param name="properties">属性列表</param>
        public static void ProfileSet(Dictionary<string, object> properties)
        {
            analyticsWrapper.ProfileSet(properties);
        }

        /// <summary>
        /// 首次设置用户的一个或多个 Profile，与 ProfileSet 不同的是，如果之前存在，则忽略，否则新创建
        /// </summary>
        /// <param name="properties">属性列表</param>
        public static void ProfileSetOnce(Dictionary<string, object> properties)
        {
            analyticsWrapper.ProfileSetOnce(properties);
        }

        /// <summary>
        /// 开始事件计时器，计时单位为秒
        /// </summary>
        /// <param name="eventName">事件的名称</param>
        /// <returns>如果需要 track 同一个事件名多次，对于后面的暂停、恢复、停止等操作使用此方法的返回值。</returns>
        public static string TrackTimerStart(string eventName)
        {
            return analyticsWrapper.TrackTimerStart(eventName);
        }

        /// <summary>
        /// 停止事件计时器，并记录此事件信息
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="properties">事件属性</param>
        public static void TrackTimerEnd(string eventName, Dictionary<string, object> properties = null)
        {
            analyticsWrapper.TrackTimerEnd(eventName, properties);
        }

        /// <summary>
        /// 暂停事件计时器
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public static void TrackTimerPause(string eventName)
        {
            analyticsWrapper.TrackTimerPause(eventName);
        }

        /// <summary>
        /// 恢复事件计时器
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public static void TrackTimerResume(string eventName)
        {
            analyticsWrapper.TrackTimerResume(eventName);
        }

        /// <summary>
        /// 清楚所有的计时器
        /// </summary>
        public static void ClearTrackTimer()
        {
            analyticsWrapper.ClearTrackTimer();
        }

        /// <summary>
        /// 注册所有事件都有的公共属性
        /// </summary>
        /// <param name="properties">事件公共属性</param>
        public static void RegisterSuperProperties(Dictionary<string, object> properties)
        {
            analyticsWrapper.RegisterSuperProperties(properties);
        }

        /// <summary>
        /// 删除指定属性名的信息
        /// </summary>
        /// <param name="superPropertyName">属性名</param>
        public static void UnregisterSuperProperty(string superPropertyName)
        {
            analyticsWrapper.UnregisterSuperProperty(superPropertyName);
        }

        /// <summary>
        /// 获取注册的公共属性信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetSuperProperties()
        {
            return analyticsWrapper.GetSuperProperties();
        }

        /// <summary>
        /// 清除所有的公共属性
        /// </summary>
        public static void ClearSuperProperties()
        {
            analyticsWrapper.ClearSuperProperties();
        }

        /// <summary>
        /// 设置匿名 ID
        /// </summary>
        /// <param name="distinctId">匿名 ID</param>
        public static void Identify(string distinctId)
        {
            analyticsWrapper.Identify(distinctId);
        }

        /// <summary>
        /// 用于 App 首次启动时追踪渠道来源并设置渠道事件的属性
        /// </summary>
        /// <param name="properties">事件的属性</param>
        /// <param name="disableCallback">是否关闭此次渠道匹配的回调请求</param>
        public static void TrackAppInstall(Dictionary<string, object> properties = null, bool disableCallback = false)
        {
            analyticsWrapper.TrackInstallation(properties, disableCallback);
        }

        /// <summary>
        /// 删除事件计时器
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public static void RemoveTimer(string eventName)
        {
            analyticsWrapper.RemoveTimer(eventName);
        }

        /// <summary>
        /// 设置本地缓存上限值，单位是 byte，默认是 32MB：32 * 1024 * 1024，最小 16MB：16 * 1024 * 1024，若小于 16MB，则按 16MB 处理
        /// </summary>
        /// <param name="maxCacheSize">单位 byte</param>
        public static void SetAndroidMaxCacheSize(long maxCacheSize)
        {
            #if UNITY_ANDROID
            analyticsWrapper.SetAndroidMaxCacheSize(maxCacheSize);
            #endif
        }

        /// <summary>
        /// 设置本地缓存上限值，默认和最小值都是 10000 条
        /// </summary>
        /// <param name="maxCount">最大缓存条数</param>
        public static void SetiOSMaxCacheSize(int maxCount)
        {
            #if UNITY_IOS
            analyticsWrapper.SetiOSMaxCacheSize(maxCount);
            #endif
        }

        /// <summary>
        /// 删除缓存的所有事件
        /// </summary>
        public static void DeleteAll()
        {
            analyticsWrapper.DeleteAll();
        }

        /// <summary>
        /// 设置本地缓存日志的最大条目数，最小 50 条
        /// </summary>
        /// <param name="flushBulkSize">缓存数目</param>
        public static void SetFlushBulkSize(int flushBulkSize)
        {
            analyticsWrapper.SetFlushBulkSize(flushBulkSize);
        }

        /// <summary>
        /// 设置两次数据发送的最小时间间隔，最小 5 秒钟
        /// </summary>
        /// <param name="flushInteval">单位毫秒</param>
        public static void SetFlushInterval(int flushInteval)
        {
            analyticsWrapper.SetFlushInterval(flushInteval);
        }

        /// <summary>
        /// 设置 flush 时网络发送策略
        /// </summary>
        /// <param name="types">类似：NetworkType.Type_2G + NetworkType.Type_3G</param>
        public static void SetFlushNetworkPolicy(int types)
        {
            analyticsWrapper.SetFlushNetworkPolicy(types);
        }


       /* /// <summary>
        /// 注册动态公共属性
        /// </summary>
        /// <param name="superProperties">动态公共属性需要实现的接口</param>
        public static void RegisterDynamciSuperProperties(IDynamicSuperProperties superProperties)
        {
            analyticsWrapper.RegisterDynamciSuperProperties(superProperties);
        }*/
    }

}

