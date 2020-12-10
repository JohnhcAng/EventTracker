using System;

namespace EventTracker.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int DaysPassed { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
