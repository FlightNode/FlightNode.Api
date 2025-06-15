using System.Data.Entity.Migrations;

namespace FlightNode.DataCollection.Domain.Migrations
{

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ImprovingSurveySupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Observations", "PrimaryActivityId", c => c.Int(nullable: false));
            AddColumn("dbo.Observations", "SecondaryActivityId", c => c.Int(nullable: false));
            AddColumn("dbo.Observations", "HabitatTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Observations", "FeedingSuccessRate", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "AssessmentId", c => c.Int());
            AddColumn("dbo.SurveyCompleted", "VantagePointId", c => c.Int());
            AddColumn("dbo.SurveyCompleted", "AccessPointId", c => c.Int());
            AddColumn("dbo.SurveyCompleted", "WindSpeedId", c => c.Int());
            AddColumn("dbo.SurveyPending", "AssessmentId", c => c.Int());
            AddColumn("dbo.SurveyPending", "VantagePointId", c => c.Int());
            AddColumn("dbo.SurveyPending", "AccessPointId", c => c.Int());
            AddColumn("dbo.SurveyPending", "WindSpeedId", c => c.Int());
            AlterColumn("dbo.SurveyCompleted", "WeatherId", c => c.Int());
            AlterColumn("dbo.SurveyCompleted", "StartTemperature", c => c.Int());
            AlterColumn("dbo.SurveyCompleted", "EndTemperature", c => c.Int());
            AlterColumn("dbo.SurveyCompleted", "TideId", c => c.Int());
            AlterColumn("dbo.SurveyCompleted", "TimeOfLowTide", c => c.DateTime());
            AlterColumn("dbo.SurveyPending", "StartDate", c => c.DateTime());
            AlterColumn("dbo.SurveyPending", "EndDate", c => c.DateTime());
            AlterColumn("dbo.SurveyPending", "WeatherId", c => c.Int());
            AlterColumn("dbo.SurveyPending", "StartTemperature", c => c.Int());
            AlterColumn("dbo.SurveyPending", "EndTemperature", c => c.Int());
            AlterColumn("dbo.SurveyPending", "TideId", c => c.Int());
            AlterColumn("dbo.SurveyPending", "TimeOfLowTide", c => c.DateTime());
            DropColumn("dbo.Observations", "RawCount");
            DropColumn("dbo.SurveyCompleted", "Assessment");
            DropColumn("dbo.SurveyCompleted", "Vantage");
            DropColumn("dbo.SurveyCompleted", "Access");
            DropColumn("dbo.SurveyCompleted", "WindSpeed");
            DropColumn("dbo.SurveyPending", "Assessment");
            DropColumn("dbo.SurveyPending", "Vantage");
            DropColumn("dbo.SurveyPending", "Access");
            DropColumn("dbo.SurveyPending", "WindSpeed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyPending", "WindSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyPending", "Access", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyPending", "Vantage", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyPending", "Assessment", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "WindSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "Access", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "Vantage", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "Assessment", c => c.Int(nullable: false));
            AddColumn("dbo.Observations", "RawCount", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyPending", "TimeOfLowTide", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyPending", "TideId", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyPending", "EndTemperature", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyPending", "StartTemperature", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyPending", "WeatherId", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyPending", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyPending", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "TimeOfLowTide", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "TideId", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "EndTemperature", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "StartTemperature", c => c.Int(nullable: false));
            AlterColumn("dbo.SurveyCompleted", "WeatherId", c => c.Int(nullable: false));
            DropColumn("dbo.SurveyPending", "WindSpeedId");
            DropColumn("dbo.SurveyPending", "AccessPointId");
            DropColumn("dbo.SurveyPending", "VantagePointId");
            DropColumn("dbo.SurveyPending", "AssessmentId");
            DropColumn("dbo.SurveyCompleted", "WindSpeedId");
            DropColumn("dbo.SurveyCompleted", "AccessPointId");
            DropColumn("dbo.SurveyCompleted", "VantagePointId");
            DropColumn("dbo.SurveyCompleted", "AssessmentId");
            DropColumn("dbo.Observations", "FeedingSuccessRate");
            DropColumn("dbo.Observations", "HabitatTypeId");
            DropColumn("dbo.Observations", "SecondaryActivityId");
            DropColumn("dbo.Observations", "PrimaryActivityId");
        }
    }
}
