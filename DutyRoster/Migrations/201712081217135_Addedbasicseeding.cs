namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbasicseeding : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Addresses", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "ApplicationUser_Id");
            DropColumn("dbo.Duties", "Dutytype");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Duties", "Dutytype", c => c.String());
            AddColumn("dbo.Addresses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Password");
            CreateIndex("dbo.Addresses", "ApplicationUser_Id");
            AddForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
