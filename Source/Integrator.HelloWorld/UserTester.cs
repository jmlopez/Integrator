using Commander;
using Integrator.HelloWorld.Configuration;
using Integrator.HelloWorld.Domain;
using NUnit.Framework;

namespace Integrator.HelloWorld
{
    [TestFixture]
    public class UserTester
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Integrator.Initialize(x => x.AddRegistry<HelloWorldStructureMapRegistry>(), new HelloWorldIntegratorRegistry());
        }

        [Test]
        public void user_can_be_persisted()
        {
            var result = Integrator.GenerateAndPersist(new AddBlogPostCommand());
            var retrievedUser = Integrator.Retrieve<User>(result.Entity.UserId);

            retrievedUser
                .FirstName
                .ShouldEqual(result.Entity.FirstName);

            retrievedUser
                .LastName
                .ShouldEqual(result.Entity.LastName);

            retrievedUser
                .Posts
                .ShouldHaveCount(1);
        }

        #region Nested Type: AddBlogPostCommand
        public class AddBlogPostCommand : IDomainCommand<User>
        {
            public void Execute(User entity)
            {
                var post = new BlogPost(entity);
                Integrator.Fill(post);

                entity.AddPost(post);
            }
        }
        #endregion
    }
}