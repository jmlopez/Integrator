using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Integrator
{
    /// <summary>
    /// Provides a context for all integration tests.
    /// </summary>
    /// <typeparam name="TStructureMapRegistry">The type of registry used to bootstrap StructureMap.</typeparam>
    /// <typeparam name="TIntegratorRegistry">The type of registry used to bootstrap Integrator.</typeparam>
    [TestFixture]
    public class IntegrationContext<TStructureMapRegistry, TIntegratorRegistry>
        where TStructureMapRegistry : Registry, new()
        where TIntegratorRegistry : IntegratorRegistry, new()
    {
        /// <summary>
        /// Sets up the test fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            IntegrationFactory.Initialize(x => x.AddRegistry<TStructureMapRegistry>(), new TIntegratorRegistry(), ConfigureDatabase);
            BeforeAll();
        }
        /// <summary>
        /// Tears down up the test fixture.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            AfterAll();
        }
        /// <summary>
        /// Gets the container configured for the context.
        /// </summary>
        public IContainer Container { get { return IntegrationFactory.Runner.Container; } }
        /// <summary>
        /// Called by the NUnit framework to perform setup tasks.
        /// Context-specific setup tasks should be implemented by overriding the <see cref="BeforeEach"/> method.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            BeforeEach();
        }
        /// <summary>
        /// Called by the NUnit framework to perform tear down tasks.
        /// Context-specific tear down tasks should be implemented by overriding the <see cref="AfterEach"/> method.
        /// </summary>
        [SetUp]
        public void TearDown()
        {
            AfterEach();
        }
        /// <summary>
        /// Called after fixture setup tasks are performed in the <see cref="TestFixtureSetUp"/> method.
        /// </summary>
        protected virtual void BeforeAll()
        {
        }
        /// <summary>
        /// Called after fixture tear down tasks are performed in the <see cref="TestFixtureTearDown"/> method.
        /// </summary>
        protected virtual void AfterAll()
        {
        }
        /// <summary>
        /// Called after setup tasks are performed in the <see cref="SetUp"/> method.
        /// </summary>
        protected virtual void BeforeEach()
        {
        }
        /// <summary>
        /// Called after tear down tasks are performed in the <see cref="TearDown"/> method.
        /// </summary>
        protected virtual void AfterEach()
        {
        }
        /// <summary>
        /// Called during fixture setup to configure the database connection (auto-drop, how to connect, etc).
        /// </summary>
        /// <param name="expression"></param>
        protected virtual void ConfigureDatabase(DatabaseExpression expression)
        {
        }
    }
}