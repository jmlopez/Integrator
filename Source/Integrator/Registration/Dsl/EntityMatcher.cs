using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Integrator.Registration.Dsl
{
    public class EntityMatcher : ITypeMatcher
    {
        private readonly CompositeFilter<Type> _typeFilters = new CompositeFilter<Type>();
        public CompositeFilter<Type> TypeFilters { get { return _typeFilters; } }

        public void BuildMaps(TypePool pool, DomainGraph graph)
        {
            pool
                .TypesMatching(TypeFilters.Matches)
                .Each(t => graph.AddEntityMap(EntityMap.For(t)));
        }
    }
}