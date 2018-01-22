using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSW.Bookings.Filters;
using SSW.Bookings.Core;
using SSW.Bookings.Models;
using System.Data.Entity;
using WebMatrix.WebData;
using Mvc.Mailer;
using SSW.Bookings.Mailers;


namespace SSW.Bookings.Controllers
{
    [InitializeSimpleMembership]
    public class BoatController : Controller
    {
        //
        // GET: /Boat/
        public ActionResult Index()
        {
            BookingEntities context = new BookingEntities();
            Member mbr = (Member)context.Member.Where(m => m.UserProfileId == WebSecurity.CurrentUserId).First();
            Settings set = context.Settings.Find(1);
            int CancelDays;
            if(!int.TryParse(ConfigurationManager.AppSettings["CancellationDays"], out CancelDays))
            {
                CancelDays = 1;
            }
            BoatBookingModel model = new BoatBookingModel
            {
                MemberName = User.Identity.Name,
                CanBook = mbr.CanBookBoats,
                AdvanceBookingDays = set.AdvanceDaysToBook,
                Title = set.BookingPageHeader,
                Mesage = set.BookingPageMessage,
                CancellationDays = CancelDays
            };
            return View(model);
        }


        public JsonResult GetBoats(string bookingDate)
        {
            BookingEntities context = new BookingEntities();
            DateTime bd = DateTime.Parse(bookingDate);
            List<BoatBookingListMember> bbList = new List<BoatBookingListMember>();
            var boats = context.GetavailableBoats(bd);
            foreach (Boat b in boats)
            {
                bbList.Add(new BoatBookingListMember
                {
                    Id = b.Id,
                    Text = string.Format("{0} {1} {2}", b.Make, b.Class, b.Name)
                });
            }
            return Json(bbList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoatBookings(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            if(fromDate.Date < DateTime.Now.Date)
            {
                fromDate = DateTime.Now.Date;
            }
            BookingEntities context = new BookingEntities();
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

        public bool BookBoat(string bookingDate, string boatId)
        {
            BoatBooking b = null;
            BookingEntities ctx = null; ;
            try
            {
                ctx = new BookingEntities();
                int bId = int.Parse(boatId);
                Member booker = (Member)ctx.Member.Where(m => m.UserProfileId == WebSecurity.CurrentUserId).First();
                Boat boat = (Boat)ctx.Boat.Find(bId);
                DateTime bd = DateTime.Parse(bookingDate);
                decimal cost = booker.Age < 18 ? 0.0m : boat.RentalCost;
                b = new BoatBooking
                    {
                        BookingDate = bd,
                        BoatId = boat.Id,
                        BookingMemberId = booker.Id,
                        Units = 1,
                        UnitCost = cost,
                        TotalCost = cost
                    };
                ctx.BoatBooking.Add(b);
                ctx.SaveChanges();
                b = ctx.BoatBooking.Where((bb)=> bb.BookingMemberId == b.BookingMemberId && bb.BookingDate == b.BookingDate && bb.BoatId == b.BoatId).First();
                BoatBookedModel mailModel = new BoatBookedModel
                {
                    BoatName = boat.Make + " " + boat.Class + " " + boat.SailNumber,
                    BookingDate = bd,
                    Cost = cost,
                    UserEmail = booker.Email,
                    UserName = booker.Name,
                    SailingClub = ConfigurationManager.AppSettings["SailingClub"],
                    Signature = ConfigurationManager.AppSettings["Signature"],
                    BookingReference = b.BookingMemberId.ToString() + "/" + b.Id.ToString()
                };
                UserMailer um = new UserMailer();
                MvcMailMessage msgUser = um.BoatBookedUser(mailModel);
                MvcMailMessage msgTreasurer = um.BoatBookedTreasurer(mailModel);
                msgUser.Send();
                msgTreasurer.Send();
                return true;
            }
            catch
            {
                if(b != null && b.Id > 0)
                {
                    if(ctx != null)
                    {
                        ctx.BoatBooking.Remove(b);
                        ctx.SaveChanges();
                    }
                }
                return false;
            }
            finally
            {
                if (ctx != null)
                    ctx.Dispose();
            }
        }

        public bool DeleteBooking(string bookingId)
        {
            try
            {
                int bId = int.Parse(bookingId);
                BookingEntities ctx = new BookingEntities();
                BoatBooking b = ctx.BoatBooking.Where(x => x.Id == bId).First();
                BookingCancelledModel cm = new BookingCancelledModel
                {
                    UserEmail = b.Member.Email,
                    UserName = b.Member.Name,
                    SailingClub = ConfigurationManager.AppSettings["SailingClub"],
                    Signature = ConfigurationManager.AppSettings["Signature"],
                    BookingReference = b.BookingMemberId.ToString() + "/" + b.Id.ToString(),
                    BookingDate = b.BookingDate
                };
                ctx.BoatBooking.Remove(b);
                ctx.SaveChanges();
                UserMailer um = new UserMailer();
                MvcMailMessage msg = um.BoatCancelledTreasurer(cm);
                msg.Send();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public ActionResult Administrators()
        {
            BookingEntities context = new BookingEntities();
            List<AdministratorModel> model = new List<AdministratorModel>();
            var admins = context.GetAdministrators();
            foreach(var a in admins)
            {
                model.Add(new AdministratorModel
                {
                    Name = a.Name,
                    Email = a.Email
                });
            }
            return View(model);

        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}