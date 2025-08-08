//// Created by 储强盛 on 2023/3/23.// Copyright © 2015-2023 Sensors Data Co., Ltd. All rights reserved.//// Licensed under the Apache License, Version 2.0 (the "License");// you may not use this file except in compliance with the License.// You may obtain a copy of the License at////      http://www.apache.org/licenses/LICENSE-2.0//// Unless required by applicable law or agreed to in writing, software// distributed under the License is distributed on an "AS IS" BASIS,// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.// See the License for the specific language governing permissions and// limitations under the License.//using System.Collections;
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Database;using UnityEngine;

namespace SensorsAnalyticsPCSDK.Network{    [DisallowMultipleComponent]
    public class SANetworkTask : MonoBehaviour    {        private readonly static object s_networkLocker = new object();

        private readonly List<SAWebRequest> _requestList = new List<SAWebRequest>();
        private readonly List<SAResponseHandle> _responseHandleList = new List<SAResponseHandle>();

        private SAEventStore _eventStore;        // 每个 body 条数，非 debug 模式，固定 50 条        private int _bodyCount = 50;

        private static SANetworkTask s_networkTask;

        private bool _isWaiting = false;

        public SANetworkTask(): base()        {        }


        public static SANetworkTask InstanceTask(SAEventStore eventStore = null)
        {
            s_networkTask._eventStore = eventStore;
            return s_networkTask;
        }

        private void Awake()        {
            s_networkTask = this;
        }

        private void Start()        {        }

        private void Update()        {
            if (_requestList.Count > 0 && !_isWaiting)
            {
                WaitOne();
                StartRequestSendData();
            }
        }

        /// <summary>
        /// 持有信号
        /// </summary>
        public void WaitOne()
        {
            _isWaiting = true;
        }

        /// <summary>
        /// 释放信号
        /// </summary>
        public void Release()
        {
            _isWaiting = false;
        }

        public void SyncInvokeAllTask()
        {

        }

        public void StartRequest(SAWebRequest mRequest, SAResponseHandle responseHandle, int batchSize)
        {
            lock (s_networkLocker)
            {
                _requestList.Add(mRequest);
                _responseHandleList.Add(responseHandle);
            }
        }

        private void StartRequestSendData()        {
            if (_requestList.Count == 0)
            {
                return;            }            SAWebRequest request;            SAResponseHandle responseHandle;            IList<Dictionary<string, object>> list;            lock (s_networkLocker)            {                request = _requestList[0];                responseHandle = _responseHandleList[0];                // 读取数据                list = _eventStore.SelectEventRecords(_bodyCount);            }            if (request != null)            {                if (list != null && list.Count > 0)                {                    // 协程中执行耗时操作，而不会阻塞主线程                    this.StartCoroutine(this.SendData(request, responseHandle, list));                }                else                {                    responseHandle?.Invoke(null, null);                }                lock (s_networkLocker)                {                    _requestList.RemoveAt(0);                    _responseHandleList.RemoveAt(0);                }            }
        }
        private IEnumerator SendData(SAWebRequest request, SAResponseHandle responseHandle, IList<Dictionary<string, object>> list)        {
            yield return request.RequestData(responseHandle, list);
        }    }

}

