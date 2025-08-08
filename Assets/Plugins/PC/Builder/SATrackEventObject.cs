//// Created by 储强盛 on 2023/3/10.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using System.Collections.Generic;using SensorsAnalyticsPCSDK.Utils;
using SensorsAnalyticsPCSDK.Constant;namespace SensorsAnalyticsPCSDK.Builder{    /// <summary>    /// track 事件基类    /// </summary>
    public class SATrackEventObject: SABaseEventObject
	{
		/// <summary>		/// 初始化方法，先执行父类初始化		/// </summary>		/// <param name="eventId">事件 Id</param>
		public SATrackEventObject(string eventId): base()
		{
            if(eventId is string)            {                this.EventId = eventId;            }
		}        /// <summary>        /// 获取原始事件名,只读，不包含 UUID        /// </summary>        public string Event        {            get            {                if(!EventId.EndsWith(SAConstant.kSAEventIdSuffix))                {                    return EventId;                }                if(EventId.Length < 45)                {                    return null;                }                string eventName = EventId.Substring(0, EventId.Length - 45);                return eventName;            }        }        /// <summary>        /// 添加事件属性        /// </summary>        /// <param name="properties">事件属性</param>        public override void AddEventProperties(Dictionary<string, object> properties)        {            if(!SAValidator.IsValidDictionary(properties))            {                return;            }            foreach (KeyValuePair<string, object> kvp in properties)
            {                this.Properties[kvp.Key] = kvp.Value;
            }        }        /// <summary>        /// 添加公共属性        /// </summary>        /// <param name="superProperties">公共属性</param>        public override void AddSuperProperties(Dictionary<string, object> superProperties)        {            Dictionary<string, object> properties = ValidProperties(superProperties);            if(!SAValidator.IsValidDictionary(properties))            {                return;            }            foreach (KeyValuePair<string, object> kvp in properties)
            {
              this.Properties[kvp.Key] = kvp.Value;
            }
            // 从公共属性中更新 lib 节点中的 $app_version 值
            if(properties.ContainsKey(SAConstant.kSAEventPresetPropertyAppVersion))            {                string appVersion = (string)properties[SAConstant.kSAEventPresetPropertyAppVersion];                if (!string.IsNullOrEmpty(appVersion))                {                    this.LibObject.AppVersion = appVersion;                }            }
        }


        /// <summary>        /// 添加事件时长        /// </summary>        /// <param name="duration">事件时长，单位：秒</param>        public override void AddDurationProperty(float duration)        {            if(duration > 0)
            {
                this.Properties[SAConstant.kSAEventDurationProperty] = duration;
            }        }


        /// <summary>        /// 校验事件名称，报错日志提示        /// </summary>        public override void ValidateEventName()        {            if(string.IsNullOrEmpty(this.EventId))            {                SALogger.Error("Property key or Event name is empty");                return;            }            // 内置字符，正则判断，非法关键字，日志提示            if(!SAValidator.ValidateKey(this.EventId))            {                SALogger.Error("The event [" + this.EventId + "] is invalid.");                return;            }            // 长度校验            if(this.EventId.Length > SAConstant.kSAEventNameMaxLength)            {                SALogger.Error("roperty key or Event name" + this.EventId +  "'s length is longer than " + SAConstant.kSAEventNameMaxLength);            }        }

        public override Dictionary<string, object>JsonObject()        {            // 默认 添加 lib 中的预置属性信息            Dictionary<string, object> libProperties = LibObject.LibPropertiesJson();
            foreach (KeyValuePair<string, object> kvp in libProperties)
            {                this.Properties[kvp.Key] = kvp.Value;
            }            Dictionary<string, object> eventInfo = base.JsonObject();            if(!string.IsNullOrEmpty(Event))            {                eventInfo[SAConstant.kSAEventName] = Event;            }            return eventInfo;        }
	}    /// <summary>    /// 登录事件对象    /// </summary>    public class SASignUpEventObject : SATrackEventObject    {        public SASignUpEventObject(string eventId): base(eventId)        {            this.Type = SAConstant.kSAEventTypeSignup;        }        public override Dictionary<string, object>JsonObject()        {            Dictionary<string, object> jsonObject = base.JsonObject();            jsonObject["original_id"] = this.OriginalId;            return jsonObject;        }        public override bool IsSignUp()        {            return true;        }    }    /// <summary>    /// 自定义事件    /// </summary>    public class SACustomEventObject : SATrackEventObject    {        public SACustomEventObject(string eventId): base(eventId)        {            this.Type = SAConstant.kSAEventTypeTrack;        }    }    /// <summary>    /// 采集预置事件    /// $AppInstall、AppCrashed、$AppRemoteConfigChanged 等预置事件    /// </summary>    public class SAPresetEventObject : SATrackEventObject    {        public SAPresetEventObject(string eventId): base(eventId)        {            this.Type = SAConstant.kSAEventTypeTrack;        }        }    /// <summary>    /// 采集全埋点事件    /// $AppStart、$AppEnd、$AppViewScreen、$AppClick 全埋点事件    /// </summary>    public class SAAutoTrackEventObject : SATrackEventObject    {        public readonly string kSALibMethodAutoTrack = "autoTrack";        public SAAutoTrackEventObject(string eventId) : base(eventId)        {            this.Type = SAConstant.kSAEventTypeTrack;            this.LibObject.LibMethod = kSALibMethodAutoTrack;        }    }}


