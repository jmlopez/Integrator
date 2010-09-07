using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using Integrator.HelloWorld.Repositories;
using NUnit.Framework;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class UserRepositoryTester : IntegrationContext<HelloWorldStructureMapRegistry, HelloWorldIntegratorRegistry>
    {
        protected override void ConfigureDatabase(DatabaseExpression expression)
        {
            expression
                .UseDefaultConfiguration();
        }

        [Test]
        public void repository_returns_all_users()
        {
            var count = 50;
            for (int i = 0; i < 50; ++i)
            {
                IntegrationFactory.GenerateAndPersist<User>();
            }

            Container
                .GetInstance<UserRepository>()
                .GetAll()
                .ShouldHaveCount(count);
        }
    }
}