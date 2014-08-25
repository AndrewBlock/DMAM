using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DMAM.Core.Services
{
    public abstract class ServiceTask<T> : IServiceTask
    {
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        
        private readonly Dispatcher _dispatcher;
        private readonly Action<T> _clientNotify;
        private readonly object _clientData;

        protected ServiceTask(Action<T> clientNotify, object clientData)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _clientNotify = clientNotify;
            _clientData = clientData;
        }

        public event Action<IServiceTask> TaskFinished;

        public abstract TaskStatus TaskStatus { get; }

        protected CancellationToken CancellationToken
        {
            get
            {
                return _cancelTokenSource.Token;
            }
        }

        protected object ClientData
        {
            get
            {
                return _clientData;
            }
        }

        public void Dispose()
        {
            OnDispose();
        }

        public abstract void Start();

        public void Cancel()
        {
            _cancelTokenSource.Cancel();
        }
        
        protected abstract void OnDispose();

        protected void NotifyTaskFinished(T notifyData)
        {
            if (_clientNotify != null)
            {
                _dispatcher.BeginInvoke(_clientNotify, new object[] { notifyData });
            }

            if (TaskFinished != null)
            {
                TaskFinished(this);
            }
        }
    }
}
