using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace DMAM.Core.Services
{
    public abstract class UIThreadBase
    {
        private readonly string _threadName;
        
        private Thread _thread;
        private object _threadLock = new object();

        private Dispatcher _threadDispatcher;
        private EventWaitHandle _threadStartEvent
            = new EventWaitHandle(false, EventResetMode.ManualReset);
        private bool _threadStartResult = false;

        protected UIThreadBase(string threadName)
        {
            _threadName = threadName;
        }

        protected bool IsThreadRunning
        {
            get
            {
                lock (_threadLock)
                {
                    return (_thread != null);
                }
            }
        }

        protected Dispatcher ThreadDispatcher
        {
            get
            {
                lock (_threadLock)
                {
                    return _threadDispatcher;
                }
            }
        }

        public bool Initialize()
        {
            lock (_threadLock)
            {
                if (_thread != null)
                {
                    return true;
                }

                _threadDispatcher = null;
                _threadStartResult = false;
                _threadStartEvent.Reset();

                _thread = new Thread(ThreadProc);
                _thread.Name = _threadName;
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Start();

                _threadStartEvent.WaitOne();

                return _threadStartResult;
            }
        }

        public void Shutdown()
        {
            Thread thread;
            Dispatcher threadDispatcher;

            lock (_threadLock)
            {
                if (_thread == null)
                {
                    return;
                }
                
                thread = _thread;
                threadDispatcher = _threadDispatcher;
            }

            threadDispatcher.InvokeShutdown();
            thread.Join();
        }

        protected abstract bool OnInitialize();
        protected abstract void OnShutdown();

        private void ThreadProc()
        {
            _threadDispatcher = Dispatcher.CurrentDispatcher;
            _threadStartResult = OnInitialize();
            _threadStartEvent.Set();

            lock (_threadLock)
            {
                if (!_threadStartResult)
                {
                    _threadDispatcher = null;
                    _thread = null;
                    return;
                }
            }

            Dispatcher.Run();

            OnShutdown();

            lock (_threadLock)
            {
                _thread = null;
            }
        }
    }
}
