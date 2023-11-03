﻿//
using System.Collections.Generic;
using SensorsAnalyticsPCSDK.Database;

namespace SensorsAnalyticsPCSDK.Network
    public class SANetworkTask : MonoBehaviour

        private readonly List<SAWebRequest> _requestList = new List<SAWebRequest>();
        private readonly List<SAResponseHandle> _responseHandleList = new List<SAResponseHandle>();

        private SAEventStore _eventStore;

        private static SANetworkTask s_networkTask;

        private bool _isWaiting = false;

        public SANetworkTask(): base()


        public static SANetworkTask InstanceTask(SAEventStore eventStore = null)
        {
            s_networkTask._eventStore = eventStore;
            return s_networkTask;
        }

        private void Awake()
            s_networkTask = this;
        }

        private void Start()

        private void Update()
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

        private void StartRequestSendData()
            if (_requestList.Count == 0)
            {
                return;
        }
        private IEnumerator SendData(SAWebRequest request, SAResponseHandle responseHandle, IList<Dictionary<string, object>> list)
            yield return request.RequestData(responseHandle, list);
        }

}
