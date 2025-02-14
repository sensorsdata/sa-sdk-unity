/*
 * Created by 储强盛 on 2023/03/07.
 * Copyright 2015－2023 Sensors Data Co., Ltd. All rights reserved
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

using UnityEngine;
using System;

namespace SensorsAnalyticsPCSDK.Database
{
    public class SAFileStore
    {
        public static void WriteObject(string key, object value)
        {
            if(string.IsNullOrEmpty(key))            {                return;            }

            if (value.GetType() == typeof(int))            {                PlayerPrefs.SetInt(key, (int)value);            }            else if (value.GetType() == typeof(float))            {                PlayerPrefs.SetFloat(key, (float)value);            }            else if (value.GetType() == typeof(string))            {                PlayerPrefs.SetString(key, (string)value);            }             PlayerPrefs.Save();        }

        public static object ReadObject(string key, Type type)
        {
            if (!string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key))
            {
                if (type == typeof(int))
                {
                    return PlayerPrefs.GetInt(key);
                }
                else if (type == typeof(float))
                {
                    return PlayerPrefs.GetFloat(key);
                }
                else if (type == typeof(string))
                {
                    return PlayerPrefs.GetString(key);
                }
                PlayerPrefs.Save();
            }
            return null;
        }

        public static void RemoveObject(string key)        {            if(string.IsNullOrEmpty(key))            {                return;            }            if(PlayerPrefs.HasKey(key))            {                PlayerPrefs.DeleteKey(key);            }        }

        public static bool HasObject(string key)        {            if (string.IsNullOrEmpty(key))            {                return false;            }            return PlayerPrefs.HasKey(key);        }
    }
}