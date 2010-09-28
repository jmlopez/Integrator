namespace Integrator.Registration
{
    public interface ISystemConfigurationAction
    {
        void Configure(DomainGraph graph, IntegratorRegistry registry);
    }
}