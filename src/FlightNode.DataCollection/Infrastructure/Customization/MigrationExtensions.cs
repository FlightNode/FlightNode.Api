using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace FlightNode.DataCollection.Infrastructure.Customization
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class MigrationExtensions
    {
        public static void CreateView(this DbMigration migration, string viewName, string viewQueryString)
        {

            ((IDbMigration)migration)
              .AddOperation(new CreateViewOperation(viewName,
                 viewQueryString));
        }

        public static void DropView(this DbMigration migration, string viewName)
        {
            ((IDbMigration) migration).AddOperation(new DropViewOperation(viewName));
        }
    }
}
