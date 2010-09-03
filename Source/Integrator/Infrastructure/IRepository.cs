using System.Collections.Generic;

namespace Integrator.Infrastructure
{
    public interface IRepository
    {
        TEntity Find<TEntity>(object id)
            where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>()
            where TEntity : class;
    }
}