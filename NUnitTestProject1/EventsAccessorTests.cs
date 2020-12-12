using EventTracker.Accessors;
using EventTracker.Models;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NUnitTests
{
    public class EventsAccessorTests
    {
        private readonly IEventsAccessor _eventsAccessor = new EventsAccessor();

        [Test]
        public void GetAllEvents_ShouldReturnListOfEventsForGivenUserName()
        {
            //Arrange
            string userName = "test_userName";

            //Act
            IQueryable<Event> evnts = _eventsAccessor.GetAllEvents(userName);
            int count = evnts.Count();

            //Assert
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void FindEvent_WithEventNotExisting_ShouldReturnNull()
        {
            //Arrange
            int testid = 99999;

            //Act
            Event shouldBeNull = _eventsAccessor.FindEvent(testid);

            //Assert
            Assert.That(shouldBeNull, Is.Null);
        }

        [Test]
        public void FindEvent_WithEventExisting_ShouldReturnEvent()
        {
            //Arrange
            int id = 1;
            DateTime dateToCheck = new DateTime(2020, 12, 6);
            int daysPassedToCheck = (DateTime.Now.Date - dateToCheck.Date).Days;

            //Act
            Event shouldExist = _eventsAccessor.FindEvent(id);

            //Assert
            Assert.That(shouldExist.Id, Is.EqualTo(1));
            Assert.That(shouldExist.DateCreated, Is.EqualTo(dateToCheck));
            Assert.That(shouldExist.DaysPassed, Is.EqualTo(daysPassedToCheck));
            Assert.That(shouldExist.Description, Is.EqualTo("I've worked on this project"));
            Assert.That(shouldExist.UserName, Is.EqualTo("test_userName"));
        }

        [Test]
        public void InsertEvent_WithGivenEvent_ShouldBeAbleToFindEvent()
        {
            //Arrange
            Event eventToBeInserted = new Event()
            {
                DateCreated = new DateTime(2020, 12, 6),
                Description = "I've used the bathroom",
                UserName = "test_userName"
            };

            //Act
            _eventsAccessor.InsertEvent(eventToBeInserted);
            Event foundEvent = this.FindEventWithDescription(eventToBeInserted.Description);

            //Assert
            Assert.That(foundEvent.DateCreated, Is.EqualTo(eventToBeInserted.DateCreated));
            Assert.That(foundEvent.Description, Is.EqualTo(eventToBeInserted.Description));
            Assert.That(foundEvent.UserName, Is.EqualTo(eventToBeInserted.UserName));

            //TearDown
            _eventsAccessor.DeleteEvent(foundEvent);
        }

        [Test]
        public void ResetEvent_ShouldResetDateCreatedOfEvent()
        {
            //Arrange
            Event insertedEvent = new Event()
            {
                DateCreated = new DateTime(2020, 12, 6),
                Description = "dateCreated should be reset",
                UserName = "test_userName"
            };
            _eventsAccessor.InsertEvent(insertedEvent);
            Event eventToReset = this.FindEventWithDescription("dateCreated should be reset");

            //Act
            _eventsAccessor.ResetEvent(eventToReset);
            Event resetedEvent = this.FindEventWithDescription("dateCreated should be reset");

            //Assert
            Assert.That(resetedEvent.DateCreated, Is.EqualTo(DateTime.Now.Date));

            //Teardown
            _eventsAccessor.DeleteEvent(resetedEvent);
        }

        [Test]
        public void DeleteEvent_ShouldNotBeAbleToFindEvent()
        {
            //Arrange
            Event insertedEvent = new Event()
            {
                DateCreated = DateTime.Now,
                Description = "Purpose to be deleted",
                UserName = "test_userName"
            };
            _eventsAccessor.InsertEvent(insertedEvent);
            Event eventToDelete = this.FindEventWithDescription(insertedEvent.Description);

            //Act
            _eventsAccessor.DeleteEvent(eventToDelete);
            Event shouldNotExist = _eventsAccessor.FindEvent(eventToDelete.Id);

            //Assert
            Assert.That(shouldNotExist, Is.Null);
        }

        //Helper Methods

        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = NP7iFJ6vOm5Mp131;";

        public Event FindEventWithDescription(string description)
        {
            string query = "SELECT * FROM EventTracker.dbo.Events WHERE description = @description ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 300);

                command.Parameters["@description"].Value = description;

                command.CommandType = CommandType.Text;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                Event existingEvent = new Event();

                if (reader.Read())
                {
                    existingEvent.Id = (int)reader["eventID"];
                    existingEvent.DateCreated = (DateTime)reader["dateCreated"];
                    existingEvent.DaysPassed = (DateTime.Now.Date - existingEvent.DateCreated.Date).Days;
                    existingEvent.Description = reader["description"].ToString();
                    existingEvent.UserName = reader["userName"].ToString();

                    return existingEvent;
                }

                return null;
            }
        }
    }
}
