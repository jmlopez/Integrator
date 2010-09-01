using System;
using Integrator.Generators;

namespace Integrator.Registration
{
    public interface IGeneratorRegistry
    {
        void Register(Type type, IGenerator generator);
    }
}