﻿using DutyRoster.Data;
using DutyRoster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace DutyRoster.Controllers
{
    [Route("duty")]
    public class CalendarController : ApiController
    {
        [Route("get")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetDuties(double start, double end)
        {
            DateTime startDate = ConvertFromUnixTimestamp(start);
            DateTime endDate = ConvertFromUnixTimestamp(end);
            using (var context = RosterContext.Create())
            {
                var duties = context.Duties.Where(d => d.FromDate >= startDate && d.ToDate <= endDate);
                List<DutyModel> model = new List<DutyModel>();
                foreach (var d in duties)
                {
                    model.Add(new DutyModel { Id = d.Id,ClubId = d.ClubId, Description= d.Description, FromDate = d.FromDate, ToDate=d.ToDate, Name=d.Name, Instructions=d.Instructions,UserId=d.UserId });
                }
                string json = JsonConvert.SerializeObject(model);
                return Ok(json);
            }
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
