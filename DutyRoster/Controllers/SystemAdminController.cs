using AutoMapper;
using DutyRoster.Data;
using DutyRoster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DutyRoster.Controllers
{
    public class SystemAdminController : Controller
    {
        // GET: SystemAdmin
        public ActionResult Index()
        {
            List<ClubModel> model = null;
            using (var context = new RosterContext())
            {
                model = context.Clubs.ProjectToList<ClubModel>();
            }
                return View(model);
        }
    }
}