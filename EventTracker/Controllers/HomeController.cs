using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventTracker.Models;
using Microsoft.AspNetCore.Http;
using EventTracker.Accessors;

namespace EventTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventsAccessor _eventsAccessor = new EventsAccessor();

        public IActionResult Index()
        {
            string user = Request.Cookies["currentUser"];
            return View(_eventsAccessor.GetAllEvents(user));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
