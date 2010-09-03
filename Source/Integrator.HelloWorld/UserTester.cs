using Commander;
using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using NUnit.Framework;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class UserTester : IntegrationContext<User, HelloWorldStructureMapRegistry, HelloWorldIntegratorRegistry, HelloWorldDbRegistry>
    {
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