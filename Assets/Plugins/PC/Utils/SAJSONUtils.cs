//// Created by 储强盛 on 2023/3/13.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using System.Collections.Generic;
namespace SensorsAnalyticsPCSDK.Utils{    public class SAJSONUtils    {        public SAJSONUtils()        {        }

        /// <summary>
        /// 将字典转换成 Json 字符串
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <returns>json 字符串，如果字典不能转换成字符串那么会返回 null。</returns>
        public static string StringWithJSONObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                return SAJSON.Serialize(obj);
            }
            catch (Exception e)
            {
                SALogger.Error(e.Message);
            }
            return null;
        }

        /// <summary>
        /// 将 json 字符串转换成字典
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns>字典对象，如果不能转换则返回 null。</returns>
        public static Dictionary<string, object> JSONObjectWithString(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                return null;
            }
            try
            {
                return SAJSON.Deserialize(jsonStr) as Dictionary<string, object>;
            }
            catch (Exception ex)
            {
                SALogger.Error(ex.Message);
            }
            return null;
        }

	}

}

