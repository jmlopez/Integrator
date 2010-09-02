using System;
using System.Linq.Expressions;

namespace Integrator.Registration.Dsl
{
    public class NulloTypeCandidateExpression : ITypeCandidateExpression
    {
        public ITypeCandidateExpression ExcludeTypes(Expression<Func<Type, bool>> filter)
        {
            return this;
        }

        public ITypeCandidateExpression Exclude<T>()
        {
            return this;
        }

        public ITypeCandidateExpression IncludeTypesNamed(Expression<Func<string, bool>> filter)
        {
            return this;
        }

        public ITypeCandidateExpression IncludedTypesInNamespaceContaining<T>()
        {
            return this;
        }

        public ITypeCandidateExpression IncludeTypes(Expression<Func<Type, bool>> filter)
        {
            return this;
        }

        public ITypeCandidateExpression IncludeTypesImplementing<T>()
        {
            return this;
        }

        public ITypeCandidateExpression IncludeTypesClosing(Type openType)
        {
            return this;
        }

        public ITypeCandidateExpression ExcludeNonConcreteTypes()
        {
            return this;
        }

        public ITypeCandidateExpression IncludeType<T>()
        {
            return this;
        }
    }
}