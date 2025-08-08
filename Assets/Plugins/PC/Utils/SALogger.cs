// Created by 储强盛 on 2023/3/9.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.

using System;
using System.Collections;using System.Diagnostics;namespace SensorsAnalyticsPCSDK.Utils{
    // 日志打印
    public class SALogger    {        public static bool EnableLog = false;        // 日志前缀        private static string s_prefixLogger = "【SensorsAnalytics Unity SDK】 ";        public static void Warn(params object[] objs)        {            if (!EnableLog)            {                return;            }            string logInfo = BuildLogInfo(objs);            UnityEngine.Debug.LogWarning(logInfo);        }        public static void Error(params object[] objs)        {            if (!EnableLog)            {                return;            }            string logInfo = BuildLogInfo(objs);            UnityEngine.Debug.LogError(logInfo);        }        public static void Exception(Exception exception)        {            if (!EnableLog)            {                return;            }            UnityEngine.Debug.LogException(exception);        }        public static void LogInfo(params object[] objs)        {            if(!EnableLog)            {                return;            }            string logInfo = BuildLogInfo(objs);            UnityEngine.Debug.Log(logInfo);        }        private static string BuildLogInfo(params object[] objs)        {            // 获取调用方法所在的文件名，方法名和代码行数            // 内部封装了一层，所以这个 GetFrame 是 2            StackFrame frame = new StackTrace(true).GetFrame(2);            var file = frame.GetFileName();
            var className = frame.GetMethod().DeclaringType.Name;
            var method = frame.GetMethod().Name;
            var line = frame.GetFileLineNumber();            // 获取当前时间，并且格式化为 2023-03-17 10:16:31.236 这种形式            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string logInfo = $"{s_prefixLogger} + [{time}] [{file}] [{className} {method}] ({line})\n";

            // 遍历可变参数数组中的每个对象
            foreach (var obj in objs)
            {
                // 如果对象是 Dictionary 类型，则序列化为 JSON 格式，并且设置 Formatting.Indented 选项来换行显示
                if (obj is IDictionary)
                {
                    // JsonConvert.SerializeObject(obj, Formatting.Indented);
                    var json = SAJSONUtils.StringWithJSONObject(obj);
                    logInfo += json;
                }
                // 对象如果是 list 类型，遍历判断每个元素，再判断处理
                else if (obj is IList)                {                    IList list = obj as IList;                    foreach (var obj1 in list)                    {                        if (obj1 is IDictionary)                        {                            //JsonConvert.SerializeObject(obj1, Formatting.Indented);                            var json = SAJSONUtils.StringWithJSONObject(obj1);                            logInfo += json;                        }                        else                        {                            logInfo += obj1;                        }                        logInfo += "\n";                    }                }
                else
                {
                    // 否则直接输出对象的字符串表示形式
                    logInfo += obj;
                }
            }            return logInfo;        }        private static string BuildLogInfo(string logMessage)        {            // 获取调用方法所在的文件名，方法名和代码行数            // 内部封装了一层，所以这个 GetFrame 是 2            StackFrame frame = new StackTrace(true).GetFrame(2);            var file = frame.GetFileName();
            var className = frame.GetMethod().DeclaringType.Name;
            var method = frame.GetMethod().Name;
            var line = frame.GetFileLineNumber();            // 获取当前时间，并且格式化为 2023-03-17 10:16:31.236 这种形式            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string logInfo = $"{s_prefixLogger} + [{time}] [{file}] [{className} {method}] ({line})";            logInfo += logMessage;            return logInfo;        }    }
}



