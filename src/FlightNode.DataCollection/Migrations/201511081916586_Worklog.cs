namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Worklog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Latitude = c.Decimal(nullable: false, precision: 9, scale: 6),
                        Longitude = c.Decimal(nullable: false, precision: 9, scale: 6),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.WorkLog",
                c => new
                    {
                        WorkLogId = c.Int(nullable: false, identity: true),
                        WorkDate = c.DateTime(nullable: false),
                        LocationId = c.Int(nullable: false),
                        WorkTypeId = c.Int(nullable: false),
                        WorkHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TravelTimeHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkLogId)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.WorkType", t => t.WorkTypeId, cascadeDelete: false)
                .Index(t => t.LocationId)
                .Index(t => t.UserId)
                .Index(t => t.WorkTypeId);

            AddForeignKey("dbo.WorkLog", "UserId", "dbo.Users", "Id", false);
            
            CreateTable(
                "dbo.WorkType",
                c => new
                    {
                        WorkTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.WorkTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkLog", "WorkTypeId", "dbo.WorkType");
            DropForeignKey("dbo.WorkLog", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.WorkLog", "UserId", "dbo.Users");
            DropIndex("dbo.WorkLog", new[] { "WorkTypeId" });
            DropIndex("dbo.WorkLog", new[] { "LocationId" });
            DropIndex("dbo.WorkLog", new[] { "UserId" });
            DropTable("dbo.WorkType");
            DropTable("dbo.WorkLog");
            DropTable("dbo.Locations");
        }
    }
}
