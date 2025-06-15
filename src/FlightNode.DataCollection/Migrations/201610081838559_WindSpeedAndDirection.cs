using System.Data.Entity.Migrations;

namespace FlightNode.DataCollection.Domain.Migrations
{
    public partial class WindSpeedAndDirection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WindDirections",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.WindSpeeds",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.SurveyCompleted", "WindDirection", c => c.Int());
            AddColumn("dbo.SurveyPending", "WindDirection", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.SurveyPending", "WindDirection");
            DropColumn("dbo.SurveyCompleted", "WindDirection");
            DropTable("dbo.WindSpeeds");
            DropTable("dbo.WindDirections");
        }
    }
}
