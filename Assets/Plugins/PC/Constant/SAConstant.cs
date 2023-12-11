//// Created by 储强盛 on 2023/3/9.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;

namespace SensorsAnalyticsPCSDK.Constant{    public enum SANetworkType
    {
        NONE = 0,
        ALL = 0xff
    }    public enum SAAutoTrackType
    {
        None = 0,
        AppStart = 1 << 0,
        AppEnd = 1 << 1
    }    /// <summary>    /// PC SDK 全局常量定义    /// </summary>    public class SAConstant
    {        // Unity SDK 版本号，注意和外部插件版本号相同，防止理解歧义        public static readonly string kSALibVersion = "2.0.1";

        public static readonly string kSAEventPresetPropertyLibName = "Unity";

        // 计时后的事件名后缀
        public static readonly string kSAEventIdSuffix = "_SATimer";

        #region 外层属性常量
        // 事件触发时间
        public static readonly string kSAEventTime = "time";

        public static readonly string kSAEventType = "type";

        public static readonly string kSAEventTrackId = "_track_id";

        public static readonly string kSAEventDistinctId = "distinct_id";

        
        public static readonly string kSAEventLoginId = "login_id";

        public static readonly string kSAEventAnonymousId = "anonymous_id";

        public static readonly string kSAEventProperties = "properties";

        public static readonly string kSAEventLib = "lib";

        // 事件名
        public static readonly string kSAEventName = "event";

        #endregion
        #region eventName        /* eventName */        // 登录事件        public static readonly string kSAEventNameSignUp = "$SignUp";

        // 绑定事件
        public static readonly string kSAEventBind = "$BindID";

        // 解绑事件
        public static readonly string kSAEventUnBind = "$UnbindID";        // 激活事件        public static readonly string kSAEventNameAppInstall = "$AppInstall";        public static readonly string kSAEventNameAppStart = "$AppStart";        public static readonly string kSAEventNameAppEnd = "$AppEnd";        #endregion
        #region 预置属性
        // App 版本 & lib        public static readonly string kSAEventPresetPropertyAppVersion = "$app_version";

        // 事件时长
        public static readonly string kSAEventDurationProperty = "event_duration";

        // UA
        public static readonly string kSAEventPropertyUserAgent = "$user_agent";

        public static readonly string kSAEventPropertyFirstVisitTime = "$first_visit_time";

        public static readonly string kSAEventPropertyIsFirstTime = "$is_first_time";

        #endregion
        #region 自定义预置属性
        /* common property */        // 自定义 project        public static readonly string kSAEventPropertyProject = "$project";

        // 自定义 token
        public static readonly string kSAEventPropertyToken = "$token";

        // project 属性，自定义后才包含
        public static readonly string kSAEventProject = "project";

        // token 属性，自定义后才包含
        public static readonly string kSAEventToken = "token";

        // 自定义事件时间
        public static readonly string kSAEventPropertyTime = "$time";

        //神策成立时间，2015-05-15 10:24:00.000，某些时间戳判断（毫秒）
        public static readonly Int64 kSAEventCommonOptionalPropertyTimeInt = 1431656640000;

        #endregion

        #region track type

        /* track type */
        public static readonly string kSAEventTypeTrack = "track";

        public static readonly string kSAEventTypeSignup = "track_signup";

        public static readonly string kSAEventTypeBind = "track_id_bind";

        public static readonly string kSAEventTypeUnbind = "track_id_unbind";

        #endregion

        #region Profile 相关事件

        /* Profile Set */
        public static readonly string kSAProfileSet = "profile_set";

        public static readonly string kSAProfileSetOnce = "profile_set_once";

        public static readonly string kSAProfileUnset = "profile_unset";

        public static readonly string kSAProfileDelete = "profile_delete";

        public static readonly string kSAProfileAppend = "profile_append";

        public static readonly string kSAProfileIncrement = "profile_increment";


        #endregion


        #region 本地文件存储 Key


        /* 数据存储 key */
        public static readonly string kSARandomDeviceIDFileName = "SensorsData_Device_ID";

        #endregion

        #region 特殊常量定义

        //event name、property key、value max length
        public static readonly int kSAEventNameMaxLength = 100;
        // 属性值长度判断
        public static readonly int kSAPropertyValueMaxLength = 1024;

        #endregion

    }

}
