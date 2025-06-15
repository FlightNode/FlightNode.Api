using FlightNode.Common.BaseClasses;

namespace FlightNode.DataCollection.Domain.Interfaces.Persistence
{
    public interface IPersistenceBase<TEntity> : IModifiable
    where TEntity : class, IEntity
    {
        ICrudSet<TEntity> Collection { get; }

    }
}
