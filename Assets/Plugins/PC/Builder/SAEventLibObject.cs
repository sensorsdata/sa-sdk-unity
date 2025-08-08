//
// Created by 储强盛 on 2023/3/9.
// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Builder;

namespace SensorsAnalyticsPCSDK.Builder
{
    /// <summary>
    /// lib 信息
    /// </summary>
    public class SAEventLibObject
    {
        // SDK 类型
        private readonly string kSAEventPresetPropertyLib = "$lib";

        // SDK 数据采集类型
        private readonly string kSAEventPresetPropertyLibMethod = "$lib_method";
        // SDK 版本
        private readonly string kSAEventPresetPropertyLibVersion = "$lib_version";

        // 客景采集版本号，只采集预置属性，不包含在 lib，类型为 List
        private readonly string kSAEventPresetPropertyDataSource = "$data_ingestion_source";

        //private readonly string kSALibMethodCode = "code";
        public string LibMethod = "code";
        public string LibVersion = SAConstant.kSALibVersion;
        public string Lib = SAConstant.kSAEventPresetPropertyLibName;
        public string AppVersion;

        public SAEventLibObject() : base()
        {
            AppVersion = SAAppInfo.AppVersion();
        }

        public void SetMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                return;
            }
            LibMethod = method;
        }

        /// <summary>
        /// lib 信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> LibJsonObject()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add(kSAEventPresetPropertyLib, Lib);
            properties.Add(kSAEventPresetPropertyLibVersion, LibVersion);
            properties.Add(SAConstant.kSAEventPresetPropertyAppVersion, AppVersion);
            properties.Add(kSAEventPresetPropertyLibMethod, LibMethod);

            return properties;
        }

        // 增加 $data_ingestion_source
        public Dictionary<string, object> LibPropertiesJson()
        {
            Dictionary<string, object> properties = LibJsonObject();
            List<string> list = new List<string>();
            list.Add(Lib);
            properties.Add(kSAEventPresetPropertyDataSource, list);
            return properties;
        }

    }
}
