using System;
using System.Linq.Expressions;

namespace Integrator.Registration.Dsl
{
    public interface ITypeCandidateExpression
    {
        ITypeCandidateExpression ExcludeTypes(Expression<Func<Type, bool>> filter);
        ITypeCandidateExpression Exclude<T>();
        ITypeCandidateExpression IncludeTypesNamed(Expression<Func<string, bool>> filter);
        ITypeCandidateExpression IncludedTypesInNamespaceContaining<T>();
        ITypeCandidateExpression IncludeTypes(Expression<Func<Type, bool>> filter);
        ITypeCandidateExpression IncludeTypesImplementing<T>();
        ITypeCandidateExpression IncludeTypesClosing(Type openType);
        ITypeCandidateExpression ExcludeNonConcreteTypes();
        ITypeCandidateExpression IncludeType<T>();
    }
}