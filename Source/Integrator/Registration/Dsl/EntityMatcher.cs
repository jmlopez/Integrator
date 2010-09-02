using System.Collections.Generic;

namespace Integrator.Registration.Dsl
{
    public class EntityMatcher : TypeMatcher
    {
        public EntityMatcher(TypePool types) 
            : base(types)
        {
        }

        public void BuildMaps(DomainGraph graph)
        {
            Matches()
                .Each(t => graph.AddEntityMap(EntityMap.For(t)));
        }
    }
}