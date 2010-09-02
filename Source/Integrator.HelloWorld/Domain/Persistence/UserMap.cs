using FluentNHibernate.Mapping;

namespace Integrator.HelloWorld.Domain.Persistence
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.UserId)
                .GeneratedBy
                .Identity();

            Map(u => u.FirstName)
                .Not
                .Nullable();

            Map(u => u.LastName)
                .Not
                .Nullable();

            HasMany(u => u.Posts)
                .AsBag()
                .Cascade
                .All();
        }
    }

    public class BlogPostMap : ClassMap<BlogPost>
    {
        public BlogPostMap()
        {
            Id(p => p.PostId)
                .GeneratedBy
                .Identity();

            Map(p => p.Body)
                .Not
                .Nullable();

            Map(p => p.Title)
                .Not
                .Nullable();

            References(p => p.Author);
        }
    }
}