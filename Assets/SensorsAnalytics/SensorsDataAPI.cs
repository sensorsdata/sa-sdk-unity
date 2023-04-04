/*
 * Created by zhangwei on 2020/8/6.
 * Copyright 2015－2021 Sensors Data Inc.
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
        NONE = 0,
        TYPE_2G = 1,
        TYPE_3G = 1 << 1,
        TYPE_4G = 1 << 2,
        TYPE_WIFI = 1 << 3,
        TYPE_5G = 1 << 4,
        TYPE_ALL = 0xff
    }    public enum AutoTrackType
    {
        None = 0,
        AppStart = 1,
        AppEnd = 1 << 1
    }

    public class SensorsDataAPI : MonoBehaviour
    {
        /// <summary>
        /// 当前 Unity SDK 版本
        /// </summary>
        public readonly static string SDK_VERSION = "1.0.4";
        [Header("SensorsData Unity SDK Config")]
        [HideInInspector]
        public string serverUrl = "请输入数据接收地址...";
        [HideInInspector]
        public bool isEnableLog = false;

        [HideInInspector]
        public AutoTrackType autoTrackType = AutoTrackType.None;
        [HideInInspector]
        public NetworkType networkType = NetworkType.TYPE_3G | NetworkType.TYPE_4G | NetworkType.TYPE_5G | NetworkType.TYPE_WIFI;


        #region internal use

        private static SensorsDataAPI saInstance;
        //private static ReaderWriterLockSlim lockObj = new ReaderWriterLockSlim();
        private static SensorsAnalyticsWrapper analyticsWrapper;
        private static volatile bool isFirstEvent = true;

        // 脚本实例化时调用
        void Awake()
        {
            SALog.Debug("sensorsdataapi awake.");
            if (saInstance == null)
            {
                // 让游戏对象在场景切换时不被销毁，即使切换到了新的场景仍然存在
                DontDestroyOnLoad(gameObject);
                saInstance = this;
            }
            else
            {
                // 销毁一个游戏对象，即在运行时将其从场景中删除
                Destroy(gameObject);
                return;
            }
            analyticsWrapper = new SensorsAnalyticsWrapper(serverUrl, isEnableLog, (int)autoTrackType, (int)networkType);
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
        /// 触发一条事件
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
        /// 触发一次数据上报，会将缓存中的数据发往服务端
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
        }        /// <summary>
        /// 结束事件计时器，并记录此事件信息
        /// </summary>
        /// <param name="eventName">事件名称或事件的 eventId</param>
        /// <param name="properties">自定义属性</param>
        /// <remarks>
        /// 多次调用 trackTimerEnd 时，以首次调用为准
        /// </remarks>
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
        /// 清除所有的计时器
        /// </summary>
        public static void ClearTrackTimer()
        {
            analyticsWrapper.ClearTrackTimer();
        }

        /// <summary>
        /// 注册每个事件都带有的一些公共属性
        /// </summary>
        /// <param name="properties">事件公共属性</param>
        public static void RegisterSuperProperties(Dictionary<string, object> properties)
        {
            analyticsWrapper.RegisterSuperProperties(properties);
        }

        /// <summary>
        /// 从公共属性中删除某个 property
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
        /// 修改匿名 ID
        /// </summary>
        /// <param name="anonymousId">匿名 ID</param>
        public static void Identify(string anonymousId)
        {
            analyticsWrapper.Identify(anonymousId);
        }

        /// <summary>
        /// 处理 url scheme 跳转打开 App
        /// Android 或 iOS SDK 会根据 url 来判断是否需要处理
        /// </summary>
        /// <param name="url">打开本 app 的回调的 url</param>
        public static void HandleSchemeUrl(string url)
        {
            analyticsWrapper.HandleSchemeUrl(url);
        }        /// <summary>        /// 用于 App 首次启动时追踪渠道来源并设置渠道事件的属性        /// </summary>        /// <param name="properties">事件的属性</param>        /// <param name="disableCallback">是否关闭此次渠道匹配的回调请求</param>
        /// <remarks>
        /// 这个接口是一个较为复杂的功能，请在使用前先阅读相关说明: https://sensorsdata.cn/manual/track_installation.html，并在必要时联系我们的技术顾问同学。        /// </remarks>
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
        /// 设置本地缓存最大事件条数，默认和最小值都是 10000 条
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
        }        /// <summary>
        /// 设置本地缓存日志的最大条目数，最小 50 条
        /// </summary>
        /// <param name="flushBulkSize">缓存数目</param>
        /// <remarks>
        /// 默认值为 100，当累积日志量达到阈值时发送数据
        /// </remarks>
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
