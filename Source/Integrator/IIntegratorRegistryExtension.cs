using System.Collections.Generic;
using Integrator.Registration;
using Integrator.Registration.Dsl;

namespace Integrator
{
    public interface IIntegratorRegistryExtension
    {
        void Configure(IntegratorRegistry registry);
    }

    public abstract class IntegratorRegistryExtension : IIntegratorRegistryExtension
    {
        private readonly IList<IConfigurationAction> _policies = new List<IConfigurationAction>();

        public MapAlterationExpression Maps { get { return new MapAlterationExpression(_policies); } }

        public virtual void Configure(IntegratorRegistry registry)
        {
            _policies.Each(action => registry.Policies.Add(action));
        }
    }
}