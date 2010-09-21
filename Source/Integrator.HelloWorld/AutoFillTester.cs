using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using NUnit.Framework;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class AutoFillTester : IntegrationContext<HelloWorldStructureMapRegistry, HelloWorldIntegratorRegistry>
    {
        protected override void ConfigureDatabase(DatabaseExpression expression)
        {
            expression
                .UseDefaultConfiguration();
        }

        [Test]
        public void entity_can_be_generated_by_auto_fill()
        {
            var newEntity = IntegrationFactory.AutoFill<User>();

            newEntity
                .Posts
                .ShouldHaveCount(1);
        }

        [Test]
        public void entity_can_be_filled_by_auto_fill()
        {
            var existingEntity = new User();
            IntegrationFactory.AutoFill(existingEntity);

            existingEntity
                .Posts
                .ShouldHaveCount(1);
        }

        [Test]
        public void auto_filled_entities_can_persist()
        {
            var newEntity = IntegrationFactory.AutoFill<User>();
            IntegrationFactory.Persist(newEntity);

            var existingEntity = new User();
            IntegrationFactory.AutoFill(existingEntity);
            IntegrationFactory.Persist(existingEntity);
        }
    }
}