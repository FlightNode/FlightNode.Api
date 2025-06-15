using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{
    public class DbEntityEntryDecorator : IDbEntityEntryDecorator
    {
        private readonly DbEntityEntry _entry;

        /// <summary>
        /// Creates a new instance of <see cref="DbEntityEntryDecorator"/> from a <see cref="DbEntityEntry"/>.
        /// </summary>
        /// <param name="entry">
        /// The decorated <see cref="DbEntityEntry"/>.
        /// </param>
        public DbEntityEntryDecorator(DbEntityEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            _entry = entry;
        }

        /// <summary>
        /// Gets the current property values for the tracked entity represented by this object.
        /// </summary> 
        public DbPropertyValues CurrentValues { get { return _entry.CurrentValues; } }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        public object Entity { get { return _entry.Entity; } }

        /// <summary>
        /// Gets the original property values for the tracked entity represented by this
        /// object. The original values are usually the entity's property values as they
        /// were when last queried from the database.
        /// </summary>
        public DbPropertyValues OriginalValues { get { return _entry.OriginalValues; } }

        /// <summary>
        /// Gets or sets the state of the entity.
        /// </summary>
        public EntityState State { get { return _entry.State; } set { _entry.State = value; } }


        /// <summary>
        /// Returns a new instance of the generic System.Data.Entity.Infrastructure.DbEntityEntry`1
        /// class for the given generic type for the tracked entity represented by this object.
        /// Note that the type of the tracked entity must be compatible with the generic
        /// type or an exception will be thrown.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <returns>
        /// A generic version.
        /// </returns>
        public DbEntityEntry<TEntity> Cast<TEntity>() where TEntity : class
        {
            return _entry.Cast<TEntity>();
        }

        /// <summary>
        /// Gets an object that represents the collection navigation property from this entity
        /// to a collection of related entities.
        /// </summary>
        /// <param name="navigationProperty">
        /// The name of the navigation property.
        /// </param>
        /// <returns>
        /// An object representing the navigation property.
        /// </returns>
        public IDbCollectionEntryDecorator Collection(string navigationProperty)
        {
            return new DbCollectionEntryDecorator(_entry.Collection(navigationProperty));
        }

        /// <summary>
        /// Gets an object that represents a complex property of this entity.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the complex property.
        /// </param>
        /// <returns>
        /// An object representing the complex property.
        /// </returns>
        public DbComplexPropertyEntry ComplexProperty(string propertyName)
        {
            return _entry.ComplexProperty(propertyName);
        }

        /// <summary>
        /// Determines whether the specified System.Data.Entity.Infrastructure.DbEntityEntry
        /// is equal to this instance. Two System.Data.Entity.Infrastructure.DbEntityEntry
        /// instances are considered equal if they are both entries for the same entity on
        /// the same System.Data.Entity.DbContext.
        /// </summary>
        /// <param name="other">
        /// The System.Data.Entity.Infrastructure.DbEntityEntry to compare with this instance.
        /// </param>
        /// <returns>
        /// true if the specified System.Data.Entity.Infrastructure.DbEntityEntry is equal
        /// to this instance; otherwise, false .
        /// </returns>
        public bool Equals(DbEntityEntry other)
        {
            return _entry.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to this instance. Two
        /// System.Data.Entity.Infrastructure.DbEntityEntry instances are considered equal
        /// if they are both entries for the same entity on the same System.Data.Entity.DbContext.
        /// </summary>
        /// <param name="obj">
        /// The System.Object to compare with this instance.
        /// </param>
        /// <returns>
        /// true if the specified System.Object is equal to this instance; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return _entry.Equals(obj);
        }

        /// <summary>
        /// Queries the database for copies of the values of the tracked entity as they currently
        /// exist in the database. Note that changing the values in the returned dictionary
        /// will not update the values in the database. If the entity is not found in the
        /// database then null is returned.
        /// </summary>
        /// <returns>
        /// The store values.
        /// </returns>
        public DbPropertyValues GetDatabaseValues()
        {
            return _entry.GetDatabaseValues();
        }

        /// <summary>
        /// Asynchronously queries the database for copies of the values of the tracked entity
        /// as they currently exist in the database. Note that changing the values in the
        /// returned dictionary will not update the values in the database. If the entity
        /// is not found in the database then null is returned.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the
        /// store values.
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </remarks>
        public Task<DbPropertyValues> GetDatabaseValuesAsync()
        {
            return _entry.GetDatabaseValuesAsync();
        }

        /// <summary>
        /// Asynchronously queries the database for copies of the values of the tracked entity
        /// as they currently exist in the database. Note that changing the values in the
        /// returned dictionary will not update the values in the database. If the entity
        /// is not found in the database then null is returned.
        /// </summary>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to
        /// complete.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the
        /// store values.
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </remarks>
        public Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
        {
            return _entry.GetDatabaseValuesAsync(cancellationToken);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return _entry.GetHashCode();
        }

        /// <summary>
        /// Gets the System.Type of the current instance.
        /// </summary>
        /// <returns>
        /// The exact runtime type of the current instance.
        /// </returns>
        public new Type GetType()
        {
            return _entry.GetType();
        }

        /// <summary>
        /// Validates this System.Data.Entity.Infrastructure.DbEntityEntry instance and returns
        /// validation result.
        /// </summary>
        /// <returns>
        /// Entity validation result. Possibly null if DbContext.ValidateEntity(DbEntityEntry,
        /// IDictionary{object,object}) method is overridden.
        /// </returns>
        public DbEntityValidationResult GetValidationResult()
        {
            return _entry.GetValidationResult();
        }

        /// <summary>
        /// Gets an object that represents a member of the entity. The runtime type of the
        /// returned object will vary depending on what kind of member is asked for. The
        /// currently supported member types and their return types are: Reference navigation
        /// property: System.Data.Entity.Infrastructure.DbReferenceEntry. Collection navigation
        /// property: System.Data.Entity.Infrastructure.DbCollectionEntry. Primitive/scalar
        /// property: System.Data.Entity.Infrastructure.DbPropertyEntry. Complex property:
        /// System.Data.Entity.Infrastructure.DbComplexPropertyEntry.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the member.
        /// </param>
        /// <returns>
        /// An object representing the member.
        /// </returns>
        public DbMemberEntry Member(string propertyName)
        {
            return _entry.Member(propertyName);
        }

        /// <summary>
        /// Gets an object that represents a scalar or complex property of this entity.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property.
        /// </param>
        /// <returns>
        /// An object representing the property.
        /// </returns>
        public DbPropertyEntry Property(string propertyName)
        {
            return _entry.Property(propertyName);
        }

        /// <summary>
        /// Gets an object that represents the reference (i.e. non-collection) navigation
        /// property from this entity to another entity.
        /// </summary>
        /// <param name="navigationProperty">
        /// The name of the navigation property.
        /// </param>
        /// <returns>
        /// An object representing the navigation property.
        /// </returns>
        public DbReferenceEntry Reference(string navigationProperty)
        {
            return _entry.Reference(navigationProperty);
        }

        /// <summary>
        /// Reloads the entity from the database overwriting any property values with values
        /// from the database. The entity will be in the Unchanged state after calling this
        /// method.
        /// </summary>
        public void Reload()
        {
            _entry.Reload();
        }

        /// <summary>
        /// Asynchronously reloads the entity from the database overwriting any property
        /// values with values from the database. The entity will be in the Unchanged state
        /// after calling this method.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </remarks>
        public Task ReloadAsync()
        {
            return _entry.ReloadAsync();
        }

        /// <summary>
        /// Asynchronously reloads the entity from the database overwriting any property
        /// values with values from the database. The entity will be in the Unchanged state
        /// after calling this method.
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
        public Task ReloadAsync(CancellationToken cancellationToken)
        {
            return _entry.ReloadAsync(cancellationToken);
        }

        /// <summary>
        /// Overrides the ToString() implementation
        /// </summary>
        //// <returns>String representation of the object</returns>
        public override string ToString()
        {
            return _entry.ToString();
        }
    }
}
