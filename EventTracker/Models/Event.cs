using System;

namespace EventTracker.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public int NumResets { get; set; }
        public int UserName { get; set; }
    }
}
