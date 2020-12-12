using EventTracker.Accessors;
using EventTracker.Models;
using System;
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

            foreach (Event e in events)
            {
                e.DaysPassed = (DateTime.Now.Date - e.DateCreated.Date).Days;
            }

            return evnts.AsQueryable();
        }

        public Event FindEvent(int id)
        {
            Event foundEvent = events.FirstOrDefault(e => e.Id == id);
            if (foundEvent != null)
            {
                foundEvent.DaysPassed = (DateTime.Now.Date - foundEvent.DateCreated.Date).Days;
            }
            return foundEvent;
        }

        public void InsertEvent(Event evnt)
        {
            events.Add(evnt);
        }

        public void ResetEvent(Event evnt)
        {
            Event foundEvent = events.FirstOrDefault(e => e.Id == evnt.Id);
            events.Remove(evnt);
            foundEvent.DateCreated = DateTime.Now;
            events.Add(foundEvent);
        }

        public void DeleteEvent(Event evnt)
        {
            events.Remove(evnt);
        }
    }
}
