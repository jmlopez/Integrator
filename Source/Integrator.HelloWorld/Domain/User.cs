using System.Collections.Generic;

namespace Integrator.HelloWorld.Domain
{
    public class User
    {
        private readonly IList<BlogPost> _posts;

        public User()
        {
            _posts = new List<BlogPost>();
        }
        public virtual int UserId { get; private set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual IEnumerable<BlogPost> Posts { get { return _posts; } }

        public virtual void AddPost(BlogPost post)
        {
            _posts.Add(post);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (User)) return false;
            return Equals((User) obj);
        }

        public virtual bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.UserId == UserId;
        }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}