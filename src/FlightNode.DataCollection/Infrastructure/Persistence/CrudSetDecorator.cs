using FlightNode.Common.BaseClasses;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace FlightNode.DataCollection.Infrastructure.Persistence
{

    public class CrudSetDecorator<TEntity> : ICrudSet<TEntity>
        where TEntity : class, IEntity
    {
        private DbSet<TEntity> _dbSet;
        private DbQuery<TEntity> _query;

        protected DbQuery<TEntity> Query
        {
            get
            {
                if (_query == null)
                {
                    _query = _dbSet.AsNoTracking();
                }
                return _query;
            }
            set
            {
                _query = value;
            }
        }


        public CrudSetDecorator(DbSet<TEntity> dbSet)
        {
            if (dbSet == null)
            {
                throw new ArgumentNullException("dbSet");
            }
            _dbSet = dbSet;
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
                return Query.AsQueryable().Expression;
            }
        }

        public ObservableCollection<TEntity> Local
        {
            get
            {
                return _dbSet.Local;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return Query.AsQueryable().Provider;
            }
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            return _dbSet.Attach(entity);
        }

        public TEntity Create()
        {
            return _dbSet.Create();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return _dbSet.Create<TDerivedEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _dbSet.AsEnumerable().GetEnumerator();
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dbSet.AsEnumerable().GetEnumerator();
        }
    }
}
