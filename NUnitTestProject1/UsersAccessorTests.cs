using EventTracker.Accessors;
using EventTracker.Models;
using NUnit.Framework;

namespace NUnitTests
{
    public class UsersAccessorTests
    {
        private readonly IUsersAccessor _usersAccessor = new UsersAccessor();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Find_WithUserExisting_ShouldReturnExistingUser()
        {
            //Arrange
            User foundUser = new User();
            string testUserName = "test_userName";

            //Act
            foundUser = _usersAccessor.Find(testUserName);

            //Assert
            Assert.AreEqual("test_userName", foundUser.UserName);
            Assert.AreEqual("test_password", foundUser.Password);
        }
    }
}