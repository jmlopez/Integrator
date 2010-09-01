using System;
using FubuCore.Util;
using Integrator.Generators;

namespace Integrator.Registration
{
    public class DefaultGeneratorRegistry : IGeneratorPolicy, IGeneratorRegistry
    {
        private readonly Cache<Type, IGenerator> _generators;

        public DefaultGeneratorRegistry()
        {
            _generators = new Cache<Type, IGenerator>
                              {
                                  OnMissing = type =>
                                                  {
                                                      throw new IntegratorException(1002,
                                                                                    "No generator specified for {0}",
                                                                                    type.Name);
                                                  }

                              };
        }

        public void Register(Type type, IGenerator generator)
        {
            _generators.Fill(type, generator);
        }

        public bool Matches(ValueRequest request)
        {
            return true;
        }

        public IGenerator Build(ValueRequest request)
        {
            var type = request.Accessor() == null ? request.Type : request.Accessor().PropertyType;
            return _generators[type];
        }
    }
}