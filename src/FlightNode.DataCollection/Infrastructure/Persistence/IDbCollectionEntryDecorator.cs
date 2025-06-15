using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{
    public interface IDbCollectionEntryDecorator
    {
        object CurrentValue { get; set; }
        DbEntityEntry EntityEntry { get; }
        bool IsLoaded { get; set; }
        string Name { get; }

        DbCollectionEntry<TEntity, TElement> Cast<TEntity, TElement>() where TEntity : class;
        void Load();
        Task LoadAsync();
        Task LoadAsync(CancellationToken cancellationToken);
        IQueryable Query();
    }
}