using System;
using System.Collections.Generic;
using Integrator.Registration;

namespace Integrator.Generators
{
    public class DefaultEntityGenerator : IEntityGenerator
    {
        protected readonly Type _entityType;
        protected readonly DomainGraph _graph;

        public DefaultEntityGenerator(Type entityType, DomainGraph graph)
        {
            _entityType = entityType;
            _graph = graph;
        }

        public virtual object CreateEntity()
        {
            return _entityType.GetDefaultInstance();
        }

        public virtual object Generate()
        {
            var entity = CreateEntity();
            Fill(entity, _graph);

            return entity;
        }

        public void Fill(object entity, DomainGraph graph)
        {
            var map = graph.MapFor(entity.GetType());
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