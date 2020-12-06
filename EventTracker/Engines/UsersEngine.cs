using EventTracker.Accessors;
using EventTracker.Models;

namespace EventTracker.Engines
{
    public class UsersEngine : IUsersEngine
    {
        private readonly IUsersAccessor _usersAccessor = new UsersAccessor();

        public UsersEngine()
        {
        }

        public UsersEngine(IUsersAccessor usersAccessor)
        {
            _usersAccessor = usersAccessor;
        }

        public string NullCred = "NullCred";
        public string UserDNE = "UserDNE";
        public string PasswordsDoNotMatch = "PasswordsDoNotMatch";
        public string UserAlreadyExists = "UserAlreadyExists";
        public string UserNameTooLong = "UserNameTooLong";
        public string SuccessfulLogin = "SuccessfulLogin";
        public string SuccessfulRegistration = "SuccessfulRegistration";

        public int UserNameMaxLength = 30;

        public string CheckLogin(string enteredUserName, string enteredPassword)
        {
            if (string.IsNullOrEmpty(enteredUserName) || string.IsNullOrEmpty(enteredPassword))
            {
                return NullCred;
            }
            else
            {
                User existingUser = _usersAccessor.FindUser(enteredUserName);

                string hashedPassword = _usersAccessor.ComputeSha256Hash(enteredPassword);

                if (existingUser == null)
                {
                    return UserDNE;
                }
                else if (hashedPassword != existingUser.Password)
                {
                    return PasswordsDoNotMatch;
                }
                else
                {
                    return SuccessfulLogin;
                }
            }
        }

        public string CheckRegistration(User newUser)
        {
            if (string.IsNullOrEmpty(newUser.UserName) || string.IsNullOrEmpty(newUser.Password))
            {
                return NullCred;
            }
            else if (newUser.UserName.Length > UserNameMaxLength)
            {
                return UserNameTooLong;
            }
            else
            {
                User userAlreadyExists = _usersAccessor.FindUser(newUser.UserName);

                if (userAlreadyExists != null)
                {
                    return UserAlreadyExists;
                }
                else
                {
                    _usersAccessor.InsertUser(newUser);

                    return SuccessfulRegistration;
                }
            }
        }
    }
}
