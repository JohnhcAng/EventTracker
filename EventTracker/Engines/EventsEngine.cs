using EventTracker.Accessors;
using EventTracker.Models;

namespace EventTracker.Engines
{
    public class EventsEngine : IEventsEngine
    {
        private readonly IEventsAccessor _eventsAccessor = new EventsAccessor();

        public EventsEngine()
        {
        }

        public EventsEngine(IEventsAccessor eventsAccessor)
        {
            _eventsAccessor = eventsAccessor;
        }

        public void AddEvent(Event evnt)
        {
            _eventsAccessor.InsertEvent(evnt);
        }

        public void DeleteEvent(Event evnt)
        {
            _eventsAccessor.DeleteEvent(evnt);
        }
    }
}
