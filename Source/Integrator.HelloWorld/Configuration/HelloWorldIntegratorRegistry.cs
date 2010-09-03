using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld.Configuration
{
    public class HelloWorldIntegratorRegistry : IntegratorRegistry
    {
        public HelloWorldIntegratorRegistry()
        {
            Applies
               .ToThisAssembly();

            Entities
                .IncludedTypesInNamespaceContaining<EntityMarker>()
                .Exclude<EntityMarker>();

            Maps
                .IgnoreCollections()
                .IgnoreProperties(p => p.Name.EndsWith("Id"));
        }
    }
}