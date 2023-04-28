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

namespace SensorsAnalyticsPCSDK.Builder
{
    public class SAAppInfo
    {

/* App 信息 */
        // App 名称
        public static string AppName() {
            return Application.productName;
        }

        // App 标识，包名
        public static string AppIdentifier() {
            return Application.identifier;
        }

        public static string AppVersion() {
            return Application.version;
        }
    }
}