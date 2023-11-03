﻿//
using SensorsAnalyticsPCSDK.Database;
using SensorsAnalyticsPCSDK.Utils;

namespace SensorsAnalyticsPCSDK.Property
    public class SASuperProperty
	{
		// 公共属性 json 存储文件名
		private readonly string kSASuperPropertyJsonFileName = @"SensorsData_SuperProperties";

		private Dictionary<string, object> _superProperties;

		public SASuperProperty()
		{
			_superProperties = UnarchiveProperties();
		}

		/// <summary>
		public void RegisterSuperProperties(Dictionary<string, object>properties)
            {
            }

		/// <summary>
		public void UnregisterSuperProperty(string propertyName)

		/// <summary>
		public Dictionary<string, object>CurrentSuperProperties()

		/// <summary>
		public void ClearSuperProperties()

		// 本地解析 superProperties
		private Dictionary<string, object>UnarchiveProperties()

			Dictionary<string, object> propertiesObject = SAJSONUtils.JSONObjectWithString(propertiesJson);

		// superProperties 本地保存
		private void ArchiveProperties(Dictionary<string, object> properties)
	}

}
