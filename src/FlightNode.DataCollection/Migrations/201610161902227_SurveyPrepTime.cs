namespace FlightNode.DataCollection.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyPrepTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyCompleted", "PrepTimeHours", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SurveyPending", "PrepTimeHours", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyPending", "PrepTimeHours");
            DropColumn("dbo.SurveyCompleted", "PrepTimeHours");
        }
    }
}
