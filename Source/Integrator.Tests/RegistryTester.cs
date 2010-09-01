using Integrator.Registration;
using Integrator.Tests.Domain;
using NUnit.Framework;

namespace Integrator.Tests
{
    [TestFixture]
    public class RegistryTester
    {
        private DomainGraph _graph;
        [SetUp]
        public void SetUp()
        {
            _graph = new IntegratorTestRegistry().BuildGraph();
        }

        [Test]
        public void graph_should_contain_entity_map_for_each_entity_in_scanning_operation()
        {
            _graph
                .EntityMaps
                .ShouldHaveCount(2);
        }

        [Test]
        public void maps_reflect_alterations()
        {
            _graph
                .MapFor<BlogPost>()
                .MapFor(p => p.Author)
                .ShouldBeNull();
        }

        [Test]
        public void generated_user_should_not_be_null()
        {
            var user = _graph
                        .MapFor<User>()
                        .GeneratorPolicy
                        .Build(ValueRequest.For<User>())
                        .Generate();

            user.ShouldNotBeNull();
        }
    }
}