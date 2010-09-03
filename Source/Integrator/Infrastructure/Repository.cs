using System;
using System.Collections.Generic;
using NHibernate;

namespace Integrator.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public TEntity Find<TEntity>(object id)
            where TEntity : class
        {
            return _session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _session
                    .CreateCriteria<TEntity>()
                    .List<TEntity>();
        }
    }
}