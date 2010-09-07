using NUnit.Framework;
using StructureMap.Configuration.DSL;

namespace Integrator
{
    /// <summary>
    /// Provides a context for entity integration tests.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to test.</typeparam>
    /// <typeparam name="TStructureMapRegistry">The type of registry used to bootstrap StructureMap.</typeparam>
    /// <typeparam name="TIntegratorRegistry">The type of registry used to bootstrap Integrator.</typeparam>
    [TestFixture]
    public class EntityIntegrationContext<TEntity, TStructureMapRegistry, TIntegratorRegistry> : IntegrationContext<TStructureMapRegistry, TIntegratorRegistry>
        where TEntity : class
        where TStructureMapRegistry : Registry, new()
        where TIntegratorRegistry : IntegratorRegistry, new()
    {
        /// <summary>
        /// Asserts that the entity can be persisted and retrieved.
        /// </summary>
        [Test]
        public void entity_can_be_persisted_and_retrieved()
        {
            IntegrationFactory.Test<TEntity>();
            AfterTest();
        }
        /// <summary>
        /// Called after the base entity persistence test is run.
        /// </summary>
        protected virtual void AfterTest()
        {
        }
    }
}