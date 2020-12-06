using EventTracker.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace EventTracker.Accessors
{
    public class UsersAccessor : IUsersAccessor
    {
        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = NP7iFJ6vOm5Mp131;";

        public User FindUser(string enteredUserName)
        {
            string sql = "SELECT * FROM EventTracker.dbo.Users WHERE userName = @userName ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@userName", System.Data.SqlDbType.NVarChar, 30);

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

        public void InsertUser(User newUser)
        {
            string sql = "INSERT INTO EventTracker.dbo.Users VALUES ( @userName , @password )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@userName", System.Data.SqlDbType.NVarChar, 30);
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 64);

                command.Parameters["@userName"].Value = newUser.UserName;
                command.Parameters["@password"].Value = ComputeSha256Hash(newUser.Password);

                command.CommandType = CommandType.Text;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
