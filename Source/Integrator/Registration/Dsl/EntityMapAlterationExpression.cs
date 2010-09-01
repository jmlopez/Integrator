using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Integrator.Registration.Dsl
{
    public class EntityMapAlterationExpression<TEntity>
        where TEntity : class
    {
        private readonly IList<IConfigurationAction> _policies;

        public EntityMapAlterationExpression(IList<IConfigurationAction> policies)
        {
            _policies = policies;
        }

        public EntityMapAlterationExpression<TEntity> Ignore(Expression<Func<TEntity, object>> propertyExpression)
        {
            _policies.Add(new LambdaConfigurationAction(graph =>
                                                            {
                                                                var map = graph.MapFor<TEntity>();
                                                                if(map == null)
                                                                {
                                                                    return;
                                                                }

                                                                map.Ignore(propertyExpression);
                                                            }));
            return this;
        }
    }
}