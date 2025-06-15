using System;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;

namespace FlightNode.DataCollection.Infrastructure.Customization
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            using (IndentedTextWriter writer = Writer())
            {
                writer.WriteLine(CreateSqlStatement(migrationOperation));
                Statement(writer);
            }
        }

        private string CreateSqlStatement(MigrationOperation migrationOperation)
        {
            var createViewOperation = migrationOperation as CreateViewOperation;
            if (createViewOperation != null)
            {
                return string.Format("CREATE VIEW {0} AS {1} ; ",
                                      createViewOperation.ViewName,
                                      createViewOperation.ViewString);
            }

            var dropViewOperation = migrationOperation as DropViewOperation;
            if (dropViewOperation != null)
            {
                return string.Format("DROP VIEW {0};", dropViewOperation.ViewName);
            }

            throw new InvalidOperationException("Invalid migration type: " + migrationOperation.GetType().FullName);
        }
    }
}
