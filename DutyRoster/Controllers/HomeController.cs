using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DutyRoster.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The Roster Project providing a better way of managing your volunteers.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact details for the roster Project";

            return View();
        }
    }
}