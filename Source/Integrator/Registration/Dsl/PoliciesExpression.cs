using System.Collections.Generic;
using System.Linq;

namespace Integrator.Registration.Dsl
{
    public class PoliciesExpression
    {
        private readonly IList<IConfigurationAction> _actions;

        public PoliciesExpression(IList<IConfigurationAction> actions)
        {
            _actions = actions;
        }

        public PoliciesExpression Add(IConfigurationAction alteration)
        {
            _actions.Fill(alteration);
            return this;
        }

        public PoliciesExpression Add<T>() 
            where T : IConfigurationAction, new()
        {
            if (_actions.Any(x => x is T))
            {
                return this;
            }

            return Add(new T());
        }
    }
}