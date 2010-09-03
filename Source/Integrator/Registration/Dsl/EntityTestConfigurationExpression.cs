using System.Collections.Generic;
using Commander;
using Integrator.Commands;

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
                                                                          .VerificationCommandType = _testConfiguration.VerificationCommandType;
                                                                  }

                                                                  if (_testConfiguration.InsertCommandType != null)
                                                                  {
                                                                      map
                                                                          .TestConfiguration
                                                                          .InsertCommandType = _testConfiguration.InsertCommandType;
                                                                  }

                                                                  if (_testConfiguration.DeleteCommandType != null)
                                                                  {
                                                                      map
                                                                          .TestConfiguration
                                                                          .DeleteCommandType = _testConfiguration.DeleteCommandType;
                                                                  }

                                                                  if (_testConfiguration.UpdateCommandType != null)
                                                                  {
                                                                      map
                                                                          .TestConfiguration
                                                                          .UpdateCommandType = _testConfiguration.UpdateCommandType;
                                                                  }
                                                              }));
        }

        public EntityTestConfigurationExpression<TEntity> AutoDelete()
        {
            _testConfiguration.DeleteCommandType = typeof (DefaultDeleteEntityCommand<TEntity>);
            return this;
        }

        public EntityTestConfigurationExpression<TEntity> InsertWith<TCommand>()
            where TCommand : IDomainCommand<TEntity>
        {
            _testConfiguration.InsertCommandType = typeof(TCommand);
            return this;
        }

        public EntityTestConfigurationExpression<TEntity> VerifyWith<TCommand>()
            where TCommand : IVerificationCommand<TEntity>
        {
            _testConfiguration.VerificationCommandType =  typeof(TCommand);
            return this;
        }

        public EntityTestConfigurationExpression<TEntity> DeleteWith<TCommand>()
            where TCommand : IDomainCommand<TEntity>
        {
            _testConfiguration.DeleteCommandType = typeof (TCommand);
            return this;
        }

        public EntityTestConfigurationExpression<TEntity> UpdateWith<TCommand>()
            where TCommand : IDomainCommand<TEntity>
        {
            _testConfiguration.UpdateCommandType = typeof(TCommand);
            return this;
        }
    }
}