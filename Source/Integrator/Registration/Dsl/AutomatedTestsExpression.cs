using System.Collections.Generic;

namespace Integrator.Registration.Dsl
{
    public class AutomatedTestsExpression
    {
        private readonly IList<IConfigurationAction> _actions;

        public AutomatedTestsExpression(IList<IConfigurationAction> actions)
        {
            _actions = actions;
        }

        public EntityTestConfigurationExpression<TEntity> Configure<TEntity>()
            where TEntity : class
        {
            return new EntityTestConfigurationExpression<TEntity>(_actions);
        }
    }
}