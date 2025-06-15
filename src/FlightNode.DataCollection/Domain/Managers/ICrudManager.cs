using FlightNode.Common.BaseClasses;
using System.Collections.Generic;

namespace FlightNode.DataCollection.Domain.Managers
{
    public interface ICrudManager<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> FindAll();
        TEntity FindById(int id);
        TEntity Create(TEntity input);
        int Update(TEntity input);
    }
}
