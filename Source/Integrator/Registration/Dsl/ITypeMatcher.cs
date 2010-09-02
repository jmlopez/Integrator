using System;
using System.Collections.Generic;
using FubuCore.Util;

namespace Integrator.Registration.Dsl
{
    public interface ITypeMatcher
    {
        CompositeFilter<Type> TypeFilters { get; }
        IEnumerable<Type> Matches();
    }
}