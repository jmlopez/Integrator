using Commander;

namespace Integrator
{
    public class PersistEntityCommand<TEntity> : IDomainCommand<TEntity>
        where TEntity : class
    {
        public void Execute(TEntity entity)
        {
        }
    }
}