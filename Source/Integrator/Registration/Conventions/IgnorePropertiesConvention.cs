using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Integrator.Registration.Conventions
{
    public class IgnorePropertiesConvention : IConfigurationAction
    {
        private readonly Func<PropertyInfo, bool> _filter;

        public IgnorePropertiesConvention(Func<PropertyInfo, bool> filter)
        {
            _filter = filter;
        }

        public void Configure(DomainGraph graph)
        {
            graph
                .EntityMaps
                .Each<EntityMap>(map =>
                          {
                              var mapsToIgnore = Enumerable.ToList<PropertyMap>(map
                                                           .PropertyMaps
                                                           .Where(m => _filter(m.Request.Accessor().InnerProperty)));

                              foreach (var mapToIgnore in mapsToIgnore)
                              {
                                  map.IgnoreProperty(mapToIgnore.Request.Accessor().InnerProperty);
                              }
                          });
        }
    }
}