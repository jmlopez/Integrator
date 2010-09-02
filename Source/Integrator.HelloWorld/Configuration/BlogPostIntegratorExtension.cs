using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld.Configuration
{
    public class BlogPostIntegratorExtension : IntegratorRegistryExtension
    {
        public BlogPostIntegratorExtension()
        {
            Maps
                .Alter<BlogPost>()
                .Ignore(p => p.PostId)
                .Ignore(p => p.Author);
        }
    }
}