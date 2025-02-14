//// Created by 储强盛 on 2023/3/9.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Utils;

namespace SensorsAnalyticsPCSDK.Main{	/// <summary>	/// PC SDK 初始化参数	/// </summary>
	public class SAConfigOptions
	{		public string ServerURL;		// 是否开启日志打印		public bool EnableLog = false;		public SAAutoTrackType AutoTrackType = SAAutoTrackType.None;		public SANetworkType FlushNetworkPolicy = SANetworkType.ALL;		private int _flushBulkSize = 100;		private int _flushInterval = 15;		private int _maxCacheSize = 10000;		/// <summary>
		/// 初始化配置
		/// </summary>
		/// <param name="serverURL"></param>
		public SAConfigOptions(string serverURL)
		{
			if (!SAValidator.IsValidServerURL(serverURL))			{				SALogger.Error("invalid serverURL");				return;			}			this.ServerURL = serverURL;		}		/// <summary>		/// 设置本地缓存日志的最大条目数，默认 100，最小 50 条		/// </summary>
		public int FlushBulkSize
		{
			get
			{
				return _flushBulkSize;
			}
			set
			{
				if (value >= 50)
				{					_flushBulkSize = value;
				}
				else
				{					_flushBulkSize = 50;
				}
			}
		}		///// <summary>		///// 两次数据发送的最小时间间隔，单位秒		///// 默认值为 15 秒，在每次调用 track 和 profileSet 等接口的时候，都会检查，以判断是否向服务器上传数据		///// </summary>		public int FlushInterval
		{
			get
			{
				return _flushInterval;
			}
			set
			{				_flushInterval = value > 5 ? value : 5;
			}
		}		/// <summary>		/// 最大缓存事件条数，默认 100000 条		/// </summary>		public int MaxCacheSize		{			get			{				return _maxCacheSize;			}			set			{				_maxCacheSize = value > 10000 ? value : 10000;			}		}    }}


