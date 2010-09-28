using System;
using System.Linq;
using System.Reflection;
using Commander.Registration;
using Integrator.Generators;
using Integrator.Registration;
using Integrator.Tests.Domain;
using NUnit.Framework;

namespace Integrator.Tests
{
    [TestFixture]
    public class when_generating_an_entity_with_a_protected_constructor
    {
        [Test]
        public void default_generator_throws_an_error()
        {
            var registry = new IntegratorTestRegistry();
            var graph = registry.BuildGraph();

            Exception<MissingMethodException>
                .ShouldBeThrownBy(() => graph
                                            .MapFor<ComplexEntity>()
                                            .GeneratorPolicy
                                            .Build(ValueRequest.For<ComplexEntity>())
                                            .Generate());
        }

        [Test]
        public void system_policy_is_applied_for_protected_generation_policy()
        {
            var registry = new ComplexIntegratorTestRegistry();
            var graph = registry.BuildGraph();

            graph
                .MapFor<ComplexEntity>()
                .GeneratorPolicy
                .Build(ValueRequest.For<ComplexEntity>())
                .Generate()
                .ShouldNotBeNull();
        }


        #region Nested Types
        public class ComplexIntegratorTestRegistry : IntegratorTestRegistry
        {
            public ComplexIntegratorTestRegistry()
            {
                ApplySystemPolicy<ProtectedEntityConstructionSystemPolicy>();
            }
        }

        public class ProtectedEntityConstructionSystemPolicy : ISystemConfigurationAction
        {
            public void Configure(DomainGraph graph, IntegratorRegistry registry)
            {
                registry
                    .Generators
                    .ApplyPolicy(new ProtectedEntityGenerationPolicy(graph));
            }
        }

        public class ProtectedEntityGenerationPolicy : IGeneratorPolicy
        {
            private readonly DomainGraph _graph;

            public ProtectedEntityGenerationPolicy(DomainGraph graph)
            {
                _graph = graph;
            }

            public bool Matches(ValueRequest request)
            {
                var isEntity = request.Type.Namespace.StartsWith(typeof (EntityMarker).Namespace);
                var isPropertyRequest = request.Accessor() != null;
                return isEntity && !isPropertyRequest &&
                        request
                            .Type
                            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                            .Length
                            .Equals(0);
            }

            public IGenerator Build(ValueRequest request)
            {
                return new ProtectedEntityGenerator(request.Type, _graph);
            }
        }

        public class ProtectedEntityGenerator : DefaultEntityGenerator
        {
            public ProtectedEntityGenerator(Type entityType, DomainGraph graph) 
                : base(entityType, graph)
            {
            }

            public override object CreateEntity()
            {
                return _entityType
                        .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                        .First()
                        .Invoke(new object[0]);
            }
        }
        #endregion
    }
}