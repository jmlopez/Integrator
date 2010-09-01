using System;

namespace Integrator.Registration.Graph
{
    public class ConfiguredDependency : IDependency
    {
        public ObjectDef Definition { get; set; }
        public Type DependencyType { get; set; }


        public void AcceptVisitor(IDependencyVisitor visitor)
        {
            visitor.Configured(this);
        }

        public override string ToString()
        {
            return string.Format("DependencyType: {0}, Definition: {1}", DependencyType, Definition);
        }
    }
}