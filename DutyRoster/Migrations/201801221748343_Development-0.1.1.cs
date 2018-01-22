namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Development011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Duties", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Duties", "UserId");
            AddForeignKey("dbo.Duties", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.DutyTypes", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DutyTypes", "Type", c => c.String(nullable: false));
            DropForeignKey("dbo.Duties", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Duties", new[] { "UserId" });
            DropColumn("dbo.Duties", "UserId");
        }
    }
}
