using NUnit.Framework;
using StructureMap.Configuration.DSL;

namespace Integrator
{
    [TestFixture]
    public class IntegrationContext<TEntity, TStructureMapRegistry, TIntegratorRegistry>
        where TEntity : class
        where TStructureMapRegistry : Registry, new()
        where TIntegratorRegistry : IntegratorRegistry, new()
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            IntegrationFactory.Initialize(x => x.AddRegistry<TStructureMapRegistry>(), new TIntegratorRegistry());
        }

        [Test]
        public void entity_can_be_persisted_and_retrieved()
        {
            IntegrationFactory.Test<TEntity>();
        }
    }
}