using EventTracker.Accessors;
using EventTracker.Engines;
using EventTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersEngine _usersEngine = new UsersEngine();
        private readonly IUsersAccessor _usersAccessor = new UsersAccessor();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string Password)
        {
            string loginRequestResult = _usersEngine.CheckLogin(userName, Password);

            switch (loginRequestResult)
            {
                case "NullCred":
                    ViewData["Message"] = "Please enter both Username and Password to Log In.";
                    break;

                case "UserDNE":
                    ViewData["Message"] = "This account does not exist. Please reenter your Username or click Register to create a new account.";
                    break;

                case "PasswordsDoNotMatch":
                    ViewData["Message"] = "The password you entered is incorrect.";
                    break;

                case "SuccessfulLogin":
                    HttpContext.Session.SetString("currentUser", userName);
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User newUser)
        {
            string registerRequestResult = _usersEngine.CheckRegistration(newUser);

            switch (registerRequestResult)
            {
                case "NullCred":
                    ViewData["Message"] = "Please enter both Username and Password to Create your new account.";
                    break;

                case "UserNameTooLong":
                    ViewData["Message"] = "Your Username must be 30 characters long or less.";
                    break;

                case "UserAlreadyExists":
                    ViewData["Message"] = "This Username is already in use, please use another Username.";
                    break;

                case "SuccessfulRegistration":
                    HttpContext.Session.SetString("currentUser", newUser.UserName);
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
