namespace Integrator.Infrastructure
{
    public interface IRepository
    {
        TEntity Find<TEntity>(object id)
            where TEntity : class;
    }
}