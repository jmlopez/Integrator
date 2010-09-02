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
    }
}