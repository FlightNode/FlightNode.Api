using FlightNode.Common.BaseClasses;
using System.Data.Entity;

namespace FlightNode.DataCollection.Domain.Interfaces.Persistence
{
    public interface ICrudSet<TEntity> : IDbSet<TEntity>
        where TEntity : class, IEntity
    {

    }
}
