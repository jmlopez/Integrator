using System.Collections.Generic;
using Integrator.Registration;
using Integrator.Registration.Dsl;

namespace Integrator
{
    public abstract class IntegratorRegistryExtension : IIntegratorRegistryExtension
    {
        private readonly IList<IConfigurationAction> _policies = new List<IConfigurationAction>();
        private readonly IList<IConfigurationAction> _conventions = new List<IConfigurationAction>();
        
        public MapAlterationExpression Maps { get { return new MapAlterationExpression(_policies); } }
        public AutomatedTestsExpression AutomatedTests { get { return new AutomatedTestsExpression(_conventions); } }

        public virtual void Configure(IntegratorRegistry registry)
        {
            _conventions.Each(registry.ApplyConvention);
            _policies.Each(action => registry.Policies.Add(action));
        }
    }
}