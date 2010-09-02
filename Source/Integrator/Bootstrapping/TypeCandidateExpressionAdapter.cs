using System;
using System.Linq.Expressions;
using Integrator.Registration.Dsl;
using TypeCandidateExpression = Commander.Registration.Dsl.TypeCandidateExpression;

namespace Integrator.Bootstrapping
{
    public class TypeCandidateExpressionAdapter : ITypeCandidateExpression
    {
        private readonly TypeCandidateExpression _inner;

        public TypeCandidateExpressionAdapter(TypeCandidateExpression inner)
        {
            _inner = inner;
        }

        public ITypeCandidateExpression ExcludeTypes(Expression<Func<Type, bool>> filter)
        {
            _inner.ExcludeTypes(filter);
            return this;
        }

        public ITypeCandidateExpression Exclude<T>()
        {
            _inner.Exclude<T>();
            return this;
        }

        public ITypeCandidateExpression IncludeTypesNamed(Expression<Func<string, bool>> filter)
        {
            _inner.IncludeTypesNamed(filter);
            return this;
        }

        public ITypeCandidateExpression IncludedTypesInNamespaceContaining<T>()
        {
            _inner.IncludedTypesInNamespaceContaining<T>();
            return this;
        }

        public ITypeCandidateExpression IncludeTypes(Expression<Func<Type, bool>> filter)
        {
            _inner.IncludeTypes(filter);
            return this;
        }

        public ITypeCandidateExpression IncludeTypesImplementing<T>()
        {
            _inner.IncludeTypesImplementing<T>();
            return this;
        }

        public ITypeCandidateExpression IncludeTypesClosing(Type openType)
        {
            _inner.IncludeTypesClosing(openType);
            return this;
        }

        public ITypeCandidateExpression ExcludeNonConcreteTypes()
        {
            _inner.ExcludeNonConcreteTypes();
            return this;
        }

        public ITypeCandidateExpression IncludeType<T>()
        {
            _inner.IncludeType<T>();
            return this;
        }
    }
}