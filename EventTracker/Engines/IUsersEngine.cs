using EventTracker.Models;

namespace EventTracker.Engines
{
    interface IUsersEngine
    {
        int CheckLogin(string enteredUserName, string enteredPassword);
        int CheckRegistration(User newUser);
    }
}
