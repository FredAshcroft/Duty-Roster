using DutyRoster.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DutyRoster.Data.Utils
{
    public class DBSeeder
    {
        public static void SeedClubs(RosterContext context)
        {
            Club lsc = new Club
            {
                Name = "Langstone Sailing Club",
                ContactEmail = "tom@daddytom.com",
                PhoneNumber = "07710471444"
            };
            Address lscAddress = new Address
            {
                Club = lsc,
                HouseNumber = "LSC",
                Street = "Langstone Rd",
                TownCity = "Havant",
                Postcode = "PO9 1RD"
            };
            lsc.Address.ToList().Add(lscAddress);
            if(!context.Clubs.Any(c => c.Name == lsc.Name))
            {
                context.Addresses.Add(lscAddress);
                context.Clubs.Add(lsc);
                context.SaveChanges();
            }
        }
        public static void SeedIdentities(RosterContext context)
        {
            //Create User Manager and Role Manager from the DB Part of the MVC Identity Framework
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //Check for and create roles if necessary
            if (!roleManager.RoleExists(RoleNames.SA))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.SA));
            }
            if (!roleManager.RoleExists(RoleNames.ADMIN))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ADMIN));
            }
            if (!roleManager.RoleExists(RoleNames.USER))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.USER));
            }
            //Check for and create SA user.
            string sauser = "fred@daddytom.com";
            string sapass = "fatcatdog";
            ApplicationUser sa = userManager.FindByName(sauser);
            if(sa == null)
            {
                sa = new ApplicationUser()
                {
                    Name = sauser,
                    UserName = sauser,
                    Email = sauser,
                    EmailConfirmed = true,
                    PhoneNumber = "07592517209",
                    PhoneNumberConfirmed = true,
                    ClubId = 1
                };
                IdentityResult result = userManager.Create(sa, sapass);
                if(result.Succeeded)
                {
                    var roleResult = userManager.AddToRole(sa.Id, RoleNames.SA);
                }
            }
            //Check for and create ADMIN user.
            string admuser = "frederickashcroft3@gmail.com";
            string admpass = "thingthing123";
            ApplicationUser admin = userManager.FindByName(admuser);
            if (admin == null)
            {
                admin = new ApplicationUser()
                {
                    Name = admuser,
                    UserName = admuser,
                    Email = admuser,
                    EmailConfirmed = true,
                    PhoneNumber = "07592517209",
                    PhoneNumberConfirmed = true,
                    ClubId = 1
                };
                IdentityResult result = userManager.Create(admin, admpass);
                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRole(admin.Id, RoleNames.ADMIN);
                }
            }
        }
        public static void SeedDuties(RosterContext context)
        {
            var SafteyBoatDriver = new DutyType
            {
                Name = "Safety Boat Driver",
                ClubId = 1,
                Description = "Driving a safety boat for an event.",
                Instructions = "Provide adiquite safety coverage for the event."
                // Sb driver ,SB assistant, Race officer ,RO assistant
            };
            var SafteyBoatAssistant = new DutyType
            {
                Name = "Safety Boat Assistant",
                ClubId = 1,
                Description = "Assisting the safety boat driver.",
                Instructions = "Help driver provide adiquite safety coverage for the event."
            };
            var RaceOfficer = new DutyType
            {
                Name = "Race Officer",
                ClubId = 1,
                Description = "Help running a race by conducting start sequence and finish times.",
                Instructions = "Help organise launching of participants and making apropriate choices for shortened course etc based on circumstances. "
            };
            var RaceOfficerassistant = new DutyType
            {
                Name = "Race Officer Assistant",
                ClubId = 1,
                Description = "Assisting thhe Race officer in running a race.",
                Instructions = "Assist the race officer with their duties to the best of your ability."
                
            };
            var d1 = new Duty
            {
                Name = "test1",
                ClubId = 1,
                Description = "test SB driver",
                FromDate = DateTime.Now.AddDays(3),
                ToDate = DateTime.Now.AddDays(3.5),
                Starttime = "9:00AM",
                Endtime = "4:00PM"
            };
            var d2 = new Duty
            {
                Name = "test2",
                ClubId = 1,
                Description = "test SB Assistant",
                FromDate = DateTime.Now.AddDays(3),
                ToDate = DateTime.Now.AddDays(3.5),
                Starttime = "9:00AM",
                Endtime = "4:00PM"
            };
            var d3 = new Duty
            {
                Name = "test3",
                ClubId = 1,
                Description = "test Race Officer",
                FromDate = DateTime.Now.AddDays(5),
                ToDate = DateTime.Now.AddDays(5.5),
                Starttime = "9:00AM",
                Endtime = "4:00PM"
            };
            var d4 = new Duty
            {
                Name = "test4",
                ClubId = 1,
                Description = "test RO Assistant",
                FromDate = DateTime.Now.AddDays(5),
                ToDate = DateTime.Now.AddDays(5.5),
                Starttime = "9:00AM",
                Endtime = "4:00PM"
            };
            context.DutyTypes.Add(SafteyBoatDriver);
            context.DutyTypes.Add(SafteyBoatAssistant);
            context.DutyTypes.Add(RaceOfficer);
            context.DutyTypes.Add(RaceOfficerassistant);

            context.Duties.Add(d1);
            context.Duties.Add(d2);
            context.Duties.Add(d3);
            context.Duties.Add(d4);

            context.SaveChanges();
        }
    }
}