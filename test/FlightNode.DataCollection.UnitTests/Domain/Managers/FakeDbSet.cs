using FlightNode.Common.BaseClasses;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace FlightNode.DataCollection.Domain.UnitTests.Domain.Managers
{


    public class FakeDbSet<TEntity> : ICrudSet<TEntity>
        where TEntity : class, IEntity
    {
        public List<TEntity> List = new List<TEntity>();

        public static FakeDbSet<TEntity> Create(params TEntity[] entities)
        {
            var set = new FakeDbSet<TEntity>();
            set.List.AddRange(entities);
            return set;
        }



        public Type ElementType
        {
            get
            {
                return typeof(TEntity);
            }
        }

        public Expression Expression
        {
            get
            {
                return List.AsQueryable().Expression;
            }
        }

        public ObservableCollection<TEntity> Local
        {
            get
            {
                return new ObservableCollection<TEntity>(List);
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return List.AsQueryable().Provider;
            }
        }

        public TEntity Add(TEntity entity)
        {
            List.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            return Add(entity);
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            throw new NotImplementedException();
        }

        public TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return List.GetEnumerator();
        }


        public string IncludeTable { get; set; }

        public ICrudSet<TEntity> Include(string relatedTable)
        {
            IncludeTable = relatedTable;
            return this;
        }

        public TEntity Remove(TEntity entity)
        {
            List.Remove(entity);
            return entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public ICrudSet<TEntity> AsNoTracking()
        {
            return this;
        }
    }
}
