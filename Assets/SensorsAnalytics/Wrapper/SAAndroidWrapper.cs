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
using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using UnityEngine;

namespace SensorsAnalytics.Wrapper
{
    public partial class SensorsAnalyticsWrapper
    {
#if UNITY_ANDROID

        private static readonly AndroidJavaClass sensorsDataAPIClass = new AndroidJavaClass("com.sensorsdata.analytics.android.sdk.SensorsDataAPI");
        private AndroidJavaObject apiInstance;

        private void _init()
        {
            AndroidJavaObject currentContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject configObj = new AndroidJavaObject("com.sensorsdata.analytics.android.sdk.SAConfigOptions", serverUrl);
            configObj.Call<AndroidJavaObject>("setAutoTrackEventType", autoTrackType);
            if (enableLog)
            {
                configObj.Call<AndroidJavaObject>("enableLog", true);
            }
            configObj.Call<AndroidJavaObject>("setNetworkTypePolicy", networkType);
            //configObj.Call<AndroidJavaObject>("appendColdStart", currentContext);
            sensorsDataAPIClass.CallStatic("startWithConfigOptions", currentContext, configObj);
            apiInstance = sensorsDataAPIClass.CallStatic<AndroidJavaObject>("sharedInstance");
        }

        private void _flush()
        {
            apiInstance.Call("flush");
        }

        private void _identify(string distinctId)
        {
            apiInstance.Call("identify", distinctId);
        }

        private void _resetAnonymousId()
        {
            apiInstance.Call("resetAnonymousId");
        }

        private void _login(string loginId)
        {
            apiInstance.Call("login", loginId);
        }

        private void _logout()
        {
            apiInstance.Call("logout");
        }

        private void _track(string eventName, Dictionary<string, object> dic = null)
        {

            AndroidJavaObject jsonObject = null;
            if (dic != null)
            {
                string jsonStr = SAUtils.Parse2JsonStr(dic);
                jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            }
            apiInstance.Call("track", eventName, jsonObject);
        }

        private void _profileSet(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return;
            }
            string jsonStr = SAUtils.Parse2JsonStr(dic);
            AndroidJavaObject jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            apiInstance.Call("profileSet", jsonObject);
        }

        private void _profileSetOnce(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return;
            }
            string jsonStr = SAUtils.Parse2JsonStr(dic);
            AndroidJavaObject jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            apiInstance.Call("profileSetOnce", jsonObject);
        }

        #region track timer
        private string _trackTimerStart(string eventName)
        {

            return apiInstance.Call<string>("trackTimerStart", eventName);
        }

        private void _trackTimerEnd(string eventName, Dictionary<string, object> properties)
        {
            AndroidJavaObject jsonObject = null;
            if (properties != null)
            {
                string jsonStr = SAUtils.Parse2JsonStr(properties);
                jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            }
            apiInstance.Call("trackTimerEnd", eventName, jsonObject);
        }

        private void _trackTimerPause(string eventName)
        {
            apiInstance.Call("trackTimerPause", eventName);
        }

        private void _trackTimerResume(string eventName)
        {
            apiInstance.Call("trackTimerResume", eventName);
        }

        private void _clearTrackTimer()
        {
            apiInstance.Call("clearTrackTimer");
        }
        #endregion

        #region super property
        private void _registerSuperProperties(Dictionary<string, object> properties)
        {
            AndroidJavaObject jsonObject = null;
            if (properties != null)
            {
                string jsonStr = SAUtils.Parse2JsonStr(properties);
                jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            }
            apiInstance.Call("registerSuperProperties", jsonObject);
        }

        private void _unregisterSuperProperty(string superPropertyName)
        {
            apiInstance.Call("unregisterSuperProperty", superPropertyName);
        }

        private Dictionary<string, object> _getSuperProperties()
        {
            AndroidJavaObject jsonObj = apiInstance.Call<AndroidJavaObject>("getSuperProperties");
            string jsonStr = jsonObj.Call<string>("toString");
            if (jsonStr == null || jsonStr.Length == 0)
            {
                return null;
            }
            return SAUtils.Parse2Dictionary(jsonStr);
        }

        private void _clearSuperProperties()
        {
            apiInstance.Call("clearSuperProperties");
        }
        #endregion

        private void _handleSchemeUrl(string url)
        {
            AndroidJavaObject currentContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intentObj = currentContext.Call<AndroidJavaObject>("getIntent");
            AndroidJavaObject sensorsDataUtilsObj = new AndroidJavaClass("com.sensorsdata.analytics.android.sdk.util.SensorsDataUtils");
            sensorsDataUtilsObj.CallStatic("handleSchemeUrl", currentContext, intentObj);
        }

        //二期内容
        private void _trackInstallation(Dictionary<string, object> properties = null, bool disableCallback = false)
        {
            AndroidJavaObject jsonObject = null;
            if (properties != null)
            {
                string jsonStr = SAUtils.Parse2JsonStr(properties);
                jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
            }
            apiInstance.Call("trackAppInstall", jsonObject, disableCallback);
        }

        private void _removeTimer(string eventName)
        {
            apiInstance.Call("removeTimer", eventName);
        }

        private void _setAndroidMaxCacheSize(long maxCacheSize)
        {
            apiInstance.Call("setMaxCacheSize", maxCacheSize);
        }

        private void _deleteAll()
        {
            apiInstance.Call("deleteAll");
        }

        private void _setFlushBulkSize(int flushBulkSize)
        {
            apiInstance.Call("setFlushBulkSize", flushBulkSize);
        }

        private void _setFlushInterval(int flushInteval)
        {
            apiInstance.Call("setFlushInterval", flushInteval);
        }

        private void _setFlushNetworkPolicy(int types)
        {
            apiInstance.Call("setFlushNetworkPolicy", types);
        }

        /* private class AndroidSuperDynamicCallback : AndroidJavaProxy
         {
             private IDynamicSuperProperties superProperties;
             public AndroidSuperDynamicCallback(IDynamicSuperProperties superProperties) : base("com.sensorsdata.analytics.android.sdk.SensorsDataDynamicSuperProperties")
             {
                 this.superProperties = superProperties;
             }

             AndroidJavaObject getDynamicSuperProperties()
             {
                 if (this.superProperties != null)
                 {
                     Dictionary<string, object> dic = superProperties.getProperties();
                     string jsonStr = SAUtils.Parse2JsonStr(dic);
                     AndroidJavaObject jsonObject = SAUtils.Parse2JavaJSONObject(jsonStr);
                     return jsonObject;
                 }
                 return null;
             }
         }

         private void _registerDynamciSuperProperties(IDynamicSuperProperties superProperties)
         {
             AndroidSuperDynamicCallback callback = new AndroidSuperDynamicCallback(superProperties);
             apiInstance.Call("registerDynamicSuperProperties", callback);
         }*/
#endif
    }
}
