using System.Collections.Generic;
using FubuCore;
using Integrator.Registration;
using Integrator.Registration.Dsl;

namespace Integrator
{
    public class RegistryMatcher : TypeMatcher
    {
        private readonly IList<IntegratorRegistry> _imports;
        
        public RegistryMatcher(TypePool types, IList<IntegratorRegistry> imports) 
            : base(types)
        {
            _imports = imports;
        }

        public void ImportAll(DomainGraph graph)
        {
            Matches()
                .Each(type => _imports.Add(type.GetDefaultInstance().As<IntegratorRegistry>()));

            _imports.Each(registry => graph.Import(registry.BuildGraph()));
        }
    }
}