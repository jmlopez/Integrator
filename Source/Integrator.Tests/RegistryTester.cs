using System.Collections.Generic;
using Commander.Registration;
using Commander.StructureMap;
using Integrator.Registration;
using Integrator.Tests.Domain;
using NUnit.Framework;
using StructureMap;

namespace Integrator.Tests
{
    [TestFixture]
    public class RegistryTester
    {
        private DomainGraph _graph;
        private CommandGraph _commandGraph;
        [SetUp]
        public void SetUp()
        {
            var registry = new IntegratorTestRegistry();
            _graph = registry.BuildGraph();

            var facility = new StructureMapContainerFacility(ObjectFactory.Container);
            _commandGraph = registry.CommandRegistry.BuildGraph(facility.BuildEntityBuilderFactory());
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
        public void command_graph_should_contain_chains_for_each_entity()
        {
            _graph
                .EntityMaps
                .Each(map =>
                          {
                              _commandGraph
                                  .ChainForNew(map.EntityType)
                                  .ShouldNotBeNull();

                              _commandGraph
                                  .ChainForExisting(map.EntityType)
                                  .ShouldNotBeNull();
                          });
        }
    }
}