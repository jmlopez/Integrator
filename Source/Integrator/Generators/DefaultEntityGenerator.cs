using System;
using System.Collections.Generic;
using Integrator.Registration;

namespace Integrator.Generators
{
    public class DefaultEntityGenerator : IEntityGenerator
    {
        private readonly Type _entityType;
        private readonly DomainGraph _graph;

        public DefaultEntityGenerator(Type entityType, DomainGraph graph)
        {
            _entityType = entityType;
            _graph = graph;
        }

        public object Generate()
        {
            var entity = _entityType.GetDefaultInstance();
            Fill(entity, _graph, _graph.MapFor(_entityType));

            return entity;
        }

        public void Fill(object entity, DomainGraph graph, EntityMap map)
        {
            map
                .PropertyMaps
                .Each(propertyMap =>
                          {
                              var value = propertyMap.Generator.Generate();
                              propertyMap
                                  .Request
                                  .Accessor()
                                  .SetValue(entity, value);
                          });
        }
    }
}