namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class SurveyPending : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Disturbances", "SurveyCompleted_Id", "dbo.SurveyCompleted");
            DropForeignKey("dbo.Observations", "SurveyCompleted_Id", "dbo.SurveyCompleted");
            DropForeignKey("dbo.SurveyCompleted", "SurveyType_Id", "dbo.SurveyType");
            DropForeignKey("dbo.Observations", "BirdSpecies_Id", "dbo.BirdSpecies");
            DropIndex("dbo.Disturbances", new[] { "SurveyCompleted_Id" });
            DropIndex("dbo.Observations", new[] { "BirdSpecies_Id" });
            DropIndex("dbo.Observations", new[] { "SurveyCompleted_Id" });
            DropIndex("dbo.SurveyCompleted", new[] { "SurveyType_Id" });
            RenameColumn(table: "dbo.Disturbances", name: "DisturbanceType_Id", newName: "DisturbanceTypeId");
            RenameColumn(table: "dbo.Observations", name: "BirdSpecies_Id", newName: "BirdSpeciesId");
            RenameIndex(table: "dbo.Disturbances", name: "IX_DisturbanceType_Id", newName: "IX_DisturbanceTypeId");
            CreateTable(
                "dbo.Weather",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyPending",
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
                        SurveyTypeId = c.Int(nullable: false),
                        SubmittedBy = c.Int(nullable: false),
                        WeatherId = c.Int(nullable: false),
                        StartTemperature = c.Int(nullable: false),
                        EndTemperature = c.Int(nullable: false),
                        WindSpeed = c.Int(nullable: false),
                        TideId = c.Int(nullable: false),
                        TimeOfLowTide = c.DateTime(nullable: false),
                    })
                .ForeignKey("dbo.Weather", t => t.WeatherId, cascadeDelete: false)
                .ForeignKey("dbo.Tides", t => t.TideId, cascadeDelete: false)
                .ForeignKey("dbo.SurveyType", t => t.SurveyTypeId, cascadeDelete: false)
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SurveyCompleted", "SurveyTypeId", c => c.Int(nullable: false));
            AddForeignKey("dbo.SurveyCompleted", "SurveyTypeId", "dbo.SurveyType", "Id", cascadeDelete: false);
            AddColumn("dbo.SurveyCompleted", "WeatherId", c => c.Int(nullable: false));
            AddForeignKey("dbo.SurveyCompleted", "WeatherId", "dbo.Weather", "Id", cascadeDelete: false);
            AddColumn("dbo.SurveyCompleted", "StartTemperature", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "EndTemperature", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "WindSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyCompleted", "TideId", c => c.Int(nullable: false));
            AddForeignKey("dbo.SurveyCompleted", "TideId", "dbo.Tides", "Id", cascadeDelete: false);
            AddColumn("dbo.SurveyCompleted", "TimeOfLowTide", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Observations", "BirdSpeciesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Observations", "BirdSpeciesId");
            AddForeignKey("dbo.Observations", "BirdSpeciesId", "dbo.BirdSpecies", "Id", cascadeDelete: true);
            DropColumn("dbo.Disturbances", "SurveyCompleted_Id");
            DropColumn("dbo.Observations", "SurveyCompleted_Id");
            DropColumn("dbo.SurveyCompleted", "SurveyType_Id");


        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyCompleted", "SurveyType_Id", c => c.Int());
            AddColumn("dbo.Observations", "SurveyCompleted_Id", c => c.Int());
            AddColumn("dbo.Disturbances", "SurveyCompleted_Id", c => c.Int());
            DropForeignKey("dbo.Observations", "BirdSpeciesId", "dbo.BirdSpecies");
            DropIndex("dbo.Observations", new[] { "BirdSpeciesId" });
            AlterColumn("dbo.Observations", "BirdSpeciesId", c => c.Int());
            DropColumn("dbo.SurveyCompleted", "TimeOfLowTide");
            DropColumn("dbo.SurveyCompleted", "TideId");
            DropColumn("dbo.SurveyCompleted", "WindSpeed");
            DropColumn("dbo.SurveyCompleted", "EndTemperature");
            DropColumn("dbo.SurveyCompleted", "StartTemperature");
            DropColumn("dbo.SurveyCompleted", "WeatherId");
            DropColumn("dbo.SurveyCompleted", "SurveyTypeId");
            DropTable("dbo.SurveyPending");
            DropTable("dbo.Tides");
            DropTable("dbo.Weather");
            RenameIndex(table: "dbo.Disturbances", name: "IX_DisturbanceTypeId", newName: "IX_DisturbanceType_Id");
            RenameColumn(table: "dbo.Observations", name: "BirdSpeciesId", newName: "BirdSpecies_Id");
            RenameColumn(table: "dbo.Disturbances", name: "DisturbanceTypeId", newName: "DisturbanceType_Id");
            CreateIndex("dbo.SurveyCompleted", "SurveyType_Id");
            CreateIndex("dbo.Observations", "SurveyCompleted_Id");
            CreateIndex("dbo.Observations", "BirdSpecies_Id");
            CreateIndex("dbo.Disturbances", "SurveyCompleted_Id");
            AddForeignKey("dbo.Observations", "BirdSpecies_Id", "dbo.BirdSpecies", "Id");
            AddForeignKey("dbo.SurveyCompleted", "SurveyType_Id", "dbo.SurveyType", "Id");
            AddForeignKey("dbo.Observations", "SurveyCompleted_Id", "dbo.SurveyCompleted", "Id");
            AddForeignKey("dbo.Disturbances", "SurveyCompleted_Id", "dbo.SurveyCompleted", "Id");
        }
    }
}
