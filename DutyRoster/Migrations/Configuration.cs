namespace DutyRoster.Migrations
{
    using DutyRoster.Data;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(u => u.UserName,
            new ApplicationUser
            {
                UserName = "Steve@Steve.com",
                Password = "Password1",
                PhoneNumber = "07896543567"
            });
            context.Users.AddOrUpdate(u => u.UserName,
            new ApplicationUser
            {
                UserName = "Henry@dudman.com",
                Password = "Henry123",
                PhoneNumber = "07121123432"
            });
            context.Users.AddOrUpdate(u => u.UserName,
            new ApplicationUser
            {
                UserName = "Nathan@mudie.com",
                Password = "Nathan098",
                PhoneNumber = "07654321098"
            });
            
        }
    }
}
