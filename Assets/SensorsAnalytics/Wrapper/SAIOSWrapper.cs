/*
 * Created by zhangwei on 2020/8/12.
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
 */#if (UNITY_IOS && !UNITY_EDITOR)

using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using UnityEngine;
using System.Runtime.InteropServices;

namespace SensorsAnalytics.Wrapper
{
    public partial class SensorsAnalyticsWrapper
    {

// iOS 平台下，用于调用 Objective-C 中定义的本地库声明
        [DllImport("__Internal")]
        private static extern void start(string server_url, bool enableLog, int eventType, int networkType);
        [DllImport("__Internal")]
        private static extern void track(string event_name, string propertiesJson);

        [DllImport("__Internal")]
        private static extern void login(string loginId);
        [DllImport("__Internal")]
        private static extern void logout();
        [DllImport("__Internal")]
        private static extern void identify(string anonymousId);
        [DllImport("__Internal")]
        private static extern string distinctId();
        [DllImport("__Internal")]
        private static extern string loginId();

        [DllImport("__Internal")]
        private static extern string track_timer_start(string eventName);
        [DllImport("__Internal")]
        private static extern void track_timer_pause(string eventName);
        [DllImport("__Internal")]
        private static extern void track_timer_resume(string eventName);
        [DllImport("__Internal")]
        private static extern void track_timer_end(string eventName, string propertiesJson);
        [DllImport("__Internal")]
        private static extern void clear_track_timer();
        [DllImport("__Internal")]
        private static extern void remove_timer(string eventname);

        [DllImport("__Internal")]
        private static extern void register_super_properties(string propertiesJson);
        [DllImport("__Internal")]
        private static extern void unregister_super_property(string superPropertyName);
        [DllImport("__Internal")]
        private static extern void clear_super_properties();
        [DllImport("__Internal")]
        private static extern string get_super_properties();

        [DllImport("__Internal")]
        private static extern void flush();

        [DllImport("__Internal")]
        private static extern void profile_set(string propertiesJson);
        [DllImport("__Internal")]
        private static extern void profile_set_once(string propertiesJson);

        [DllImport("__Internal")]
        private static extern void handle_scheme_url(string url);
        [DllImport("__Internal")]
        private static extern void delete_all();
        [DllImport("__Internal")]
        private static extern void set_flush_network_policy(int types);
        [DllImport("__Internal")]
        private static extern void set_ios_max_cache_size(int count);
        [DllImport("__Internal")]
        private static extern void set_flush_bulk_size(int flushBulkSize);
        [DllImport("__Internal")]
        private static extern void set_flush_interval(int flushInteval);
        [DllImport("__Internal")]
        private static extern void track_app_install(string properties, bool disableCallback);

        private void _init()
        {
            start(serverUrl, enableLog, autoTrackType, networkType);
        }

        private void _trackInstallation(Dictionary<string, object> properties, bool disableCallback)
        {
            track_app_install(SAUtils.Parse2JsonStr(properties), disableCallback);
        }

        private void _removeTimer(string eventName)
        {
            remove_timer(eventName);
        }

        private void _setiOSMaxCacheSize(int count)
        {
            set_ios_max_cache_size(count);
        }

        private void _deleteAll()
        {
            delete_all();
        }

        private void _setFlushBulkSize(int flushBulkSize)
        {
            set_flush_bulk_size(flushBulkSize);
        }

        private void _setFlushInterval(int flushInteval)
        {
            set_flush_interval(flushInteval);
        }

        private void _setFlushNetworkPolicy(int types)
        {
            set_flush_network_policy(types);
        }

        private void _track(string eventName, Dictionary<string, object> dic = null)
        {
            track(eventName, SAUtils.Parse2JsonStr(dic));
        }

        private void _flush()
        {
            flush();
        }

        private void _identify(string anonymousId)
        {
            identify(anonymousId);
        }

        private void _login(string loginId)
        {
            login(loginId);
        }

        private void _logout()
        {
            logout();
        }

        private string _distinctId()
        {
            return distinctId();
        }

        private string _loginId()
        {
            return loginId();
        }

        private void _profileSet(Dictionary<string, object> dic)
        {
            profile_set(SAUtils.Parse2JsonStr(dic));
        }

        private void _handleSchemeUrl(string url)
        {
            handle_scheme_url(url);
        }

        private void _profileSetOnce(Dictionary<string, object> dic)
        {
            profile_set_once(SAUtils.Parse2JsonStr(dic));
        }

        #region track timer
        private string _trackTimerStart(string eventName)
        {
            return track_timer_start(eventName);
        }

        private void _trackTimerEnd(string eventName, Dictionary<string, object> properties)
        {
            track_timer_end(eventName, SAUtils.Parse2JsonStr(properties));
        }

        private void _trackTimerPause(string eventName)
        {
            track_timer_pause(eventName);
        }

        private void _trackTimerResume(string eventName)
        {
            track_timer_resume(eventName);
        }

        private void _clearTrackTimer()
        {
            clear_track_timer();
        }
        #endregion

        #region super property
        private void _registerSuperProperties(Dictionary<string, object> properties)
        {
            register_super_properties(SAUtils.Parse2JsonStr(properties));
        }

        private void _unregisterSuperProperty(string superPropertyName)
        {
            unregister_super_property(superPropertyName);
        }

        private Dictionary<string, object> _getSuperProperties()
        {
            string propertiesJson = get_super_properties();
            if (propertiesJson == null || propertiesJson.Length == 0)
            {
                return null;
            }
            return SAUtils.Parse2Dictionary(propertiesJson);
        }

        private void _clearSuperProperties()
        {
            clear_super_properties();
        }
        #endregion

    }
}

#endif
