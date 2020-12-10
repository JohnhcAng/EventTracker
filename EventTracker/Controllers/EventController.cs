using EventTracker.Engines;
using EventTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventsEngine _eventsEngine = new EventsEngine();

        public ActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(Event evnt)
        {
            evnt.UserName = HttpContext.Session.GetString("currentUser"); //This doesnt work. Maybe IdleTimeout in Startup is the reason?
            evnt.DateCreated = DateTime.Now;
            _eventsEngine.AddEvent(evnt);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(Event evnt)
        {
            _eventsEngine.DeleteEvent(evnt);
            return RedirectToAction("Index", "Home");
        }
    }
}
