namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameWindSpeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyCompleted", "WindSpeed", c => c.Int());
            AddColumn("dbo.SurveyPending", "WindSpeed", c => c.Int());

            Sql("UPDATE dbo.SurveyCompleted SET WindSpeed = WindSpeedId");
            Sql("UPDATE dbo.SurveyPending SET WindSpeed = WindSpeedId");

            DropColumn("dbo.SurveyCompleted", "WindSpeedId");
            DropColumn("dbo.SurveyPending", "WindSpeedId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyPending", "WindSpeedId", c => c.Int());
            AddColumn("dbo.SurveyCompleted", "WindSpeedId", c => c.Int());
            DropColumn("dbo.SurveyPending", "WindSpeed");
            DropColumn("dbo.SurveyCompleted", "WindSpeed");
        }
    }
}
