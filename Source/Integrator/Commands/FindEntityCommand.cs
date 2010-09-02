using Commander.Commands;
using Commander.Runtime;
using Integrator.Infrastructure;

namespace Integrator.Commands
{
    public class FindEntityCommand<TEntity> : BasicCommand
        where TEntity : class
    {
        private readonly IRepository _repository;
        private readonly ICommandContext _commandContext;

        public FindEntityCommand(IRepository repository, ICommandContext commandContext)
        {
            _repository = repository;
            _commandContext = commandContext;
        }

        protected override DoNext PerformInvoke()
        {
            var request = _commandContext.Get<EntityRequest>();
            var entity = _repository.Find<TEntity>(request.EntityId);
            _commandContext.Set(entity);

            return DoNext.Continue;
        }
    }
}