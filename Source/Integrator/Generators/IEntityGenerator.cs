using Integrator.Registration;

namespace Integrator.Generators
{
    public interface IEntityGenerator : IGenerator
    {
        void Fill(object entity, DomainGraph graph);
    }
}