//// Created by 储强盛 on 2023/3/22.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;using System.IO.Compression;using System.Net;using System.Text;
using SensorsAnalyticsPCSDK.Utils;using UnityEngine.Networking;
namespace SensorsAnalyticsPCSDK.Network{
    /// <summary>    /// 上传回调    /// </summary>    /// <param name="webRequest">请求 request，获取上传状态</param>    /// <param name="eventDatas">上传的数据 list</param>
    public delegate void SAResponseHandle(UnityWebRequest webRequest = null, IList<Dictionary<string, object>> eventDatas = null);

    public class SAWebRequest    {        private readonly string _serverURL;        public SAWebRequest(string serverURL)        {            _serverURL = serverURL;        }        public IEnumerator RequestData(SAResponseHandle responseHandle, IList<Dictionary<string, object>> eventDatas)        {            //Dictionary<string, object> param = new Dictionary<string, object>();            string eventContent = SAJSONUtils.StringWithJSONObject(eventDatas);            // 编码和压缩处理            string encodeContent = BuildEventRecords(eventContent);            string urlEncodeContent = WebUtility.UrlEncode(encodeContent);                    // 拼接校验信息 crc=            //int hashCode = encodeContent.GetHashCode();            string jsonString = $"gzip=1&data_list={urlEncodeContent}";            byte[] requestBody = Encoding.UTF8.GetBytes(jsonString);            using UnityWebRequest webRequest = new UnityWebRequest(_serverURL, "POST");            webRequest.timeout = 30 * 1000;            webRequest.SetRequestHeader("Content-Type", "text/plain");            webRequest.SetRequestHeader("User-Agent", "SensorsAnalytics Unity SDK");            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(requestBody);            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();            yield return webRequest.SendWebRequest();#if UNITY_2020_1_OR_NEWER
            switch (webRequest.result)            {                case UnityWebRequest.Result.ConnectionError:                case UnityWebRequest.Result.DataProcessingError:                case UnityWebRequest.Result.ProtocolError:                    SALogger.Error("Error response : \n", webRequest.error);                    break;                case UnityWebRequest.Result.Success:                    SALogger.LogInfo("SAWebRequest RequestData Success: \n", webRequest.downloadHandler.text);                    break;            }#else
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    SALogger.Error("Error response : \n", webRequest.error);
                }
                else
                {
                     SALogger.LogInfo("SAWebRequest RequestData Success: \n", webRequest.downloadHandler.text);
                }
#endif
            responseHandle?.Invoke(webRequest, eventDatas);        }        // 事件压缩和编码        private static string BuildEventRecords(string inputStr)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputStr);
            using var outputStream = new MemoryStream();            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))                gzipStream.Write(inputBytes, 0, inputBytes.Length);            var outputBytes = outputStream.ToArray();            var base64Output = Convert.ToBase64String(outputBytes);            return base64Output;
        }    }

}


