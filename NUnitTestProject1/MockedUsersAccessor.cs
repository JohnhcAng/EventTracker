using EventTracker.Accessors;
using EventTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NUnitTests
{
    public class MockedUsersAccessor : IUsersAccessor
    {
        public List<User> users = new List<User>();

        public User FindUser(string enteredUserName)
        {
            var foundUser = users.FirstOrDefault(u => u.UserName == enteredUserName);

            if (foundUser == null)
            {
                return null;
            }
            else
            {
                return foundUser;
            }
        }

        public void InsertUser(User newUser)
        {
            var existingUser = users.FirstOrDefault(u => u.UserName == newUser.UserName);

            if (existingUser == null)
            {
                newUser.Password = ComputeSha256Hash(newUser.Password);

                users.Add(newUser);
            }
        }

        public void DeleteUser(string userName)
        {
            users.RemoveAll(u => u.UserName == userName);
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
