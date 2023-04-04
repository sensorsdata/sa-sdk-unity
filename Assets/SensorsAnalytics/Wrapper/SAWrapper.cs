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
    public interface IDynamicSuperProperties
    {
        Dictionary<string, object> getProperties();
    }

    public partial class SensorsAnalyticsWrapper
    {
        private string serverUrl = "";
        private bool enableLog = false;
        private int autoTrackType = 0;
        private int networkType = 0;

        public SensorsAnalyticsWrapper(string serverUrl, bool enableLog, int autoTrackType, int networkType)
        {
            this.serverUrl = serverUrl;
            this.enableLog = enableLog;
            this.autoTrackType = autoTrackType;
            this.networkType = networkType;
            SALog.IsLogEnalbe(enableLog);
            SALog.Debug("Unity Config=======init serverUrl:" + serverUrl + ", enableLog:" + enableLog
                + ", networkType:" + networkType + ", autoTrackType:" + autoTrackType);
            _init();
        }


        public void Flush()
        {
            _flush();
        }

        public void Identify(string anonymousId)
        {
            _identify(anonymousId);
        }

        public void ResetAnonymousId()
        {
            _resetAnonymousId();
        }

        public void Login(string loginId)
        {
            _login(loginId);
        }

        public void Logout()
        {
            _logout();
        }

        public void Track(string eventName, Dictionary<string, object> properties = null)
        {
            _track(eventName, properties);
        }

        public void ProfileSet(Dictionary<string, object> properties)
        {
            _profileSet(properties);
        }

        public void ProfileSetOnce(Dictionary<string, object> properties)
        {
            _profileSetOnce(properties);
        }

        public string TrackTimerStart(string eventName)
        {
            return _trackTimerStart(eventName);
        }

        public void TrackTimerEnd(string eventName, Dictionary<string, object> properties)
        {
            _trackTimerEnd(eventName, properties);
        }

        public void TrackTimerPause(string eventName)
        {
            _trackTimerPause(eventName);
        }

        public void TrackTimerResume(string eventName)
        {
            _trackTimerResume(eventName);
        }

        public void ClearTrackTimer()
        {
            _clearTrackTimer();
        }

        public void RegisterSuperProperties(Dictionary<string, object> properties)
        {
            _registerSuperProperties(properties);
        }

        public void UnregisterSuperProperty(string superPropertyName)
        {
            _unregisterSuperProperty(superPropertyName);
        }

        public Dictionary<string, object> GetSuperProperties()
        {
            return _getSuperProperties();
        }

        public void ClearSuperProperties()
        {
            _clearSuperProperties();
        }

        public void HandleSchemeUrl(string url)
        {
            _handleSchemeUrl(url);
        }

        //二期内容
        public void TrackInstallation(Dictionary<string, object> properties = null, bool disableCallback = false)
        {
            _trackInstallation(properties, disableCallback);
        }

        public void RemoveTimer(string eventName)
        {
            _removeTimer(eventName);
        }

        public void SetAndroidMaxCacheSize(long maxCacheSize)
        {
            #if UNITY_ANDROID
            _setAndroidMaxCacheSize(maxCacheSize);
            #endif
        }

        public void SetiOSMaxCacheSize(int maxCount)
        {
            #if UNITY_IOS
            _setiOSMaxCacheSize(maxCount);
            #endif
        }

        public void DeleteAll()
        {
            _deleteAll();
        }

        public void SetFlushBulkSize(int flushBulkSize)
        {
            _setFlushBulkSize(flushBulkSize);
        }

        public void SetFlushInterval(int flushInteval)
        {
            _setFlushInterval(flushInteval);
        }

        public void SetFlushNetworkPolicy(int types)
        {
            _setFlushNetworkPolicy(types);
        }

       /* public void RegisterDynamciSuperProperties(IDynamicSuperProperties superProperties)
        {
            _registerDynamciSuperProperties(superProperties);
        }*/

    }
}
