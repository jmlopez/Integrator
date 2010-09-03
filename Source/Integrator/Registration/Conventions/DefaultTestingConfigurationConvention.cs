using System.Collections.Generic;
using Integrator.Commands;

namespace Integrator.Registration.Conventions
{
    public class DefaultTestingConfigurationConvention : IConfigurationAction
    {
        public void Configure(DomainGraph graph)
        {
            graph
                .EntityMaps
                .Each(map =>
                          {
                              var config = new EntityTestConfiguration(map.EntityType);
                              config.InsertCommandType = typeof(DefaultPersistEntityCommand<>).MakeGenericType(map.EntityType);
                              config.VerificationCommandType = typeof(DefaultVerificationCommand<>).MakeGenericType(map.EntityType);

                              map.Configure(config);
                          });
        }
    }
}