using EventTracker.Accessors;
using EventTracker.Models;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

namespace NUnitTests
{
    public class UsersAccessorTests
    {
        private readonly IUsersAccessor _usersAccessor = new UsersAccessor();

        [Test]
        public void FindUser_WithUserExisting_ShouldReturnExistingUser()
        {
            //Arrange
            User foundUser = new User();
            string testUserName = "test_userName";

            //Act
            foundUser = _usersAccessor.FindUser(testUserName);

            //Assert
            Assert.That(foundUser.UserName, Is.EqualTo("test_userName"));
            Assert.That(foundUser.Password, Is.EqualTo("10a6e6cc8311a3e2bcc09bf6c199adecd5dd59408c343e926b129c4914f3cb01"));
        }

        [Test]
        public void FindUser_WithUserNotExisting_ShouldReturnNull()
        {
            //Arrange
            User shouldBeNull = new User();
            string nonExistingUserName = "DoesNotExist96226611";

            //Act
            shouldBeNull = _usersAccessor.FindUser(nonExistingUserName);

            //Assert
            Assert.That(shouldBeNull, Is.Null);
        }

        [Test]
        public void InsertUser_WithNewNonExistingUser_ShouldFindUserInDB()
        {
            //Arrange
            User userToInsert = new User()
            {
                UserName = "newTestUserName",
                Password = "newTestPassword"
            };
            User foundUser = new User();

            //Act
            _usersAccessor.InsertUser(userToInsert);
            foundUser = _usersAccessor.FindUser(userToInsert.UserName);

            //Assert
            Assert.That(foundUser.UserName, Is.EqualTo("newTestUserName"));
            Assert.That(foundUser.Password, Is.EqualTo("e1129e4321dc4dbd4107c58c9b9d083bf63cef947fcc6786e15af61a6540d63b"));

            //Teardown
            this.DeleteUser(userToInsert.UserName);
        }

        [Test]
        public void ComputeSha256Hash_WithGivenPassword_ShouldReturnHashedPassword()
        {
            //Arrange
            string passwordToHash = "testPasswordToHash";
            string returnedHash;

            //Act
            returnedHash = _usersAccessor.ComputeSha256Hash(passwordToHash);

            //Assert
            Assert.That("0704d3c410ab00ce1dab5e1626c2dd1a32cb47a396d299749aa6713aa601b0f6", Is.EqualTo(returnedHash));
        }

        //Helper Methods

        public readonly string connectionString = "Data Source = 34.121.98.190; Initial Catalog = EventTracker; User ID = sqlserver; Password = NP7iFJ6vOm5Mp131;";

        public void DeleteUser(string userName)
        {
            string sql = "DELETE FROM EventTracker.dbo.Users WHERE userName = @userName ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@userName", System.Data.SqlDbType.NVarChar, 20);

                command.Parameters["@userName"].Value = userName;

                command.CommandType = CommandType.Text;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}