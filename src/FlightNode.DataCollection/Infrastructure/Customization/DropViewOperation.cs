using System.Data.Entity.Migrations.Model;

namespace FlightNode.DataCollection.Infrastructure.Customization
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class DropViewOperation : MigrationOperation
    {
        public DropViewOperation(string viewName)
            : base(null)
        {
            ViewName = viewName;
        }

        public string ViewName { get; private set; }


        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }
}
