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

    }
}