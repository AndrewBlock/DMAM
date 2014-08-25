using System;

namespace DMAM.Core.Services
{
    public abstract class ServiceBase<T>
        where T : ServiceBase<T>, new()
    {
        private static T _instance;
        private static object _instanceLock = new object();

        private bool _isServiceRunning;
        private object _isServiceRunningLock = new object();

        private ServiceTaskQueue _taskQueue = new ServiceTaskQueue();

        public static T GetInstance()
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public bool IsServiceRunning
        {
            get
            {
                lock (_isServiceRunningLock)
                {
                    return _isServiceRunning;
                }
            }
        }

        public bool Initialize()
        {
            lock (_isServiceRunningLock)
            {
                if (_isServiceRunning)
                {
                    return true;
                }

                if (!OnInitialize())
                {
                    return false;
                }

                _isServiceRunning = true;
                return true;
            }
        }

        public void Shutdown()
        {
            lock (_isServiceRunningLock)
            {
                if (!_isServiceRunning)
                {
                    return;
                }

                _taskQueue.CancelAllTasks();
                OnShutdown();

                _isServiceRunning = false;
            }
        }

        protected void QueueTask(IServiceTask task)
        {
            _taskQueue.StartTask(task);
        }

        protected virtual bool OnInitialize()
        {
            return true;
        }

        protected virtual void OnShutdown()
        {
        }
    }
}
