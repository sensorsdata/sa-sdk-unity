//// Created by 储强盛 on 2023/3/13.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;using System.Collections.Generic;using System.Text.RegularExpressions;namespace SensorsAnalyticsPCSDK.Utils{    public class SAValidator
	{
		public SAValidator()
		{
		}

		private static readonly Regex kSARegexPattern = new Regex("^((?!^distinct_id$|^original_id$|^time$|^properties$|^id$|^first_id$|^second_id$|^users$|^events$|^event$|^user_id$|^date$|^datetime$|^user_tag.*|^user_group.*)[a-zA-Z_$][a-zA-Z\\d_$]*)$");

		/// <summary>
        /// 判断事件的 key 值是否合规
        /// </summary>
        /// <param name="key">event key</param>
        /// <returns>合规就返回 true，否则返回 false</returns>
        public static bool ValidateKey(string key)
        {
            if (key == null || key.Length == 0)
            {
                SALogger.Error("The key is empty.");
                return false;
            }
            if (key.Length > 255)
            {
                SALogger.Error("The key [" + key + "] is too long, max length is 255.");
                return false;
            }

            if (kSARegexPattern.IsMatch(key))
            {
                return true;
            }
            else
            {
                SALogger.Error("The key [" + key + "] is invalid.");
                return false;
            }
        }

        /// <summary>        /// 校验是否为合法的属性值        /// </summary>        /// <param name="value">属性值</param>        /// <returns></returns>
        public static bool IsValidPropertyValue(object value)        {            if (value == null)            {                return false;            }            if ((value is string) || IsNumerValue(value) || (value is DateTime) || value.GetType().IsArray)            {                return true;            }            //  判断是否为 List<> 类型            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))            {                return true;            }            SALogger.Error("The value:" + value + "invalid, property value must be an instance of string, number, DateTime, Array or List");            return false;        }

        public static bool IsValidDictionary(Dictionary<string, object>dic)        {            if (dic == null || dic.Count == 0)            {                return false;            }            return true;        }

        // 是否为数值
        public static bool IsNumerValue(object obj)
        {
            return obj is sbyte
                || obj is byte
                || obj is short
                || obj is ushort
                || obj is int
                || obj is uint
                || obj is long
                || obj is ulong
                || obj is double
                || obj is decimal
                || obj is float
                || obj is bool;
        }

        // 是否为有效的 url
        public static bool IsValidServerURL(string serverURL)        {            return serverURL != null && serverURL.Length > 0 && (serverURL.Contains("http") || serverURL.Contains("https"));        }
    }}
