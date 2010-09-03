using Commander;
using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using Integrator.Infrastructure;
using NUnit.Framework;
using StructureMap;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class UserTester : IntegrationContext<User, HelloWorldStructureMapRegistry, HelloWorldIntegratorRegistry, HelloWorldDbRegistry>
    {
        protected override void AfterTest()
        {
            ObjectFactory
                .GetInstance<IRepository>()
                .GetAll<User>()
                .ShouldHaveCount(0);
        }
    }

    public class InsertUserWithBlogPostCommand : IDomainCommand<User>
    {
        public void Execute(User entity)
        {
            var post = new BlogPost(entity);
            IntegrationFactory.Fill(post);

            entity.AddPost(post);
        }
    }
}