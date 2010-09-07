using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using Integrator.Infrastructure;
using NUnit.Framework;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class UserTester : EntityIntegrationContext<User, HelloWorldStructureMapRegistry, HelloWorldIntegratorRegistry>
    {
        protected override void ConfigureDatabase(DatabaseExpression expression)
        {
            expression
                .AutoDrop(true)
                .Use("HelloWorld")
                .ConnectWith("Integrator");
        }

        protected override void AfterTest()
        {
            Container
                .GetInstance<IRepository>()
                .GetAll<User>()
                .ShouldHaveCount(0);
        }
    }
}