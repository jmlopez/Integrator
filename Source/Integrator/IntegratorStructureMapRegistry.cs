using System;
using FubuCore;
using Integrator.Commands;
using Integrator.Infrastructure;
using ProAceFx.Core.Configuration;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Integrator
{
    public class IntegratorStructureMapRegistry : Registry
    {
        public IntegratorStructureMapRegistry()
        {
            IncludeRegistry<ProAceCoreRegistry>();
            
            For<IRepository>().Use<Repository>();
            For<IObjectDiffStrategy>().Use<ObjectDiffStrategy>();

            var registry = new PropertyDiffRegistry();
            registry.Register(new EqualityPropertyDiffStrategy());
            registry.Register(new EnumerablePropertyDiffStrategy());

            For<IPropertyDiffRegistry>().Use(registry);

            Scan(x =>
                     {
                         x.AssembliesFromApplicationBaseDirectory();
                         x.Include(type => type.IsCustomRegistryExtension());
                         x.With(new RegisterExtensionsConvention());
                     });
        }

        #region #Nested Type: RegisterExtensionsConvention
        private class RegisterExtensionsConvention : IRegistrationConvention
        {
            public void Process(Type type, Registry registry)
            {
                registry
                    .For<IIntegratorRegistryExtension>()
                    .Add(type.GetDefaultInstance().As<IIntegratorRegistryExtension>());
            }
        }
        #endregion
    }
}