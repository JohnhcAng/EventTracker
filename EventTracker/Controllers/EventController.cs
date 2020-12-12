using EventTracker.Engines;
using EventTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventTracker.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventsEngine _eventsEngine = new EventsEngine();

        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(Event evnt)
        {
            evnt.UserName = Request.Cookies["currentUser"];
            evnt.DateCreated = DateTime.Now;
            _eventsEngine.AddEvent(evnt);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteEvent(Event evnt)
        {
            _eventsEngine.DeleteEvent(evnt);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ResetEvent(Event evnt)
        {
            _eventsEngine.ResetEvent(evnt);
            return RedirectToAction("Index", "Home");
        }
    }
}
