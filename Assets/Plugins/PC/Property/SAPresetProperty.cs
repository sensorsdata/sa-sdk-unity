﻿//
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Builder;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Database;

namespace SensorsAnalyticsPCSDK.Property

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

			string lastDayString = (string)SAFileStore.ReadObject(kSAInstallDateFileName, typeof(string));

		/// <summary>
		public Dictionary<string, object> Properties()

		/// <summary>
		public Dictionary<string, object> UncoverableProperties(string type = null)
	}
}


