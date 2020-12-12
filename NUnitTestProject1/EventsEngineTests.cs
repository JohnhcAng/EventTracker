using EventTracker.Engines;
using EventTracker.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests
{
    public class EventsEngineTests
    {
        private readonly IEventsEngine _eventsEngine;
        private readonly MockedEventsAccessor _mockedEventsAccessor;

        public EventsEngineTests()
        {
            _mockedEventsAccessor = new MockedEventsAccessor();
            _eventsEngine = new EventsEngine(_mockedEventsAccessor);
        }

        [Test]
        public void AddEvent_ShouldBeAbleToFindEvent()
        {
            //Arrange
            Event eventToBeAdded = new Event()
            {
                Id = 1,
                DateCreated = new DateTime(2020, 12, 6),
                Description = "Test Description",
                UserName = "test_userName"
            };
            int daysPassedToCheck = (DateTime.Now.Date - eventToBeAdded.DateCreated.Date).Days;

            //Act
            _eventsEngine.AddEvent(eventToBeAdded);
            Event foundEvent = _mockedEventsAccessor.FindEvent(eventToBeAdded.Id);

            //Assert
            Assert.That(foundEvent.Id, Is.EqualTo(eventToBeAdded.Id));
            Assert.That(foundEvent.DateCreated, Is.EqualTo(eventToBeAdded.DateCreated));
            Assert.That(foundEvent.DaysPassed, Is.EqualTo(daysPassedToCheck));
            Assert.That(foundEvent.Description, Is.EqualTo(eventToBeAdded.Description));
            Assert.That(foundEvent.UserName, Is.EqualTo(eventToBeAdded.UserName));

            //Teardown
            _mockedEventsAccessor.DeleteEvent(foundEvent);
        }

        [Test]
        public void ResetEvent_ShouldResetDateCreatedOfEvent() 
        {
            //Arrange
            Event insertedEvent = new Event()
            {
                Id = 1,
                DateCreated = new DateTime(2020, 12, 6),
                Description = "dateCreated should be reset",
                UserName = "test_userName"
            };
            _eventsEngine.AddEvent(insertedEvent);

            //Act
            _eventsEngine.ResetEvent(insertedEvent);
            Event resetedEvent = _mockedEventsAccessor.FindEvent(insertedEvent.Id);

            //Assert
            Assert.That(resetedEvent.DateCreated.Date, Is.EqualTo(DateTime.Now.Date));

            //Teardown
            _mockedEventsAccessor.DeleteEvent(resetedEvent);
        }

        [Test]
        public void DeleteEvent_ShouldNotBeAbleToFindEvent()
        {
            //Arrange
            Event eventToBeDeleted = new Event()
            {
                Id = 1,
                DateCreated = new DateTime(2020, 12, 6),
                Description = "Test Description",
                UserName = "test_userName"
            };
            _eventsEngine.AddEvent(eventToBeDeleted);

            //Act
            _eventsEngine.DeleteEvent(eventToBeDeleted);
            Event shouldbeNull = _mockedEventsAccessor.FindEvent(eventToBeDeleted.Id);

            //Assert
            Assert.That(shouldbeNull, Is.Null);
        }
    }
}
