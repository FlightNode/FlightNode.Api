using FlightNode.Common.BaseClasses;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightNode.DataCollection.Domain.Managers
{
    public abstract class DomainManagerBase<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IPersistenceBase<TEntity> Persistence;
        
        protected DomainManagerBase(IPersistenceBase<TEntity> persistence)
        {
            Persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }


        public virtual TEntity Create(TEntity input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            input.Validate();

            Persistence.Add(input);
            Persistence.SaveChanges();

            // input will now have its primary key populated
            return input;
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return Persistence.Collection
                    .ToList();
        }

        public virtual TEntity FindById(int id)
        {
            return Persistence.Collection.FirstOrDefault(x => x.Id == id);
        }

        public virtual int UpdateAttachedObject(TEntity input)
        {
            input.Validate();
            

            return Persistence.SaveChanges();            
        }

        public virtual int Update(TEntity input)
        {
            input.Validate();


            // TODO: re-evaluate the architecture. Is DomainManagerBase for the domain or persistence layer?
            // If it is for the domain layer, it shouldn't know anything about EF internals!
            //Persistence.Collection.Attach(input);

            //StateModifier.SetModifiedState(Persistence, input);

            Persistence.Update(input);

            return Persistence.SaveChanges();
        }
    }
}
