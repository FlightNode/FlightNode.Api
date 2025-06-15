using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{
    public class DbCollectionEntryDecorator : IDbCollectionEntryDecorator
    {
        private readonly DbCollectionEntry _entry;

        public DbCollectionEntryDecorator(DbCollectionEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            _entry = entry;
        }

        /// <summary>
        /// Gets or sets the current value of the navigation property. The current value
        /// is the entity that the navigation property references.
        /// </summary>
        public object CurrentValue { get { return _entry.CurrentValue; } set { _entry.CurrentValue = value; } }

        /// <summary>
        /// The System.Data.Entity.Infrastructure.DbEntityEntry to which this navigation
        /// property belongs.
        /// </summary>
        public DbEntityEntry EntityEntry { get { return _entry.EntityEntry; } }

        /// <summary>
        /// Gets or sets a value indicating whether all entities of this collection have
        /// been loaded from the database.
        /// </summary>
        /// <remarks>
        /// Loading the related entities from the database either using lazy-loading, as
        /// part of a query, or explicitly with one of the Load methods will set the IsLoaded
        /// flag to true. IsLoaded can be explicitly set to true to prevent the related entities
        /// of this collection from being lazy-loaded. This can be useful if the application
        /// has caused a subset of related entities to be loaded into this collection and
        /// wants to prevent any other entities from being loaded automatically. Note that
        /// explict loading using one of the Load methods will load all related entities
        /// from the database regardless of whether or not IsLoaded is true. When any related
        /// entity in the collection is detached the IsLoaded flag is reset to false indicating
        /// that the not all related entities are now loaded.
        /// </remarks>
        public bool IsLoaded { get { return _entry.IsLoaded; } set { _entry.IsLoaded = value; } }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name { get { return _entry.Name; } }

        /// <summary>
        /// Returns the equivalent generic System.Data.Entity.Infrastructure.DbCollectionEntry`2
        /// object.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of entity on which the member is declared.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of the collection element.
        /// </typeparam>
        /// <returns>
        /// The equivalent generic object.
        /// </returns>
        public DbCollectionEntry<TEntity, TElement> Cast<TEntity, TElement>() where TEntity : class
        {
            return _entry.Cast<TEntity, TElement>();
        }

        /// <summary>
        /// Loads the collection of entities from the database. Note that entities that already
        /// exist in the context are not overwritten with values from the database.
        /// </summary>
        public void Load()
        {
            _entry.Load();
        }

        /// <summary>
        /// Asynchronously loads the collection of entities from the database. Note that
        /// entities that already exist in the context are not overwritten with values from
        /// the database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </remarks>
        public Task LoadAsync()
        {
            return _entry.LoadAsync();
        }

        /// <summary>
        /// Asynchronously loads the collection of entities from the database. Note that
        /// entities that already exist in the context are not overwritten with values from
        /// the database.
        /// </summary>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to
        /// complete.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </remarks>
        public Task LoadAsync(CancellationToken cancellationToken)
        {
            return _entry.LoadAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the query that would be used to load this collection from the database.
        /// The returned query can be modified using LINQ to perform filtering or operations
        /// in the database, such as counting the number of entities in the collection in
        /// the database without actually loading them.
        /// </summary>
        /// <returns>
        /// A query for the collection.
        /// </returns>
        public IQueryable Query()
        {
            return _entry.Query();
        }
    }
}
