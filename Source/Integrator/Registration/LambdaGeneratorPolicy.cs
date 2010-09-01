using System;
using Integrator.Generators;

namespace Integrator.Registration
{
    public class LambdaGeneratorPolicy : IGeneratorPolicy
    {
        private readonly Func<ValueRequest, bool> _matches;
        private readonly IGenerator _generator;
        public LambdaGeneratorPolicy(Func<ValueRequest, bool> matches, IGenerator generator)
        {
            _matches = matches;
            _generator = generator;
        }

        public bool Matches(ValueRequest request)
        {
            return _matches(request);
        }

        public IGenerator Build(ValueRequest request)
        {
            return _generator;
        }
    }
}