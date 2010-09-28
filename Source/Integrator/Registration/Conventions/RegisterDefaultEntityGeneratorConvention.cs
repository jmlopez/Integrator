using System.Collections.Generic;
using System.Linq;
using Integrator.Generators;

namespace Integrator.Registration.Conventions
{
    public class RegisterDefaultEntityGeneratorConvention : IGeneratorRegistryModification
    {
        public void Modify(DomainGraph graph, IGeneratorRegistry registry)
        {
            graph
                .EntityMaps
                .Where(map => !registry.Has(map.EntityType))
                .Each(map => registry.Register(map.EntityType, new DefaultEntityGenerator(map.EntityType, graph)));
        }
    }
}