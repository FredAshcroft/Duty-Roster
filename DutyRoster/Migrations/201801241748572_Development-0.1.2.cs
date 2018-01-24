namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Development012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Duties", "Starttime", c => c.String());
            AddColumn("dbo.Duties", "Endtime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Duties", "Endtime");
            DropColumn("dbo.Duties", "Starttime");
        }
    }
}
