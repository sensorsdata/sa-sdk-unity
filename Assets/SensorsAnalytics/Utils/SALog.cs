/*
 * Created by zhangwei on 2020/8/6.
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
 */
 
using System;

namespace SensorDataAnalytics.Utils
{
    /// <summary>
    /// SensorsData Log Class
    /// </summary>
    public class SALog
    {
        private static bool isLogEnable = false;
        public static void IsLogEnalbe(bool isEnable)
        {
            isLogEnable = isEnable;
        }

        public static void Debug(string logMessage)
        {
            if (isLogEnable) UnityEngine.Debug.Log(logMessage);
        }

        public static void Warn(string logMessage)
        {
            if (isLogEnable) UnityEngine.Debug.LogWarning(logMessage);
        }

        public static void Error(string logMessage)
        {
            if (isLogEnable) UnityEngine.Debug.LogError(logMessage);
        }

        public static void Exception(Exception exception)
        {
            if (isLogEnable) UnityEngine.Debug.LogException(exception);
        }

    }

}
