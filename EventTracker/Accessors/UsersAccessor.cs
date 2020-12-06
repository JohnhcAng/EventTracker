using EventTracker.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EventTracker.Accessors
{
    public class UsersAccessor : IUsersAccessor
    {
        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = ;";

        public User Find(string enteredUserName)
        {
            string sql = "SELECT * FROM EventTracker.dbo.Users WHERE userName = @userName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@userName", System.Data.SqlDbType.NVarChar, 20);

                command.Parameters["@userName"].Value = enteredUserName;

                command.CommandType = CommandType.Text;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                User existingUser = new User();

                if (reader.Read())
                {
                    existingUser.UserName = reader["userName"].ToString();
                    existingUser.Password = reader["password"].ToString();

                    return existingUser;
                }

                return null;
            }
        }

        public void Insert(User newUser)
        {
            throw new NotImplementedException();
        }

        public string ComputeSha256Hash(string password)
        {
            throw new NotImplementedException();
        }
    }
}
