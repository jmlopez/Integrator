using System;
using System.Collections.Generic;
using System.Reflection;
using Integrator.Registration.Conventions;

namespace Integrator.Registration.Dsl
{
    public class MapAlterationExpression
    {
        private readonly IList<IConfigurationAction> _policies;

        public MapAlterationExpression(IList<IConfigurationAction> policies)
        {
            _policies = policies;
        }

        public EntityMapAlterationExpression<TEntity> Alter<TEntity>()
            where TEntity : class
        {
            return new EntityMapAlterationExpression<TEntity>(_policies);
        }

        public MapAlterationExpression IgnoreCollections()
        {
            _policies.Add(new IgnoreCollectionsConvention());
            return this;
        }

        public MapAlterationExpression IgnoreProperties(Func<PropertyInfo, bool> predicate)
        {
            _policies.Add(new IgnorePropertiesConvention(predicate));
            return this;
        }
    }
}