using Commander;
using ProAceFx.Infrastructure;

namespace Integrator.Commands
{
    public class DefaultDeleteEntityCommand<TEntity> : IDomainCommand<TEntity>
        where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public DefaultDeleteEntityCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute(TEntity entity)
        {
            _unitOfWork.Initialize();
            _unitOfWork.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}