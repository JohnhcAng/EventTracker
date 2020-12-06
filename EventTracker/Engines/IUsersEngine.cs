using EventTracker.Models;

namespace EventTracker.Engines
{
    public interface IUsersEngine
    {
        string CheckLogin(string enteredUserName, string enteredPassword);
        string CheckRegistration(User newUser);
    }
}
