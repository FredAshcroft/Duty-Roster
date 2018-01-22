namespace DutyRoster.Migrations
{
    using DutyRoster.Data;
    using DutyRoster.Data.Utils;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<RosterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RosterContext context)
        {
            if(Debugger.IsAttached == false)
            {
                Debugger.Launch();
            }
            //  This method will be called after migrating to the latest version.
            DBSeeder.SeedClubs(context);
            DBSeeder.SeedIdentities(context);
            DBSeeder.SeedDuties(context);
            
        }
    }
}
