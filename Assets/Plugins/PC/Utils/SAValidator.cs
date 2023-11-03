﻿//
	{
		public SAValidator()
		{
		}

		private static readonly Regex kSARegexPattern = new Regex("^((?!^distinct_id$|^original_id$|^time$|^properties$|^id$|^first_id$|^second_id$|^users$|^events$|^event$|^user_id$|^date$|^datetime$|^user_tag.*|^user_group.*)[a-zA-Z_$][a-zA-Z\\d_$]*)$");

		/// <summary>
        /// 判断事件的 key 值是否合规
        /// </summary>
        /// <param name="key">event key</param>
        /// <returns>合规就返回 true，否则返回 false</returns>
        public static bool ValidateKey(string key)
        {
            if (key == null || key.Length == 0)
            {
                SALogger.Error("The key is empty.");
                return false;
            }
            if (key.Length > 255)
            {
                SALogger.Error("The key [" + key + "] is too long, max length is 255.");
                return false;
            }

            if (kSARegexPattern.IsMatch(key))
            {
                return true;
            }
            else
            {
                SALogger.Error("The key [" + key + "] is invalid.");
                return false;
            }
        }

        /// <summary>
        public static bool IsValidPropertyValue(object value)

        public static bool IsValidDictionary(Dictionary<string, object>dic)

        // 是否为数值
        public static bool IsNumerValue(object obj)
        {
            return obj is sbyte
                || obj is byte
                || obj is short
                || obj is ushort
                || obj is int
                || obj is uint
                || obj is long
                || obj is ulong
                || obj is double
                || obj is decimal
                || obj is float
                || obj is bool;
        }

        // 是否为有效的 url
        public static bool IsValidServerURL(string serverURL)
