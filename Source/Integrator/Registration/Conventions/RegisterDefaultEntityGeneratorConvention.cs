using System.Collections.Generic;
using Integrator.Generators;

namespace Integrator.Registration.Conventions
{
    public class RegisterDefaultEntityGeneratorConvention : IGeneratorRegistryModification
    {
        public void Modify(DomainGraph graph, IGeneratorRegistry registry)
        {
            graph
                .EntityMaps
                .Each(map => registry.Register(map.EntityType, new DefaultEntityGenerator(map.EntityType, graph)));
        }
    }
}