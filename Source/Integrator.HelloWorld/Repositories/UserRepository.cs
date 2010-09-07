using System.Collections.Generic;
using Integrator.HelloWorld.Domain;
using NHibernate;

namespace Integrator.HelloWorld.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<User> GetAll()
        {
            return _session
                .CreateCriteria<User>()
                .List<User>();
        }
    }
}