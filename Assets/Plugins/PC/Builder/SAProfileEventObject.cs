//// Created by 储强盛 on 2023/3/10.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using SensorsAnalyticsPCSDK.Utils;
using System.Collections.Generic;

namespace SensorsAnalyticsPCSDK.Builder{
	/// <summary>	/// Profile 事件类型	/// </summary>
    public class SAProfileEventObject : SABaseEventObject
	{
		public SAProfileEventObject(string type): base()
		{
			Type = type;
		}		// 添加 Profile 属性		public void AddProfileProperties(Dictionary<string, object> properties)		{            if (!SAValidator.IsValidDictionary(properties))            {                return;            }            foreach (KeyValuePair<string, object> kvp in properties)
            {                this.Properties[kvp.Key] = kvp.Value;
            }        }    }

	/// <summary>	/// profile_increment Profile 增加数值;	/// 暂未使用	/// </summary>
	public class SAProfileIncrementEventObject: SABaseEventObject	{        public override object ValidPropertyValue(string key,object value)		{			object newValue = base.ValidPropertyValue(key, value);			// 判断是否为数值类型			if(!value.GetType().IsValueType)			{				SALogger.Error("profile_increment value must be ValueType， got: " + value);				return null;			}			return newValue;		}	}

	/// <summary>	/// profile_append 向一个 NSSet 或者 NSArray 类型的 value 添加一些值	/// 暂未使用	/// </summary>
	public class SAProfileAppendEventObject : SABaseEventObject    {        public override object ValidPropertyValue(string key,object value)		{			object newValue = base.ValidPropertyValue(key, value);			// 判断是否为数值类型，待定是否支持集合或其他类型			if(!value.GetType().IsArray)			{				SALogger.Error("profile_append value must be Array， got: " + value);				return null;			}			return newValue;		}    }
}


