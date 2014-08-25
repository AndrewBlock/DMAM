using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DMAM.Core.Services
{
    public abstract class HttpTask<T> : ServiceTask<T>
    {
        private readonly HttpClient _client = new HttpClient();
        private Task<HttpResponseMessage> _task;
        private readonly object _taskLock = new object();

        protected HttpTask(Action<T> clientNotify, object clientData)
            : base(clientNotify, clientData)
        {
        }

        public override TaskStatus TaskStatus
        {
            get
            {
                lock (_taskLock)
                {
                    return _task != null ? _task.Status : TaskStatus.Created;
                }
            }
        }

        protected override void OnDispose()
        {
            lock (_taskLock)
            {
                if (_task != null)
                {
                    _task.Dispose();
                }

                if (_client != null)
                {
                    _client.Dispose();
                }
            }
        }

        protected abstract T ProcessHttpResult(HttpResponseMessage response);

        protected void Get(Uri requestUri)
        {
            OnTaskStarted(_client.GetAsync(requestUri, CancellationToken));
        }

        protected void Put(Uri requestUri, HttpContent content)
        {
            OnTaskStarted(_client.PutAsync(requestUri, content, CancellationToken));
        }

        protected void Post(Uri requestUri, HttpContent content)
        {
            OnTaskStarted(_client.PostAsync(requestUri, content, CancellationToken));
        }

        protected void Delete(Uri requestUri)
        {
            OnTaskStarted(_client.DeleteAsync(requestUri, CancellationToken));
        }

        protected void OnTaskStarted(Task<HttpResponseMessage> task)
        {
            lock (_taskLock)
            {
                _task = task;
            }

            task.GetAwaiter().OnCompleted(OnTaskFinished);
        }

        private void OnTaskFinished()
        {
            lock (_taskLock)
            {
                NotifyTaskFinished(ProcessHttpResult(_task.Result));
            }
        }
    }
}
