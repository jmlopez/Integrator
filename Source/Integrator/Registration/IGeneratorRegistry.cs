using System;
using Integrator.Generators;

namespace Integrator.Registration
{
    public interface IGeneratorRegistry
    {
        bool Has(Type type);
        void Register(Type type, IGenerator generator);
    }
}