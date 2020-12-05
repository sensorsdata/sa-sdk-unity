/*
 * Created by zhangwei on 2020/8/6.
 * Copyright 2015－2020 Sensors Data Inc.
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SensorDataAnalytics.Utils
{
    public class SAUtils
    {
        private static readonly Regex KEY_PATTERN = new Regex(@"^((?!^distinct_id$|^original_id$|^time$|^properties$|^id$|^first_id$|^second_id$|^users$|^events$|^event$|^user_id$|^date$|^datetime$)[a-zA-Z_$][a-zA-Z\\d_$]{0,99})$");

        /// <summary>
        /// 将字符串转换成 Java JSONObject 对象
        /// </summary>
        /// <param name="jsonStr">json 字符串</param>
        /// <returns>AndroidJavaObject，如果不能成功转换则会返回 null</returns>
        public static AndroidJavaObject Parse2JavaJSONObject(string jsonStr)
        {
            if (jsonStr == null || "null".Equals(jsonStr))
            {
                return null;
            }
            try
            {
                return new AndroidJavaObject("org.json.JSONObject", jsonStr);
            }
            catch (Exception e)
            {
                SALog.Error("Can not parse " + jsonStr + "to JSONObject: " + e);
            }
            return null;
        }

        /// <summary>
        /// 将字典转换成 Json 字符串
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <returns>json 字符串，如果字典不能转换成字符串那么会返回 null。</returns>
        public static string Parse2JsonStr(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            try
            {
                return MiniJSON.Json.Serialize(dictionary);
            }
            catch (Exception e)
            {
                SALog.Error(e.Message);
            }
            return null;
        }

        /// <summary>
        /// 将 json 字符串转换成字典
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns>字典对象，如果不能转换则返回 null。</returns>
        public static Dictionary<string, object> Parse2Dictionary(string jsonStr)
        {
            if (jsonStr == null)
            {
                return null;
            }
            try
            {
                return MiniJSON.Json.Deserialize(jsonStr) as Dictionary<string, object>;
            }
            catch (Exception ex)
            {
                SALog.Error(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 判断事件的 key 值是否合规
        /// </summary>
        /// <param name="key">event key</param>
        /// <returns>合规就返回 true，否则返回 false</returns>
        public static bool AssertKey(string key)
        {
            if (key == null || key.Length == 0)
            {
                SALog.Error("The key is empty.");
                return false;
            }
            if (key.Length > 255)
            {
                SALog.Error("The key [" + key + "] is too long, max length is 255.");
                return false;
            }

            if (KEY_PATTERN.IsMatch(key))
            {
                return true;
            }
            else
            {
                SALog.Error("The key [" + key + "] is invalid.");
                return false;
            }
        }

        /// <summary>
        /// 判断字典的 value 是否为支持的类型，当前支持 string,number,DateTime
        /// </summary>
        /// <param name="dic">event properties dictionary</param>
        /// <returns>合规就返回 true，否则返回 false</returns>
        public static bool AssertValue(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return true;
            }
            foreach (var value in dic.Values)
            {
                if (!(value is string || IsNumeric(value) || value is DateTime))//TODO 此处校验不全
                {
                    SALog.Error("The property values must be an instance of string, number or DateTime");
                    return false;
                }
            }
            return true;
        }

        public static string ToDebugString(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var item in dictionary)
            {
                sb.Append("").Append(item.Key).Append(",").Append(item.Value).Append("),");
            }
            sb.Append("]");
            return sb.ToString();
        }


        public static bool IsNumeric(object obj)
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
                || obj is float;
        }
    }
}