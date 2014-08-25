using System;
using System.Threading.Tasks;

namespace DMAM.Core.Services
{
    public abstract class BasicTask<T> : ServiceTask<T>
    {
        private Task _task;
        private readonly object _taskLock = new object();

        protected BasicTask(Action<T> clientNotify, object clientData)
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
                    _task = null;
                }
            }
        }

        public override void Start()
        {
            lock (_taskLock)
            {
                _task = new Task(OnTaskStart);
                _task.Start();
            }
        }

        protected abstract T PerformTask();

        private void OnTaskStart()
        {
            NotifyTaskFinished(PerformTask());
        }
    }
}
