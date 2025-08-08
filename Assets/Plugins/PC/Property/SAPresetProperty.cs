//// Created by 储强盛 on 2023/3/14.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Builder;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Database;

namespace SensorsAnalyticsPCSDK.Property{

    public class SAPresetPropertyObject
	{
		
		public string DeviceId;
		public bool IsFirstDay = false;

		/// 设备型号
		private readonly string kSAEventPresetPropertyModel = "$model";
		/// 生产商
		private readonly string kSAEventPresetPropertyManufacturer = "$manufacturer";
		/// 屏幕高
		private readonly string kSAEventPresetPropertyScreenHeight = "$screen_height";
		/// 屏幕宽
		private readonly string kSAEventPresetPropertyScreenWidth = "$screen_width";

		/// 系统
		private readonly string kSAEventPresetPropertyOS = "$os";
		/// 系统版本
		private readonly string kSAEventPresetPropertyOSVersion = "$os_version";

		/// 应用 ID
		private readonly string SAEventPresetPropertyAppID = "$app_id";
		/// 应用名称
		private readonly string kSAEventPresetPropertyAppName = "$app_name";
		/// 时区偏移量
		private readonly string kSAEventPresetPropertyTimezoneOffset = "$timezone_offset";

		/// 网络类型
		//private readonly string kSAEventPresetPropertyNetworkType = "$network_type";

		/// 是否 WI-FI，无法准确区分
		//private readonly string kSAEventPresetPropertyWifi = "$wifi";
		
        private readonly string kSADeviceIDPropertyDeviceID = "$device_id";

        /// 是否首日
		private readonly string kSAEventPresetPropertyIsFirstDay = "$is_first_day";

		// 本地日期存储 key
		private readonly string kSAInstallDateFileName = "SensorsData_Install_Date";


		private readonly string _appID, _appName_, _manufacturer, _model, OS, _osVersion;
		//private readonly bool _isWifi;
		private readonly int _screenHeight, _screenWidth, _timeZoneOffset;


		public SAPresetPropertyObject()
		{
			_appID = SAAppInfo.AppIdentifier();
			_appName_ = SAAppInfo.AppName();

			_manufacturer = SADeviceInfo.Manufacturer();

			_model = SADeviceInfo.DeviceModel();
			OS = SADeviceInfo.OSName();
			_osVersion = SADeviceInfo.OSVersion();
			_screenWidth = SADeviceInfo.ScreenWidth();
			_screenHeight = SADeviceInfo.ScreenHeight();
			_timeZoneOffset = SADeviceInfo.TimeZone();

			DateTime now = DateTime.Now;
			string dateStr = now.ToString("yyyy-MM-dd");

			string lastDayString = (string)SAFileStore.ReadObject(kSAInstallDateFileName, typeof(string));			if(string.IsNullOrEmpty(lastDayString))			{				IsFirstDay = true;				SAFileStore.WriteObject(kSAInstallDateFileName, dateStr);			}			else			{				IsFirstDay = (dateStr.Equals(lastDayString));			}			DeviceId = SADeviceInfo.DeviceID();		}

		/// <summary>		/// 获取预置属性		/// </summary>		/// <returns></returns>
		public Dictionary<string, object> Properties()		{			Dictionary<string, object> properties = new Dictionary<string, object>();            if (!string.IsNullOrEmpty(_model))            {                properties.Add(kSAEventPresetPropertyModel, _model);            }            if (!string.IsNullOrEmpty(_manufacturer))            {                properties.Add(kSAEventPresetPropertyManufacturer, _manufacturer);            }            properties.Add(kSAEventPresetPropertyScreenHeight, _screenHeight);			properties.Add(kSAEventPresetPropertyScreenWidth, _screenWidth);			properties.Add(kSAEventPresetPropertyOS, OS);			properties.Add(kSAEventPresetPropertyOSVersion, _osVersion);			if(!string.IsNullOrEmpty(_appID))			{                properties.Add(SAEventPresetPropertyAppID, _appID);            }            if (!string.IsNullOrEmpty(_appName_))            {                properties.Add(kSAEventPresetPropertyAppName, _appName_);            }            properties.Add(kSAEventPresetPropertyTimezoneOffset, _timeZoneOffset);			return properties;		}

		/// <summary>		/// 不可覆盖的预置属性		/// 包括设备 Id，是否首日 等		/// </summary>
		public Dictionary<string, object> UncoverableProperties(string type = null)		{					Dictionary<string, object> properties = new Dictionary<string, object>();            properties.Add(kSADeviceIDPropertyDeviceID, DeviceId);			if(string.IsNullOrEmpty(type))			{				return properties;			}			if(type.Equals(SAConstant.kSAEventTypeTrack) || type.Equals(SAConstant.kSAEventTypeBind) || type.Equals(SAConstant.kSAEventTypeUnbind))			{				// 是否首日访问，只有 track/bind/unbind 事件添加 $is_first_day 属性				properties.Add(kSAEventPresetPropertyIsFirstDay, IsFirstDay);			}						return properties;		}
	}
}



