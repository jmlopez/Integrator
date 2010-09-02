using Commander;

namespace Integrator.Commands
{
    public class DefaultPersistEntityCommand<TEntity> : IDomainCommand<TEntity>
        where TEntity : class
    {
        public void Execute(TEntity entity)
        {
        }
    }
}