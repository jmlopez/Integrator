namespace Integrator
{
    public interface IVerificationCommand<TEntity>
        where TEntity : class
    {
        void Verify(TEntity beforeInsert, TEntity afterInsert);
    }
}