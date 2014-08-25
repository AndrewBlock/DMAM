using System.Collections.Generic;
using System.Windows.Threading;

namespace DMAM.Core.Events
{
    public class Event<T> where T : EventData
    {
        private readonly object _invocationLock = new object();
        private readonly Dictionary<EventHandler<T>, Dispatcher> _handlers
            = new Dictionary<EventHandler<T>, Dispatcher>();
        
        public void Notify(T eventData)
        {
            Dictionary<EventHandler<T>, Dispatcher> handlers;

            lock (_handlers)
            {
                handlers = new Dictionary<EventHandler<T>, Dispatcher>(_handlers);
            }

            foreach (var handler in handlers.Keys)
            {
                handlers[handler].BeginInvoke(handler,
                    new object[] { eventData.Clone() });
            }
        }
        
        public void Subscribe(EventHandler<T> handler)
        {
            lock (_invocationLock)
            {
                if (_handlers.ContainsKey(handler))
                {
                    return;
                }

                _handlers.Add(handler, Dispatcher.CurrentDispatcher);
            }
        }

        public void Unsubscribe(EventHandler<T> handler)
        {
            lock (_invocationLock)
            {
                if (!_handlers.ContainsKey(handler))
                {
                    return;
                }

                _handlers.Remove(handler);
            }
        }
    }
}
