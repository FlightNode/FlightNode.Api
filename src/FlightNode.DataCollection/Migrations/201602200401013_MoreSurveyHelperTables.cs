namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MoreSurveyHelperTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedingSuccessRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HabitatTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteAssessments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VantagePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);



            
            AddForeignKey("dbo.SurveyCompleted", "AssessmentId", "dbo.SiteAssessments", "Id");
            AddForeignKey("dbo.SurveyPending", "AssessmentId", "dbo.SiteAssessments", "Id");
            AddForeignKey("dbo.SurveyCompleted", "VantagePointId", "dbo.VantagePoints", "Id");
            AddForeignKey("dbo.SurveyPending", "VantagePointId", "dbo.VantagePoints", "Id");
            AddForeignKey("dbo.SurveyCompleted", "AccessPointId", "dbo.AccessPoints", "Id");
            AddForeignKey("dbo.SurveyPending", "AccessPointId", "dbo.AccessPoints", "Id");

            AddForeignKey("dbo.Observations", "HabitatTypeId", "dbo.HabitatTypes", "Id");
            AddForeignKey("dbo.Observations", "PrimaryActivityId", "dbo.SurveyActivities", "Id");
            AddForeignKey("dbo.Observations", "SecondaryActivityId", "dbo.SurveyActivities", "Id");
            AddForeignKey("dbo.Observations", "FeedingSuccessRate", "dbo.FeedingSuccessRates", "Id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyCompleted", "AssessmentId", "dbo.SiteAssessments");
            DropForeignKey("dbo.SurveyPending", "AssessmentId", "dbo.SiteAssessments");
            DropForeignKey("dbo.SurveyCompleted", "VantagePointId", "dbo.VantagePoints");
            DropForeignKey("dbo.SurveyPending", "VantagePointId", "dbo.VantagePoints");
            DropForeignKey("dbo.SurveyCompleted", "AccessPointId", "dbo.AccessPoints");
            DropForeignKey("dbo.SurveyPending", "AccessPointId", "dbo.AccessPoints");

            DropForeignKey("dbo.Observations", "HabitatTypeId", "dbo.HabitatTypes");
            DropForeignKey("dbo.Observations", "PrimaryActivityId", "dbo.SurveyActivities");
            DropForeignKey("dbo.Observations", "SecondaryActivityId", "dbo.SurveyActivities");
            DropForeignKey("dbo.Observations", "FeedingSuccessRate","dbo.FeedingSuccessRates");

            DropTable("dbo.SurveyActivities");
            DropTable("dbo.VantagePoints");
            DropTable("dbo.SiteAssessments");
            DropTable("dbo.HabitatTypes");
            DropTable("dbo.FeedingSuccessRates");
            DropTable("dbo.AccessPoints");
        }
    }
}
