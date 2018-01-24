using DutyRoster.Data;
using DutyRoster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace DutyRoster.Controllers
{
    [Route("calendar")]
    public class CalendarController : Controller
    {
        [Route("GetDuties")]      
        public JsonResult GetDuties(string start, string end)
        {
            DateTime startDate =  Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            using (var context = RosterContext.Create())
            {
                var duties = context.Duties.Where(d => d.FromDate >= startDate && d.ToDate <= endDate);
                List<DutyModel> model = new List<DutyModel>();
                foreach (var d in duties)
                {
                    model.Add(new DutyModel {
                        Id = d.Id,
                        ClubId = d.ClubId,
                        Description = d.Description,
                        start = d.FromDate.ToString("yyyy'-'MM'-'dd"),
                        end =d.ToDate.ToString("yyyy'-'MM'-'dd"),
                        title =d.Name,
                        Instructions =d.Instructions,
                        UserId =d.UserId,
                        starttime = d.Starttime,
                        endtime = d.Endtime,
                        backgroundColor = string.IsNullOrEmpty(d.UserId) ? "Red" : "Green"

                    });
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("volunteer")]
        public bool Volunteer(int dutyId)
        {

            try
            {
                using (var context = RosterContext.Create())
                {
                    var usr = context.Users.First(u => u.Name == User.Identity.Name);
                    var duty = context.Duties.First(d => d.Id == dutyId);
                    duty.UserId = usr.Id;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
