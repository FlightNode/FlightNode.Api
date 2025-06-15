namespace FlightNode.DataCollection.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class DecimalPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 9, scale: 6));
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 9, scale: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
