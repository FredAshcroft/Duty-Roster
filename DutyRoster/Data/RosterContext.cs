using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DutyRoster.Data
{
    /// <summary>
    /// This is the connection to the database. Its based on the Entity Framework and the Identity Framework.
    /// Which means that you get a database that will also manage your user identities and 3rd party oauth logins too.
    /// </summary>
    public class RosterContext : IdentityDbContext<ApplicationUser>
    {
        public RosterContext()
            : base("RosterDB", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<RosterContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<RosterContext>());
        }

        public static RosterContext Create()
        {
            return new RosterContext();
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DutyGroup> DutyGroups { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<DutyType> DutyTypes { get; set; }
    }
}