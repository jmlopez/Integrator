using System;
using System.Collections.Generic;
using FubuCore.Util;
using Integrator.Generators;
using Integrator.Registration.Dsl;

namespace Integrator.Registration.Conventions
{
    public class RegisterDefaultGenerators : IGeneratorRegistryModification, ITypeMatcher
    {
        private readonly CompositeFilter<Type> _typeFilters = new CompositeFilter<Type>();
        private readonly TypeCandidateExpression _typeExpression;
        private readonly TypePool _types;

        public RegisterDefaultGenerators(TypePool types)
        {
            _types = types;
            _typeExpression = new TypeCandidateExpression(this, _types);
            _typeExpression.IncludedTypesInNamespaceContaining<IGenerator>();
            _typeExpression.IncludeTypesImplementing<IGenerator>();
            _typeExpression.ExcludeTypes(t => t.IsInterface || typeof (IEntityGenerator).IsAssignableFrom(t));
        }

        public CompositeFilter<Type> TypeFilters
        {
            get { return _typeFilters; }
        }

        public void Modify(DomainGraph graph, IGeneratorRegistry registry)
        {
            _types
                .TypesMatching(TypeFilters.Matches)
                .Each(type =>
                          {
                              var targetType = type.GetGeneratorType();
                              if(targetType == null)
                              {
                                  return;
                              }

                              registry.Register(targetType, (IGenerator)type.GetDefaultInstance());
                          });
        }
    }
}