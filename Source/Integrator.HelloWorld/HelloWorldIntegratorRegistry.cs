using System;
using Integrator.Generators;
using Integrator.HelloWorld.Domain;
using Integrator.Registration;

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

            Generators
                .ApplyPolicy<MyGeneratorPolicy>();

            Maps
                .Alter<BlogPost>()
                .Ignore(p => p.Author);
        }
    }

    public class MyGeneratorPolicy : IGeneratorPolicy
    {
        public bool Matches(ValueRequest request)
        {
            return true;
        }

        public IGenerator Build(ValueRequest request)
        {
            throw new NotImplementedException();
        }
    }
}