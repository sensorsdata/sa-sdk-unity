//// Created by 储强盛 on 2023/3/13.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using System.Collections.Generic;
using System.Text;

namespace SensorsAnalyticsPCSDK.Utils{
    public class SAUtils
	{

		/// <summary>        /// 日志提示转字符        /// </summary>        /// <param name="dictionary"></param>        /// <returns></returns>
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

        /// <summary>        /// base64 编码        /// </summary>        /// <param name="inputStr">原始明文字符</param>        /// <returns>base64 编码后的结果</returns>
        public static string Base64EncodingString(string inputStr)        {            if(string.IsNullOrEmpty(inputStr))            {                return null;            }            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(inputStr);
            string base64Str = System.Convert.ToBase64String(bytes);            return base64Str;        }

        /// <summary>        /// base64 解码        /// </summary>        /// <param name="base64Str">base64 编码的字符</param>        /// <returns>base64 解码后的结果</returns>
        public static string Base64DecodingString(string base64Str)        {            if(string.IsNullOrEmpty(base64Str))            {                return null;            }            byte[] bytes = System.Convert.FromBase64String(base64Str);
            string originStr = System.Text.Encoding.UTF8.GetString(bytes);            return originStr;        }

	}
}


