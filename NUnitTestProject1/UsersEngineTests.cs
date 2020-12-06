using EventTracker.Engines;
using EventTracker.Models;
using NUnit.Framework;

namespace NUnitTests
{
    public class UsersEngineTests
    {
        private readonly IUsersEngine _usersEngine;
        private readonly MockedUsersAccessor _mockedUsersAccessor;

        public UsersEngineTests()
        {
            _mockedUsersAccessor = new MockedUsersAccessor();
            _usersEngine = new UsersEngine(_mockedUsersAccessor);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckLogin_WithNullCred_ShouldReturnNullCred()
        {
            //Arrange
            string userName = "";
            string password = "";

            //Act
            string result = _usersEngine.CheckLogin(userName, password);

            //Assert
            Assert.That(result, Is.EqualTo("NullCred"));
        }

        [Test]
        public void CheckLogin_WithNoExistingUser_ShouldReturnUserDNE()
        {
            //Arrange
            string enteredUserName = "DoesNotExist6605444";
            string enteredPassword = "testExamplePassword";

            //Act
            string result = _usersEngine.CheckLogin(enteredUserName, enteredPassword);

            //Assert
            Assert.That(result, Is.EqualTo("UserDNE"));
        }

        [Test]
        public void CheckLogin_WithWrongPassword_ShouldReturnPasswordsDoNotMatch()
        {
            //Arrange
            User existingUser = new User()
            {
                UserName = "test_userName",
                Password = "testExamplePassword"
            };
            _mockedUsersAccessor.InsertUser(existingUser);
            string enteredUserName = "test_userName";
            string enteredPassword = "IncorrectPassword";

            //Act
            string result = _usersEngine.CheckLogin(enteredUserName, enteredPassword);

            //Assert
            Assert.That(result, Is.EqualTo("PasswordsDoNotMatch"));

            //Teardown
            _mockedUsersAccessor.DeleteUser(existingUser.UserName);
        }

        [Test]
        public void CheckLogin_WithCorrectCredentials_ShouldReturnSuccessfulLogin3()
        {
            //Arrange
            User existingUser = new User()
            {
                UserName = "testExampleUserName",
                Password = "testExamplePassword"
            };
            _mockedUsersAccessor.InsertUser(existingUser);
            string enteredEmail = "testExampleUserName";
            string enteredPassword = "testExamplePassword";

            //Act
            string result = _usersEngine.CheckLogin(enteredEmail, enteredPassword);

            //Assert
            Assert.That(result, Is.EqualTo("SuccessfulLogin"));

            //Teardown
            _mockedUsersAccessor.DeleteUser(existingUser.UserName);
        }

        [Test]
        public void CheckRegistration_WithNullCred_ShouldReturnNullCred()
        {
            //Arrange
            User nullUser = new User()
            {
                UserName = "",
                Password = ""
            };

            //Act
            string result = _usersEngine.CheckRegistration(nullUser);

            //Assert
            Assert.That(result, Is.EqualTo("NullCred"));
        }

        [Test]
        public void CheckRegistration_WithUserNameTooLong_ShouldReturnUserNameTooLong()
        {
            //Arrange
            User newUser = new User()
            {
                UserName = "0123456789012345678901234567890", //31 characters
                Password = "testExamplePassword"
            };

            //Act
            string result = _usersEngine.CheckRegistration(newUser);

            //Assert
            Assert.That(result, Is.EqualTo("UserNameTooLong"));
        }

        [Test]
        public void CheckRegistration_WithUserAlreadyExisting_ShouldReturnUserAlreadyExists()
        {
            //Arrange
            User existingUser = new User()
            {
                UserName = "testExampleUserName",
                Password = "testExamplePassword"
            };
            _mockedUsersAccessor.InsertUser(existingUser);

            //Act
            string result = _usersEngine.CheckRegistration(existingUser);

            //Assert
            Assert.That(result, Is.EqualTo("UserAlreadyExists"));

            //Teardown
            _mockedUsersAccessor.DeleteUser(existingUser.UserName);
        }

        [Test]
        public void CheckRegistration_WithSuccessfulRegistration_ShouldReturnSuccessfulRegistration()
        {
            //Arrange
            User newUser = new User()
            {
                UserName = "testExampleUserName",
                Password = "testExamplePassword"
            };

            //Act
            string result = _usersEngine.CheckRegistration(newUser);

            //Assert
            Assert.That(result, Is.EqualTo("SuccessfulRegistration"));

            //Teardown
            _mockedUsersAccessor.DeleteUser(newUser.UserName);
        }
    }
}
