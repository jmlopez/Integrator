using Integrator.Tests.Domain;

namespace Integrator.Tests
{
    public class IntegratorTestRegistry: IntegratorRegistry
    {
        public IntegratorTestRegistry()
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