using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Integrator.Registration.Conventions
{
    public class GeneratorResolver
    {
        private readonly List<IGeneratorPolicy> _policies = new List<IGeneratorPolicy>();

        public void Apply(DomainGraph graph, EntityMap map)
        {
            var entityType = map.EntityType;
            var entityRequest = ValueRequest.For(entityType);

            var entityPolicy = _policies.Last(p => p.Matches(entityRequest));
            map.GenerateWith(entityPolicy);
            
            var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            properties.Each(property =>
                                {
                                    var propertyRequest = ValueRequest.For(entityType, property);
                                    var propertyPolicy = _policies.Last(p => p.Matches(propertyRequest));

                                    map.AddPropertyMap(new PropertyMap(propertyRequest, () => propertyPolicy.Build(propertyRequest)));
                                });
        }

        public void ApplyToAll(DomainGraph graph)
        {
            graph
                .EntityMaps
                .ToList()
                .Each(map => Apply(graph, map));
        }

        public void RegisterGeneratorPolicy(IGeneratorPolicy policy)
        {
            _policies.Add(policy);
        }
    }
}