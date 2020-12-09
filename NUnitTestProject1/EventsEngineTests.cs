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
                NumOccurences = 1,
                UserName = "test_userName"
            };

            //Act
            _eventsEngine.AddEvent(eventToBeAdded);
            Event foundEvent = _mockedEventsAccessor.FindEvent(eventToBeAdded.Id);

            //Assert
            Assert.That(foundEvent.Id, Is.EqualTo(eventToBeAdded.Id));
            Assert.That(foundEvent.DateCreated, Is.EqualTo(eventToBeAdded.DateCreated));
            Assert.That(foundEvent.Description, Is.EqualTo(eventToBeAdded.Description));
            Assert.That(foundEvent.NumOccurences, Is.EqualTo(eventToBeAdded.NumOccurences));
            Assert.That(foundEvent.UserName, Is.EqualTo(eventToBeAdded.UserName));

            //Teardown
            _mockedEventsAccessor.DeleteEvent(foundEvent);
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
                NumOccurences = 1,
                UserName = "test_userName"
            };
            _eventsEngine.AddEvent(eventToBeDeleted);

            //Act
            _eventsEngine.DeleteEvent(eventToBeDeleted);
            Event shouldbeNull = _mockedEventsAccessor.FindEvent(eventToBeDeleted.Id);

            //Assert
            Assert.That(shouldbeNull, Is.Null);
        }

        [Test]
        public void IncrementEvent_NumOccurencesOfEventShouldBeIncremented()
        {
            //Arrange
            Event eventToBeIncremented = new Event()
            {
                Id = 1,
                DateCreated = new DateTime(2020, 12, 6),
                Description = "Test Description",
                NumOccurences = 1,
                UserName = "test_userName"
            };
            _eventsEngine.AddEvent(eventToBeIncremented);

            //Act
            _eventsEngine.IncrementEvent(eventToBeIncremented);
            Event incrementedEvent = _mockedEventsAccessor.FindEvent(eventToBeIncremented.Id);

            //Assert
            Assert.That(incrementedEvent.NumOccurences, Is.EqualTo(2));

            //Teardown
            _mockedEventsAccessor.DeleteEvent(eventToBeIncremented);
        }
    }
}
