using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Integrator.Registration.Dsl
{
    public class TypeMatcher : ITypeMatcher
    {
        private readonly TypePool _types;
        private readonly CompositeFilter<Type> _typeFilters;

        public TypeMatcher(TypePool types)
        {
            _types = types;
            _typeFilters = new CompositeFilter<Type>();
        }

        public CompositeFilter<Type> TypeFilters
        {
            get { return _typeFilters; }
        }

        public IEnumerable<Type> Matches()
        {
            return _types.TypesMatching(TypeFilters.Matches);
        }
    }
}