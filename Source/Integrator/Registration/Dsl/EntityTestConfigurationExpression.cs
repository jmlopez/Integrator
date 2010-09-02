using System.Collections.Generic;
using Commander;

namespace Integrator.Registration.Dsl
{
    public class EntityTestConfigurationExpression<TEntity>
        where TEntity : class
    {
        private readonly EntityTestConfiguration _testConfiguration;
        public EntityTestConfigurationExpression(IList<IConfigurationAction> testActions)
        {
            _testConfiguration = new EntityTestConfiguration(typeof(TEntity));
            testActions.Add(new LambdaConfigurationAction(graph =>
                                                              {
                                                                  var map = graph.MapFor<TEntity>();
                                                                  if(map.TestConfiguration == null)
                                                                  {
                                                                      map.Configure(_testConfiguration);
                                                                      return;
                                                                  }

                                                                  if(_testConfiguration.VerificationCommandType != null)
                                                                  {
                                                                      map
                                                                          .TestConfiguration
                                                                          .VerifyWith(_testConfiguration.VerificationCommandType);
                                                                  }

                                                                  if (_testConfiguration.CommandType != null)
                                                                  {
                                                                      map
                                                                          .TestConfiguration
                                                                          .InsertWith(_testConfiguration.CommandType);
                                                                  }
                                                              }));
        }

        public EntityTestConfigurationExpression<TEntity> InsertWith<TCommand>()
            where TCommand : IDomainCommand<TEntity>
        {
            _testConfiguration.InsertWith(typeof(TCommand));
            return this;
        }

        public EntityTestConfigurationExpression<TEntity> VerifyWith<TCommand>()
            where TCommand : IVerificationCommand<TEntity>
        {
            _testConfiguration.VerifyWith(typeof(TCommand));
            return this;
        }
    }
}