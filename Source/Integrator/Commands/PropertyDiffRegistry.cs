using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Integrator.Commands
{
    public class PropertyDiffRegistry : IPropertyDiffRegistry
    {
        private readonly List<IPropertyDiffStrategy> _strategies;

        public PropertyDiffRegistry()
        {
            _strategies = new List<IPropertyDiffStrategy>();
        }

        public void Register(IPropertyDiffStrategy strategy)
        {
            _strategies.Fill(strategy);
        }


        public IPropertyDiffStrategy StrategyFor(PropertyInfo property)
        {
            return _strategies.Last(strategy => strategy.Matches(property));
        }
    }
}