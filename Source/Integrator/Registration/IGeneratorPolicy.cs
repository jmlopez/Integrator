using Integrator.Generators;

namespace Integrator.Registration
{
    public interface IGeneratorPolicy
    {
        bool Matches(ValueRequest request);
        IGenerator Build(ValueRequest request);
    }
}