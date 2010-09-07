using Commander;
using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld
{
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