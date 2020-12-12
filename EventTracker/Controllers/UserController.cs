using EventTracker.Engines;
using EventTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersEngine _usersEngine = new UsersEngine();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string Password)
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
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Response.Cookies.Append("currentUser", userName, cookieOptions);
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
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
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Response.Cookies.Append("currentUser", newUser.UserName, cookieOptions);
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("currentUser");
            return RedirectToAction("Login");
        }
    }
}
