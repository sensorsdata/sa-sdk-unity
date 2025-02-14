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

/*
#if !UNITY_EDITOR

using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using UnityEngine;

namespace SensorsAnalytics.Wrapper
{
    public partial class SensorsAnalyticsWrapper
    {

        private void _init()
        {
            SALog.Debug("Editor Log: calling init.");
        }

        private void _flush()
        {
            SALog.Debug("Editor Log: calling flush.");
        }

        private void _identify(string anonymousId)
        {
            SALog.Debug("Editor Log: calling identity: anonymousId=" + anonymousId + ".");
        }

        private void _login(string loginId)
        {
            SALog.Debug("Editor Log: calling login: loginId=" + loginId + ".");
        }

        private void _logout()
        {
            SALog.Debug("Editor Log: calling logout.");
        }

        private string _distinctId()        {            SALog.Debug("Editor Log: calling distinctId()");            return null;        }        private string _loginId()        {            SALog.Debug("Editor Log: calling loginId()");            return null        }

        private void _track(string eventName, Dictionary<string, object> properties = null)
        {
            SALog.Debug("Editor Log: calling track: eventName=" + eventName + ", properties=" + SAUtils.ToDebugString(properties));
        }

        private void _profileSet(Dictionary<string, object> properties)
        {
            SALog.Debug("Editor Log: calling profileSet: properties=" + SAUtils.ToDebugString(properties));
        }

        private void _profileSetOnce(Dictionary<string, object> properties)
        {
            SALog.Debug("Editor Log: calling profileSetOnce: properties=" + SAUtils.ToDebugString(properties));
        }

        private string _trackTimerStart(string eventName)
        {
            SALog.Debug("Editor Log: calling trackTimerStart: eventName=" + eventName);
            return "";
        }

        private void _trackTimerEnd(string eventName, Dictionary<string, object> properties)
        {
            SALog.Debug("Editor Log: calling trackTimerEnd: eventName=" + eventName + ", properties=" + SAUtils.ToDebugString(properties));
        }

        private void _trackTimerPause(string eventName)
        {
            SALog.Debug("Editor Log: calling trackTimerPause: eventName=" + eventName);
        }

        private void _trackTimerResume(string eventName)
        {
            SALog.Debug("Editor Log: calling trackTimerResume: eventName=" + eventName);
        }

        private void _clearTrackTimer()
        {
            SALog.Debug("Editor Log: calling clearTrackTimer.");
        }

        private void _registerSuperProperties(Dictionary<string, object> properties)
        {
            SALog.Debug("Editor Log: calling registerSuperProperties: properties=" + SAUtils.ToDebugString(properties));
        }

        private void _unregisterSuperProperty(string superPropertyName)
        {
            SALog.Debug("Editor Log: calling unregisterSuperProperty: superPropertyName=" + superPropertyName);
        }

        private Dictionary<string, object> _getSuperProperties()
        {
            SALog.Debug("Editor Log: calling getSuperProperties.");
            return new Dictionary<string, object>();
        }

        private void _clearSuperProperties()
        {
            SALog.Debug("Editor Log: calling clearSuperProperties.");
        }

        private void _handleSchemeUrl(string url)
        {
            SALog.Debug("editor handle scheme: " + url);
        }

        //二期内容
        private void _trackInstallation(Dictionary<string, object> properties = null, bool disableCallback = false)
        {
            SALog.Debug($"Editor Log: calling TrackInstallation: properties={SAUtils.ToDebugString(properties)}, disableCallback = {disableCallback}");
        }

        private void _removeTimer(string eventName)
        {
            SALog.Debug("Editor Log: calling RemoveTimer: eventName=" + eventName);
        }

        private void _setAndroidMaxCacheSize(long maxCacheSize)
        {
            SALog.Debug("Editor Log: calling SetAndroidMaxCacheSize: maxCacheSize=" + maxCacheSize);
        }

        private void _setiOSMaxCacheSize(long maxCount)
        {
            SALog.Debug("Editor Log: calling SetiOSMaxCacheSize: maxCacheSize=" + maxCount);
        }

        private void _deleteAll()
        {
            SALog.Debug("Editor Log: calling DeleteAll");
        }

        private void _setFlushBulkSize(int flushBulkSize)
        {
            SALog.Debug("Editor Log: calling SetFlushBulkSize: flushBulkSize=" + flushBulkSize);
        }

        private void _setFlushInterval(int flushInteval)
        {
            SALog.Debug("Editor Log: calling SetFlushInterval: flushInteval=" + flushInteval);
        }

        private void _setFlushNetworkPolicy(int types)
        {
            SALog.Debug("Editor Log: calling SetFlushNetworkPolicy: types=" + types);
        }

        //public void _registerDynamciSuperProperties(IDynamicSuperProperties superProperties)
        //{
        //    SALog.Debug("Editor Log: calling RegisterDynamciSuperProperties: superProperties=" + superProperties);
        //}

    }
}

#endif

*/
