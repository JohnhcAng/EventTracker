using EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Engines
{
    public interface IEventsEngine
    {
        void AddEvent(Event evnt);
        void DeleteEvent(Event evnt);
    }
}
