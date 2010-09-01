using System;

namespace Integrator.Registration
{
    public class LambdaConfigurationAction : IConfigurationAction
    {
        private readonly Action<DomainGraph> _action;

        public LambdaConfigurationAction(Action<DomainGraph> action)
        {
            _action = action;
        }

        public void Configure(DomainGraph graph)
        {
            _action(graph);
        }
    }
}