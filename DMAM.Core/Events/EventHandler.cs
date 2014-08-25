namespace DMAM.Core.Events
{
    public delegate void EventHandler<T>(T eventData) where T : EventData;
}
