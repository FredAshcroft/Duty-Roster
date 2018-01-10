namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Development010 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ClubId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "ClubId");
            AddForeignKey("dbo.Addresses", "ClubId", "dbo.Clubs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Addresses", new[] { "ClubId" });
            DropColumn("dbo.Addresses", "ClubId");
        }
    }
}
