//
// Created by 储强盛 on 2023/3/13.
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

using System.Collections.Generic;
using System.Linq;
using SensorsAnalyticsPCSDK.Constant;
using SensorsAnalyticsPCSDK.Database;
using SensorsAnalyticsPCSDK.Utils;

namespace SensorsAnalyticsPCSDK.Builder
{
	public class SAIdentifier
	{

		// Id 信息本地存储
		// 登录 Id
		private readonly string kSAIdentitiesLoginId = "$identity_login_id";

		// 匿名 Id
		private readonly string kSAIdentitiesAnonymousId = @"$identity_anonymous_id";

		// 完整 Id 信息 json 存储文件名
		private readonly string kSAIdentitiesJsonFileName = @"SensorsData_Identities";

		// 完整用户 id 信息
		private Dictionary<string, string> _identities = new Dictionary<string, string>();

		public SAIdentifier()
		{
			// 解析本地存储
			_identities = UnarchiveIdentities();

			// anonymous_id 为空则重置
			if (!_identities.ContainsKey(kSAIdentitiesAnonymousId))
			{
				string deviceId = SADeviceInfo.DeviceID();
				// 获取设备 Id 失败
				if (string.IsNullOrEmpty(deviceId))
				{
					deviceId = System.Guid.NewGuid().ToString("D");
				}
				_identities[kSAIdentitiesAnonymousId] = deviceId;
				ArchiveIdentities(_identities);
			}
		}

		/// <summary>
		/// 获取登录 Id
		/// </summary>
		/// <returns>登录 Id 值</returns>
		public string LoginId()
		{
			if (_identities.ContainsKey(kSAIdentitiesLoginId))
			{
				return _identities[kSAIdentitiesLoginId];
			}
			return null;
		}

		/// <summary>
		/// 获取匿名 Id
		/// </summary>
		/// <returns>匿名 Id 值</returns>
		public string AnonymousId()
		{
			return _identities[kSAIdentitiesAnonymousId];
		}


		/// <summary>
		/// 获取唯一 Id
		/// 唯一用户标识：loginId -> 设备 Id
		/// </summary>
		/// <returns>获取唯一 Id 值</returns>
		public string DistinctId()
		{
			string loginId = LoginId();
			if (!string.IsNullOrEmpty(loginId))
			{
				return loginId;
			}
			return AnonymousId();
		}

		/// <summary>
		/// 登录操作
		/// </summary>
		/// <param name="loginId">登录 Id</param>
		public bool Login(string loginId)
		{
			if (!IsValidLoginId(loginId))
			{
				return false;
			}
			_identities[kSAIdentitiesLoginId] = loginId;
			ArchiveIdentities(_identities);

			return true;
		}

		/// <summary>
		/// 修改匿名 Id
		/// </summary>
		/// <param name="anonymousId">新的匿名 Id</param>
		public bool Identify(string anonymousId)
		{

			if (!(anonymousId is string))
			{
				SALogger.Error("AnonymousId must be string");
				return false;
			}
			if (string.IsNullOrEmpty(anonymousId))
			{
				SALogger.Error("AnonymousId is empty");
				return false;
			}
			if (anonymousId.Length > SAConstant.kSAPropertyValueMaxLength)
			{
				SALogger.Error("AnonymousId: " + anonymousId + "'s length is longer than " + SAConstant.kSAPropertyValueMaxLength);
				return false;
			}
			string originAnonymousId = AnonymousId();
			if (originAnonymousId.Equals(anonymousId))
			{
				return false;
			}

			_identities[kSAIdentitiesAnonymousId] = anonymousId;

			ArchiveIdentities(_identities);

			return true;
		}

		/// <summary>
		/// 退出登录
		/// </summary>
		public void Logout()
		{
			_identities.Remove(kSAIdentitiesLoginId);

			ArchiveIdentities(_identities);
		}

		/// <summary>
		/// loginId 是否有效
		/// </summary>
		/// <param name="loginId">登录 Id</param>
		/// <returns></returns>
		private bool IsValidLoginId(string loginId)
		{

			if (!(loginId is string))
			{
				SALogger.Error("LoginId must be string");
				return false;
			}
			if (string.IsNullOrEmpty(loginId))
			{
				SALogger.Error("LoginId is empty");
				return false;
			}
			if (loginId.Length > SAConstant.kSAPropertyValueMaxLength)
			{
				SALogger.Error("LoginId: " + loginId + "'s length is longer than " + SAConstant.kSAPropertyValueMaxLength);
				return false;
			}

			string originLoginId = LoginId();
			string anonymousId = AnonymousId();
			// 为了避免将匿名 ID 作为 LoginID 传入
			if (loginId.Equals(originLoginId) || loginId.Equals(anonymousId))
			{
				return false;
			}

			return true;
		}

		// 本地解析 Identities
		private Dictionary<string, string> UnarchiveIdentities()
		{
			string base64Json = (string)SAFileStore.ReadObject(kSAIdentitiesJsonFileName, typeof(string));
			if (string.IsNullOrEmpty(base64Json))
			{
				return new Dictionary<string, string>();
			}
			// base 64 解码
			string identitiesJson = SAUtils.Base64DecodingString(base64Json);
			if (string.IsNullOrEmpty(identitiesJson))
			{
				return new Dictionary<string, string>();
			}

			Dictionary<string, object> identitiesObject = SAJSONUtils.JSONObjectWithString(identitiesJson);
			if (!SAValidator.IsValidDictionary(identitiesObject))
			{
				return new Dictionary<string, string>();
			}
			Dictionary<string, string> identitiesDic = identitiesObject.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
			return identitiesDic;
		}

		// Identities 本地保存
		private void ArchiveIdentities(Dictionary<string, string> identities)
		{
			Dictionary<string, object> identitiesObj = identities.ToDictionary(x => x.Key, x => (object)x.Value);
			if (!SAValidator.IsValidDictionary(identitiesObj))
			{
				return;
			}

			string identitiesStr = SAJSONUtils.StringWithJSONObject(identitiesObj);
			if (string.IsNullOrEmpty(identitiesStr))
			{
				return;
			}

			// base64 编码
			string base64IdentitiesStr = SAUtils.Base64EncodingString(identitiesStr);

			SAFileStore.WriteObject(kSAIdentitiesJsonFileName, base64IdentitiesStr);
		}
	}

}