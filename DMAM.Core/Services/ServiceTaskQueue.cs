using System.Collections.Generic;
using System.Threading;

namespace DMAM.Core.Services
{
    internal class ServiceTaskQueue
    {
        private readonly List<IServiceTask> _tasks = new List<IServiceTask>();
        private bool _cancellationInProgress;
        private readonly EventWaitHandle _tasksClearedEvent
            = new EventWaitHandle(true, EventResetMode.ManualReset);

        public bool CancellationInProgress
        {
            get
            {
                lock (_tasks)
                {
                    return _cancellationInProgress;
                }
            }
        }

        public int TaskCount
        {
            get
            {
                lock (_tasks)
                {
                    return _tasks.Count;
                }
            }
        }

        public void StartTask(IServiceTask task)
        {
            lock (_tasks)
            {
                if (_tasks.Contains(task))
                {
                    return;
                }

                if (_cancellationInProgress)
                {
                    task.Dispose();
                    return;
                }

                _tasks.Add(task);

                if (_tasks.Count == 1)
                {
                    _tasksClearedEvent.Reset();
                }
            }

            task.TaskFinished += task_TaskFinished;
            task.Start();
        }

        public void CancelAllTasks()
        {
            List<IServiceTask> tasks;

            lock (_tasks)
            {
                tasks = new List<IServiceTask>(_tasks);
                _cancellationInProgress = true;
            }

            foreach (var task in tasks)
            {
                task.Cancel();
            }

            _tasksClearedEvent.WaitOne();

            lock (_tasks)
            {
                _cancellationInProgress = false;
            }
        }

        private void task_TaskFinished(IServiceTask task)
        {
            lock (_tasks)
            {
                if (!_tasks.Contains(task))
                {
                    return;
                }

                _tasks.Remove(task);
                if (_tasks.Count == 0)
                {
                    _tasksClearedEvent.Set();
                }
            }

            task.TaskFinished -= task_TaskFinished;
            task.Dispose();
        }
    }
}
