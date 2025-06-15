namespace FlightNode.DataCollection.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsInObservations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Observations", "PrimaryActivityId", c => c.Int());
            AlterColumn("dbo.Observations", "SecondaryActivityId", c => c.Int());
            AlterColumn("dbo.Observations", "HabitatTypeId", c => c.Int());
            AlterColumn("dbo.Observations", "FeedingSuccessRate", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Observations", "FeedingSuccessRate", c => c.Int(nullable: false));
            AlterColumn("dbo.Observations", "HabitatTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Observations", "SecondaryActivityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Observations", "PrimaryActivityId", c => c.Int(nullable: false));
        }
    }
}
