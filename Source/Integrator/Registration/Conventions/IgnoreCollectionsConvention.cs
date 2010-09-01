using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Integrator.Registration.Conventions
{
    public class IgnoreCollectionsConvention : IConfigurationAction
    {
        public void Configure(DomainGraph graph)
        {
            graph
                .EntityMaps
                .Each<EntityMap>(map =>
                          {
                              var mapsToIgnore = map
                                                    .PropertyMaps
                                                    .Where(m => typeof (IEnumerable).IsAssignableFrom(m.PropertyType()) && m.PropertyType() != typeof(string))
                                                    .ToList();

                              foreach (var mapToIgnore in mapsToIgnore)
                              {
                                  map.IgnoreProperty(mapToIgnore.Request.Accessor().InnerProperty);
                              }
                          });
        }
    }
}