using System;
using System.Threading.Tasks;

namespace DMAM.Core.Services
{
    public interface IServiceTask : IDisposable
    {
        event Action<IServiceTask> TaskFinished;

        TaskStatus TaskStatus { get; }

        void Start();
        void Cancel();
    }
}
