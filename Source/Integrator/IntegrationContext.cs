using NUnit.Framework;
using StructureMap.Configuration.DSL;

namespace Integrator
{
    [TestFixture]
    public class IntegrationContext<TEntity, TStructureMapRegistry, TIntegratorRegistry, TDatabaseRegistry>
        where TEntity : class
        where TStructureMapRegistry : Registry, new()
        where TIntegratorRegistry : IntegratorRegistry, new()
        where TDatabaseRegistry : DatabaseRegistry, new()
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            IntegrationFactory.Initialize(x => x.AddRegistry<TStructureMapRegistry>(), new TIntegratorRegistry(), new TDatabaseRegistry());
        }

        [Test]
        public void entity_can_be_persisted_and_retrieved()
        {
            IntegrationFactory.Test<TEntity>();
        }
    }
}