﻿using EventTracker.Models;
using System.Linq;

namespace EventTracker.Accessors
{
    public interface IEventsAccessor
    {
        IQueryable<Event> GetAllEvents(string userName);
        Event FindEvent(int id);
        void InsertEvent(Event evnt);
        void ResetEvent(Event evnt);
        void DeleteEvent(Event evnt);
    }
}
