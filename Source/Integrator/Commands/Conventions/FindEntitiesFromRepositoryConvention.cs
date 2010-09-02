using System.Collections.Generic;
using Commander.Registration;
using Commander.Registration.Nodes;

namespace Integrator.Commands.Conventions
{
    public class FindEntitiesFromRepositoryConvention : IConfigurationAction
    {
        public void Configure(CommandGraph graph)
        {
            graph
                .ChainsForExisting
                .Each(chain =>
                          {
                              graph.Observer.RecordCallStatus(chain.Placeholder(), "Adding FindEntityCommand directly before placeholder");
                              chain.Placeholder().AddBefore(new Wrapper(typeof(FindEntityCommand<>).MakeGenericType(chain.EntityType)));
                          });
        }
    }
}