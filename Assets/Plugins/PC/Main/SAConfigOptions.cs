﻿//
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Utils;

namespace SensorsAnalyticsPCSDK.Main
	public class SAConfigOptions
	{
		/// 初始化配置
		/// </summary>
		/// <param name="serverURL"></param>
		public SAConfigOptions(string serverURL)
		{
			if (!SAValidator.IsValidServerURL(serverURL))
		public int FlushBulkSize
		{
			get
			{
				return _flushBulkSize;
			}
			set
			{
				if (value >= 50)
				{
				}
				else
				{
				}
			}
		}
		{
			get
			{
				return _flushInterval;
			}
			set
			{
			}
		}

