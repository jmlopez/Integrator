using System;

namespace Integrator.Registration.Graph
{
    public interface IDependency
    {
        Type DependencyType { get; }
        void AcceptVisitor(IDependencyVisitor visitor);
    }
}