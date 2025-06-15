using System.Data.Entity.Migrations;

namespace FlightNode.DataCollection.Domain.Migrations
{
   
    public partial class SurveyCreateDate : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.SurveyPending ADD CreateDate DATETIME NOT NULL CONSTRAINT DF_SurveyPending_CreateDate DEFAULT (GETDATE())");
            Sql("ALTER TABLE dbo.SurveyCompleted ADD CreateDate DATETIME NOT NULL CONSTRAINT DF_SurveyCompleted_CreateDate DEFAULT (GETDATE())");
        }

        public override void Down()
        {
            Sql("ALTER TABLE dbo.SurveyPending DROP CONSTRAINT DF_SurveyPending_CreateDate");
            Sql("ALTER TABLE dbo.SurveyCompleted DROP CONSTRAINT DF_SurveyCompleted_CreateDate");
            Sql("ALTER TABLE dbo.SurveyPending DROP COLUMN CreateDate");
            Sql("ALTER TABLE dbo.SurveyCompleted DROP COLUMN CreateDate");
        }
    }
}
