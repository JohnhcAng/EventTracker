using EventTracker.Models;

namespace EventTracker.Engines
{
    public interface IEventsEngine
    {
        void AddEvent(Event evnt);
        void ResetEvent(Event evnt);
        void DeleteEvent(Event evnt);
    }
}
