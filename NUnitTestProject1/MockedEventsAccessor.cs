using EventTracker.Accessors;
using EventTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTests
{
    public class MockedEventsAccessor : IEventsAccessor
    {
        public List<Event> events = new List<Event>();

        public IQueryable<Event> GetAllEvents(string userName)
        {
            List<Event> evnts = new List<Event>();

            foreach (Event e in events)
            {
                if (e.UserName == userName)
                {
                    evnts.Add(e);
                }
            }
            return evnts.AsQueryable();
        }

        public Event FindEvent(int id)
        {
            Event foundEvent = events.FirstOrDefault(e => e.Id == id);
            return foundEvent;
        }

        public void InsertEvent(Event evnt)
        {
            events.Add(evnt);
        }

        public void IncrementEvent(Event evnt)
        {
            Event evntToIncrement = events.Find(e => e.Id == evnt.Id);
            evntToIncrement.NumOccurences += 1;
            events.Remove(evnt);
            events.Add(evntToIncrement);
        }

        public void DeleteEvent(Event evnt)
        {
            events.Remove(evnt);
        }
    }
}
