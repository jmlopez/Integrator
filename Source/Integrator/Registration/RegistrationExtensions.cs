using System.Collections.Generic;

namespace Integrator.Registration
{
    public static class RegistrationExtensions
    {
        public static void Configure(this IEnumerable<IConfigurationAction> actions, DomainGraph graph)
        {
            actions.Each(x => x.Configure(graph));
        }
    }
}