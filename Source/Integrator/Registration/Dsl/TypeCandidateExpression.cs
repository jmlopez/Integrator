using System;
using System.Linq.Expressions;
using FubuCore;

namespace Integrator.Registration.Dsl
{
    public class TypeCandidateExpression : ITypeCandidateExpression
    {
        private readonly ITypeMatcher _matcher;
        private readonly TypePool _types;
        private readonly ITypeCandidateExpression _inner;

        public TypeCandidateExpression(ITypeMatcher matcher, TypePool types, ITypeCandidateExpression inner)
        {
            _matcher = matcher;
            _types = types;
            _inner = inner;
        }

        public ITypeCandidateExpression ExcludeTypes(Expression<Func<Type, bool>> filter)
        {
            _inner.ExcludeTypes(filter);
            _matcher.TypeFilters.Excludes += filter;
            return this;
        }

        public ITypeCandidateExpression Exclude<T>()
        {
            _inner.Exclude<T>();
            _matcher.TypeFilters.Excludes += (type => type.Equals(typeof(T)));
            return this;
        }

        public ITypeCandidateExpression IncludeTypesNamed(Expression<Func<string, bool>> filter)
        {
            _inner.IncludeTypesNamed(filter);

            var typeParam = Expression.Parameter(typeof(Type), "type"); // type =>
            var nameProp = Expression.Property(typeParam, "Name");  // type.Name
            var invokeFilter = Expression.Invoke(filter, nameProp); // filter(type.Name)
            var lambda = Expression.Lambda<Func<Type, bool>>(invokeFilter, typeParam); // type => filter(type.Name)

            return IncludeTypes(lambda);
        }

        public ITypeCandidateExpression IncludedTypesInNamespaceContaining<T>()
        {
            _inner.IncludedTypesInNamespaceContaining<T>();
            _matcher.TypeFilters.Includes += (type => type.Namespace == typeof (T).Namespace);
            return this;
        }

        public ITypeCandidateExpression IncludeTypes(Expression<Func<Type, bool>> filter)
        {
            _inner.IncludeTypes(filter);
            _matcher.TypeFilters.Includes += filter;
            return this;
        }

        public ITypeCandidateExpression IncludeTypesImplementing<T>()
        {
            _inner.IncludeTypesImplementing<T>();
            return IncludeTypes(type => !type.IsOpenGeneric() && type.IsConcreteTypeOf<T>());
        }

        public ITypeCandidateExpression IncludeTypesClosing(Type openType)
        {
            _inner.IncludeTypesClosing(openType);

            if (!openType.IsOpenGeneric())
            {
                throw new ApplicationException("This scanning operation can only be used with open generic types");
            }

            return IncludeTypes(type => type.ImplementsInterfaceTemplate(openType));
        }

        public ITypeCandidateExpression ExcludeNonConcreteTypes()
        {
            _inner.ExcludeNonConcreteTypes();

            _matcher.TypeFilters.Excludes += type => !type.IsConcrete();
            return this;
        }

        public ITypeCandidateExpression IncludeType<T>()
        {
            _inner.IncludeType<T>();

            _types.AddType(typeof(T));
            _matcher.TypeFilters.Includes += type => type == typeof(T);
            return this;
        }
    }
}