using EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EventTracker.Accessors
{
    public class EventsAccessor : IEventsAccessor
    {
        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = NP7iFJ6vOm5Mp131;";

        public IQueryable<Event> GetAllEvents(string userName)
        {
            string query = "SELECT * FROM EventTracker.dbo.Events WHERE userName = @userName ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@userName", SqlDbType.VarChar, 30);

                command.Parameters["@userName"].Value = userName;

                command.CommandType = CommandType.Text;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Event> evnts = new List<Event>();

                while (reader.Read())
                {
                    Event existingEvent = new Event
                    {
                        Id = (int)reader["eventId"],
                        DateCreated = (DateTime)reader["dateCreated"],
                        Description = reader["description"].ToString(),
                        UserName = reader["userName"].ToString()
                    };

                    evnts.Add(existingEvent);
                }

                foreach (Event evnt in evnts)
                {
                    evnt.DaysPassed = (DateTime.Now.Date - evnt.DateCreated.Date).Days;
                }

                return evnts.AsQueryable();
            }
        }

        public Event FindEvent(int id)
        {
            string query = "SELECT * FROM EventTracker.dbo.Events WHERE eventId = @eventId ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@eventId", SqlDbType.Int);

                command.Parameters["@eventId"].Value = id;

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

        public void InsertEvent(Event evnt)
        {
            string query = "INSERT INTO EventTracker.dbo.Events VALUES ( @dateCreated , @description , @userName )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@dateCreated", SqlDbType.Date);
                command.Parameters.Add("@description", SqlDbType.VarChar, 300);
                command.Parameters.Add("@userName", SqlDbType.VarChar, 30);

                command.Parameters["@dateCreated"].Value = evnt.DateCreated;
                command.Parameters["@description"].Value = evnt.Description;
                command.Parameters["@userName"].Value = evnt.UserName;

                command.CommandType = CommandType.Text;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void DeleteEvent(Event evnt)
        {
            string query = "DELETE FROM EventTracker.dbo.Events WHERE eventID = @eventID ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@eventID", System.Data.SqlDbType.Int);

                command.Parameters["@eventID"].Value = evnt.Id;

                command.CommandType = CommandType.Text;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
