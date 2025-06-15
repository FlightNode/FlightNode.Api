using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{
    public interface IDbEntityEntryDecorator
    {
        DbPropertyValues CurrentValues { get; }
        object Entity { get; }
        DbPropertyValues OriginalValues { get; }
        EntityState State { get; set; }

        DbEntityEntry<TEntity> Cast<TEntity>() where TEntity : class;
        IDbCollectionEntryDecorator Collection(string navigationProperty);
        DbComplexPropertyEntry ComplexProperty(string propertyName);
        bool Equals(object obj);
        bool Equals(DbEntityEntry other);
        DbPropertyValues GetDatabaseValues();
        Task<DbPropertyValues> GetDatabaseValuesAsync();
        Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken);
        int GetHashCode();
        Type GetType();
        DbEntityValidationResult GetValidationResult();
        DbMemberEntry Member(string propertyName);
        DbPropertyEntry Property(string propertyName);
        DbReferenceEntry Reference(string navigationProperty);
        void Reload();
        Task ReloadAsync();
        Task ReloadAsync(CancellationToken cancellationToken);
        string ToString();
    }
}