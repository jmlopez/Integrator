using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld.Configuration
{
    public class UserIntegratorExtension : IntegratorRegistryExtension
    {
        public UserIntegratorExtension()
        {
            AutomatedTests
                .Configure<User>()
                .InsertWith<InsertUserWithBlogPostCommand>()
                .AutoDelete();
        }
    }
}