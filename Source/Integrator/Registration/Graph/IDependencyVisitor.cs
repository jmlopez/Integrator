namespace Integrator.Registration.Graph
{
    public interface IDependencyVisitor
    {
        void Value(ValueDependency dependency);
        void Configured(ConfiguredDependency dependency);
    }
}