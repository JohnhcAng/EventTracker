using EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventTracker.Accessors
{
    public class EventsAccessor : IEventsAccessor
    {
        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = NP7iFJ6vOm5Mp131;";

        public IQueryable<Event> GetAllEvents(string userName)
        {
            throw new NotImplementedException();
        }

        public Event FindEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertEvent(Event evnt)
        {
            throw new NotImplementedException();
        }

        public void ResetEvent(Event evnt)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(Event evnt)
        {
            throw new NotImplementedException();
        }
    }
}
