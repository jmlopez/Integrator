using System.Collections.Generic;
using Integrator.HelloWorld.Domain;

namespace Integrator.HelloWorld.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}