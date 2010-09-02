using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld
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
                .IgnoreCollections();

            Maps
                .Alter<BlogPost>()
                .Ignore(p => p.PostId)
                .Ignore(p => p.Author);

            Maps
                .Alter<User>()
                .Ignore(u => u.UserId);
        }
    }
}