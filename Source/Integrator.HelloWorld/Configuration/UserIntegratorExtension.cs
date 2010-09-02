using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld.Configuration
{
    public class UserIntegratorExtension : IntegratorRegistryExtension
    {
        public UserIntegratorExtension()
        {
            Maps
                .Alter<User>()
                .Ignore(u => u.UserId);

            AutomatedTests
                .Configure<User>()
                .InsertWith<InsertUserWithBlogPostCommand>();
        }
    }
}