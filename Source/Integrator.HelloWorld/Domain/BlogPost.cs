namespace Integrator.HelloWorld.Domain
{
    public class BlogPost
    {
        protected BlogPost() { }
        public BlogPost(User author)
        {
            Author = author;
        }

        public virtual int PostId { get; private set; }
        public virtual User Author { get; private set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (BlogPost)) return false;
            return Equals((BlogPost) obj);
        }

        public virtual bool Equals(BlogPost other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.PostId == PostId;
        }

        public override int GetHashCode()
        {
            return PostId;
        }
    }
}