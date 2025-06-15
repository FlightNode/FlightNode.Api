namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class DecSix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkLog", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.WorkLog", "WorkTypeId", "dbo.WorkType");
            DropPrimaryKey("dbo.Locations");
            DropPrimaryKey("dbo.WorkLog");
            DropPrimaryKey("dbo.WorkType");
            DropColumn("dbo.Locations", "LocationId");
            DropColumn("dbo.WorkLog", "WorkLogId");
            DropColumn("dbo.WorkType", "WorkTypeId");
            AddColumn("dbo.Locations", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WorkLog", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WorkType", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "Id");
            AddPrimaryKey("dbo.WorkLog", "Id");
            AddPrimaryKey("dbo.WorkType", "Id");
            AddForeignKey("dbo.WorkLog", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkLog", "WorkTypeId", "dbo.WorkType", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkType", "WorkTypeId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.WorkLog", "WorkLogId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Locations", "LocationId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.WorkLog", "WorkTypeId", "dbo.WorkType");
            DropForeignKey("dbo.WorkLog", "LocationId", "dbo.Locations");
            DropPrimaryKey("dbo.WorkType");
            DropPrimaryKey("dbo.WorkLog");
            DropPrimaryKey("dbo.Locations");
            DropColumn("dbo.WorkType", "Id");
            DropColumn("dbo.WorkLog", "Id");
            DropColumn("dbo.Locations", "Id");
            AddPrimaryKey("dbo.WorkType", "WorkTypeId");
            AddPrimaryKey("dbo.WorkLog", "WorkLogId");
            AddPrimaryKey("dbo.Locations", "LocationId");
            AddForeignKey("dbo.WorkLog", "WorkTypeId", "dbo.WorkType", "WorkTypeId", cascadeDelete: true);
            AddForeignKey("dbo.WorkLog", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: true);
        }
    }
}
