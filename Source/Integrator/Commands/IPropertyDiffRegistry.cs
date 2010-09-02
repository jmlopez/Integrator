using System;
using System.Reflection;

namespace Integrator.Commands
{
    public interface IPropertyDiffRegistry
    {
        IPropertyDiffStrategy StrategyFor(PropertyInfo property);
    }
}