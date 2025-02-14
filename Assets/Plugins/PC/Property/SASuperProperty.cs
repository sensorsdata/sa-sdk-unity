//// Created by 储强盛 on 2023/3/15.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Database;
using SensorsAnalyticsPCSDK.Utils;

namespace SensorsAnalyticsPCSDK.Property{
    public class SASuperProperty
	{
		// 公共属性 json 存储文件名
		private readonly string kSASuperPropertyJsonFileName = @"SensorsData_SuperProperties";

		private Dictionary<string, object> _superProperties;

		public SASuperProperty()
		{
			_superProperties = UnarchiveProperties();
		}

		/// <summary>		/// 注册公共属性		/// </summary>		/// <param name="properties">属性内容</param>
		public void RegisterSuperProperties(Dictionary<string, object>properties)		{			if(!SAValidator.IsValidDictionary(properties))			{				return;			}			foreach (KeyValuePair<string, object> kvp in properties)
            {                _superProperties[kvp.Key] = kvp.Value;
            }            ArchiveProperties(_superProperties);        }

		/// <summary>		/// 注销某个属性		/// </summary>		/// <param name="propertyName">属性名</param>
		public void UnregisterSuperProperty(string propertyName)		{			if(string.IsNullOrEmpty(propertyName))			{				return;			}			_superProperties.Remove(propertyName);			ArchiveProperties(_superProperties);		}

		/// <summary>		/// 获取当前公共属性		/// </summary>		/// <returns></returns>
		public Dictionary<string, object>CurrentSuperProperties()		{			return _superProperties;		}

		/// <summary>		/// 清除公共属性		/// </summary>
		public void ClearSuperProperties()		{			_superProperties = null;			SAFileStore.RemoveObject(kSASuperPropertyJsonFileName);		}

		// 本地解析 superProperties
		private Dictionary<string, object>UnarchiveProperties()		{			string base64Json = (string)SAFileStore.ReadObject(kSASuperPropertyJsonFileName, typeof(string));            if (string.IsNullOrEmpty(base64Json))            {                return new Dictionary<string, object>();            }            // base 64 解码            string propertiesJson = SAUtils.Base64DecodingString(base64Json);            if (string.IsNullOrEmpty(propertiesJson))            {                return new Dictionary<string, object>();            }

			Dictionary<string, object> propertiesObject = SAJSONUtils.JSONObjectWithString(propertiesJson);            if (!SAValidator.IsValidDictionary(propertiesObject))            {                return new Dictionary<string, object>();            }            return propertiesObject;		}

		// superProperties 本地保存
		private void ArchiveProperties(Dictionary<string, object> properties)		{			if(properties == null)			{				return;			}			if(properties.Count == 0)			{				SAFileStore.RemoveObject(kSASuperPropertyJsonFileName);				return;			}			string propertiesStr = SAJSONUtils.StringWithJSONObject(properties);            if (string.IsNullOrEmpty(propertiesStr))            {                return;            }            // base64 编码            string base64PropertiesStr = SAUtils.Base64EncodingString(propertiesStr);            if (string.IsNullOrEmpty(base64PropertiesStr))            {                return;            }            SAFileStore.WriteObject(kSASuperPropertyJsonFileName, base64PropertiesStr);		}
	}

}

