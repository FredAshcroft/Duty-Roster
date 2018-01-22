using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DutyRoster.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            Rostercontext context = new DutyEntities();
            Member mbr = (Member)context.Member.Where(m => m.UserProfileId == WebSecurity.CurrentUserId).First();
            Settings set = context.Settings.Find(1);
            BoatBookingModel model = new BoatBookingModel
            {
                MemberName = User.Identity.Name,
                CanBook = mbr.CanBookBoats,
                AdvanceBookingDays = set.AdvanceDaysToBook,
                Title = set.BookingPageHeader,
                Mesage = set.BookingPageMessage,
            };
            return View(model);
        }

        public JsonResult GetBoatBookings(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            if (fromDate.Date < DateTime.Now.Date)
            {
                fromDate = DateTime.Now.Date;
            }
            Rostercontext context = new DutyEntities();
            var bookings = context.GetBoatBookings(fromDate, toDate);
            var eventList = from e in bookings
                            select new
                            {
                                id = e.Id,
                                title = e.boat + " Booked",
                                description = "Booked By: " + e.MemberName,
                                start = e.BookingDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                                //end = e.BookingDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                                allDay = true,
                                member = e.MemberName,
                                backgroundColor = e.MemberName.ToLower() == WebSecurity.CurrentUserName.ToLower() ? "Green" : "Red"
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getcalendarevents(double start, double end)
            {
                var fromDate = ConvertFromUnixTimestamp(start);
                var toDate = ConvertFromUnixTimestamp(end);
                if (fromDate.Date < DateTime.Now.Date)
                {
                    fromDate = DateTime.Now.Date;
                }
                BookingEntities context = new BookingEntities();
                var bookings = context.GetBoatBookings(fromDate, toDate);
                var eventList = from e in bookings
                                select new
                                {
                                    id = e.Id,
                                    title = e.Duty + " Booked",
                                    description = "Booked By: " + e.MemberName,
                                    start = e.BookingDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                                    //end = e.BookingDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                                    allDay = true,
                                    member = e.MemberName,
                                    backgroundColor = e.MemberName.ToLower() == WebSecurity.CurrentUserName.ToLower() ? "Green" : "Red"
                                };
                var rows = eventList.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);
            }

            private static DateTime ConvertFromUnixTimestamp(double timestamp)
            {
                var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(timestamp);
            }
        }


    } }