using EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Engines
{
    public class UsersEngine : IUsersEngine
    {
        public int CheckLogin(string enteredUserName, string enteredPassword)
        {
            throw new NotImplementedException();
        }

        public int CheckRegistration(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
