namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentitySeed2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String(nullable: false));
        }
    }
}
