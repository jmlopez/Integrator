using System;
using FubuCore;
using Integrator.Infrastructure;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Integrator
{
    public class IntegratorStructureMapRegistry : Registry
    {
        public IntegratorStructureMapRegistry()
        {
            For<IRepository>().Use<Repository>();
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