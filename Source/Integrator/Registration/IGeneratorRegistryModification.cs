namespace Integrator.Registration
{
    public interface IGeneratorRegistryModification
    {
        void Modify(DomainGraph graph, IGeneratorRegistry registry);
    }
}