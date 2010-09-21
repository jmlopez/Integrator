using System;
using System.Linq.Expressions;
using Commander;
using StructureMap;

namespace Integrator
{
    public interface IIntegrationRunner
    {
        IContainer Container { get; }

        #region Generation and Persistence
        TEntity Generate<TEntity>()
            where TEntity : class;

        void Fill<TEntity>(TEntity entity)
            where TEntity : class;

        TEntity AutoFill<TEntity>()
            where TEntity : class, new();
        
        void AutoFill<TEntity>(TEntity entity)
            where TEntity : class;

        TEntity AutoFill<TEntity, TCommand>()
            where TEntity : class, new()
            where TCommand : IDomainCommand<TEntity>;
        
        void AutoFill<TEntity, TCommand>(TEntity entity)
            where TEntity : class
            where TCommand : IDomainCommand<TEntity>;

        InvocationResult<TEntity> GenerateAndPersist<TEntity>()
            where TEntity : class;

        InvocationResult<TEntity> GenerateAndPersist<TEntity>(bool autoDelete)
            where TEntity : class;

        InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command)
            where TEntity : class;

        InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command, bool autoDelete)
            where TEntity : class;

        InvocationResult<TEntity> Persist<TEntity>(TEntity entity)
            where TEntity : class;

        InvocationResult<TEntity> Persist<TEntity>(TEntity entity, IDomainCommand<TEntity> command)
            where TEntity : class;

        TEntity Retrieve<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void Delete<TEntity>(IDomainCommand<TEntity> command, TEntity entity)
            where TEntity : class;
        #endregion

        #region Testing

        void Test<TEntity>() where TEntity : class;
        void Test<TEntity>(Expression<Func<TEntity, object>> identifier) where TEntity : class;

        #endregion
    }
}