using System;
using FubuCore.Util;

namespace Integrator.Registration.Dsl
{
    public interface ITypeMatcher
    {
        CompositeFilter<Type> TypeFilters { get; }
    }
}