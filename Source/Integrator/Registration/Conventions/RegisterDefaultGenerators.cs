using System.Collections.Generic;
using Integrator.Generators;
using Integrator.Registration.Dsl;

namespace Integrator.Registration.Conventions
{
    public class RegisterDefaultGenerators : TypeMatcher, IGeneratorRegistryModification
    {
        private readonly TypeCandidateExpression _typeExpression;
        public RegisterDefaultGenerators(TypePool types)
            : base(types)
        {
            _typeExpression = new TypeCandidateExpression(this, types, new NulloTypeCandidateExpression());
            _typeExpression.IncludedTypesInNamespaceContaining<IGenerator>();
            _typeExpression.IncludeTypesImplementing<IGenerator>();
            _typeExpression.ExcludeTypes(t => t.IsInterface || typeof (IEntityGenerator).IsAssignableFrom(t));
        }

        public void Modify(DomainGraph graph, IGeneratorRegistry registry)
        {
            Matches()
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