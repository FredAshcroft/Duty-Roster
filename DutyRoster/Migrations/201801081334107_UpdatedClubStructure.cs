namespace DutyRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClubStructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Duties", "ClubId", c => c.Int(nullable: false));
            AddColumn("dbo.Duties", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Duties", "ToDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DutyTypes", "ClubId", c => c.Int(nullable: false));
            AlterColumn("dbo.DutyTypes", "Type", c => c.String(nullable: false));
            CreateIndex("dbo.Duties", "ClubId");
            CreateIndex("dbo.DutyTypes", "ClubId");
            AddForeignKey("dbo.Duties", "ClubId", "dbo.Clubs", "Id", cascadeDelete: false);
            AddForeignKey("dbo.DutyTypes", "ClubId", "dbo.Clubs", "Id", cascadeDelete: false);
            DropColumn("dbo.Duties", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Duties", "Date", c => c.String(nullable: false));
            DropForeignKey("dbo.DutyTypes", "ClubId", "dbo.Clubs");
            DropForeignKey("dbo.Duties", "ClubId", "dbo.Clubs");
            DropIndex("dbo.DutyTypes", new[] { "ClubId" });
            DropIndex("dbo.Duties", new[] { "ClubId" });
            AlterColumn("dbo.DutyTypes", "Type", c => c.String());
            DropColumn("dbo.DutyTypes", "ClubId");
            DropColumn("dbo.Duties", "ToDate");
            DropColumn("dbo.Duties", "FromDate");
            DropColumn("dbo.Duties", "ClubId");
        }
    }
}
