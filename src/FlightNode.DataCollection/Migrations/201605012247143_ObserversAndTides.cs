namespace FlightNode.DataCollection.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObserversAndTides : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WaterHeights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SurveyCompleted", "WaterHeightId", c => c.Int());
            AddColumn("dbo.SurveyCompleted", "Observers", c => c.String(maxLength: 1000));
            AddColumn("dbo.SurveyPending", "WaterHeightId", c => c.Int());
            AddColumn("dbo.SurveyPending", "Observers", c => c.String(maxLength: 1000));
            AlterColumn("dbo.SurveyCompleted", "StartDate", c => c.DateTime());
            AlterColumn("dbo.SurveyCompleted", "EndDate", c => c.DateTime());
            DropTable("dbo.Observers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Observers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SurveyIdentifier = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.SurveyCompleted", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.SurveyPending", "Observers");
            DropColumn("dbo.SurveyPending", "WaterHeightId");
            DropColumn("dbo.SurveyCompleted", "Observers");
            DropColumn("dbo.SurveyCompleted", "WaterHeightId");
            DropTable("dbo.WaterHeights");
        }
    }
}
