namespace Integrator.HelloWorld.Domain
{
    public class BlogPost
    {
        public BlogPost(User author)
        {
            Author = author;
        }

        public int PostId { get; private set; }
        public User Author { get; private set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}