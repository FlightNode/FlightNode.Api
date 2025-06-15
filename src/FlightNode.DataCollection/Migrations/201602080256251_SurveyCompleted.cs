namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class SurveyCompleted : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disturbances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        DurationMinutes = c.Int(nullable: false),
                        Result = c.String(maxLength: 150),
                        SurveyIdentifier = c.Guid(nullable: false),
                        DisturbanceType_Id = c.Int(nullable: false),
                        SurveyCompleted_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisturbanceTypes", t => t.DisturbanceType_Id, cascadeDelete: true)
                .ForeignKey("dbo.SurveyCompleted", t => t.SurveyCompleted_Id)
                .Index(t => t.DisturbanceType_Id)
                .Index(t => t.SurveyCompleted_Id);
            
            CreateTable(
                "dbo.DisturbanceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Observations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NestPresent = c.Boolean(nullable: false),
                        ChicksPresent = c.Boolean(nullable: false),
                        FledglingPresent = c.Boolean(nullable: false),
                        RawCount = c.Int(nullable: false),
                        Bin1 = c.Int(nullable: false),
                        Bin2 = c.Int(nullable: false),
                        Bin3 = c.Int(nullable: false),
                        SurveyIdentifier = c.Guid(nullable: false),
                        BirdSpecies_Id = c.Int(),
                        SurveyCompleted_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BirdSpecies", t => t.BirdSpecies_Id)
                .ForeignKey("dbo.SurveyCompleted", t => t.SurveyCompleted_Id)
                .Index(t => t.BirdSpecies_Id)
                .Index(t => t.SurveyCompleted_Id);
            
            CreateTable(
                "dbo.Observers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SurveyIdentifier = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql("ALTER TABLE dbo.Observers ADD CONSTRAINT FK_Observers_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id) ON DELETE CASCADE;");
            
            CreateTable(
                "dbo.SurveyCompleted",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyIdentifier = c.Guid(nullable: false),
                        LocationId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Assessment = c.Int(nullable: false),
                        Vantage = c.Int(nullable: false),
                        Access = c.Int(nullable: false),
                        GeneralComments = c.String(maxLength: 500),
                        DisturbanceComments = c.String(maxLength: 500),
                        SurveyType_Id = c.Int(),
                        SubmittedBy = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyType", t => t.SurveyType_Id)
                .Index(t => t.SurveyType_Id);

            Sql("ALTER TABLE dbo.SurveyCompleted ADD CONSTRAINT FK_SurveyCompleted_Users FOREIGN KEY (SubmittedBy) REFERENCES dbo.Users (Id) ON DELETE NO ACTION;");

            DropColumn("dbo.Locations", "SiteCode");
            AddColumn("dbo.Locations", "SiteCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Locations", "County", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Locations", "City", c => c.String(nullable: false, maxLength: 100));

            Sql("UPDATE dbo.Locations SET SiteName = Description");
            DropColumn("dbo.Locations", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Description", c => c.String(nullable: false, maxLength: 100));
            Sql("UPDATE dbo.Locations SET Description = SiteName");

            DropForeignKey("dbo.SurveyCompleted", "SurveyType_Id", "dbo.SurveyType");
            DropForeignKey("dbo.Observations", "SurveyCompleted_Id", "dbo.SurveyCompleted");
            DropForeignKey("dbo.Disturbances", "SurveyCompleted_Id", "dbo.SurveyCompleted");
            DropForeignKey("dbo.Observations", "BirdSpecies_Id", "dbo.BirdSpecies");
            DropForeignKey("dbo.Disturbances", "DisturbanceType_Id", "dbo.DisturbanceTypes");
            DropIndex("dbo.SurveyCompleted", new[] { "SurveyType_Id" });
            DropIndex("dbo.Observations", new[] { "SurveyCompleted_Id" });
            DropIndex("dbo.Observations", new[] { "BirdSpecies_Id" });
            DropIndex("dbo.Disturbances", new[] { "SurveyCompleted_Id" });
            DropIndex("dbo.Disturbances", new[] { "DisturbanceType_Id" });
            DropColumn("dbo.Locations", "City");
            DropColumn("dbo.Locations", "County");
            DropColumn("dbo.Locations", "SiteCode");
            DropColumn("dbo.Locations", "SiteName");
            DropTable("dbo.SurveyCompleted");
            DropTable("dbo.Observers");
            DropTable("dbo.Observations");
            DropTable("dbo.DisturbanceTypes");
            DropTable("dbo.Disturbances");
        }
    }
}
