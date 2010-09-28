using Integrator.Generators;
using Integrator.Registration.Conventions;

namespace Integrator.Registration.Dsl
{
    public class GeneratorExpression
    {
        private readonly GeneratorResolver _generatorResolver;

        public GeneratorExpression(GeneratorResolver generatorResolver)
        {
            _generatorResolver = generatorResolver;
        }

        public GeneratorExpression ApplyPolicy<TPolicy>()
            where TPolicy : IGeneratorPolicy, new()
        {
            return ApplyPolicy(new TPolicy());
        }

        public GeneratorExpression ApplyPolicy(IGeneratorPolicy policy)
        {
            _generatorResolver.RegisterGeneratorPolicy(policy);
            return this;
        }

        public GeneratorExpression GenerateWith<T, TGenerator>()
            where T : class
            where TGenerator : IGenerator, new()
        {
            _generatorResolver.RegisterGeneratorPolicy(new LambdaGeneratorPolicy(r => r.Type == typeof(T), new TGenerator()));
            return this;
        }
    }
}