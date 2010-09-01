using System.Collections.Generic;

namespace Integrator.Registration.Dsl
{
    public class GeneratorRegistryExpression
    {
        private readonly IList<IGeneratorRegistryModification> _generatorModifications;

        public GeneratorRegistryExpression(IList<IGeneratorRegistryModification> generatorModifications)
        {
            _generatorModifications = generatorModifications;
        }

        public GeneratorRegistryExpression ModifyWith<TModification>()
            where TModification : IGeneratorRegistryModification, new()
        {
            _generatorModifications.Add(new TModification());
            return this;
        }

        public GeneratorRegistryExpression ModifyWith<TModification>(TModification modification)
            where TModification : IGeneratorRegistryModification
        {
            _generatorModifications.Add(modification);
            return this;
        }
    }
}