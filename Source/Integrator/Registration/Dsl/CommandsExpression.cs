using Commander;

namespace Integrator.Registration.Dsl
{
    public class CommandsExpression
    {
        private readonly CommandRegistry _registry;

        public CommandsExpression(CommandRegistry registry)
        {
            _registry = registry;
        }

        public CommandsExpression ApplyConvention<T>()
            where T : Commander.Registration.IConfigurationAction, new()
        {
            _registry.ApplyConvention<T>();
            return this;
        }

        public CommandsExpression ApplyConvention(Commander.Registration.IConfigurationAction action)
        {
            _registry.ApplyConvention(action);
            return this;
        }
    }
}