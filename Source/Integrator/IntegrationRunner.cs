using System;
using System.Linq;
using System.Linq.Expressions;
using Commander;
using FubuCore;
using FubuCore.Reflection;
using Integrator.Commands;
using Integrator.Generators;
using Integrator.Infrastructure;
using Integrator.Registration;
using NUnit.Framework;
using StructureMap;

namespace Integrator
{
    public class IntegrationRunner : IIntegrationRunner
    {
        private readonly DomainGraph _domainGraph;
        private readonly IContainer _container;
        private readonly ICommandInvoker _invoker;

        public IntegrationRunner(DomainGraph domainGraph, IContainer container, ICommandInvoker invoker)
        {
            _domainGraph = domainGraph;
            _invoker = invoker;
            _container = container;
        }

        public IContainer Container { get { return _container; } }

        #region Generation and Persistence
        public IEntityGenerator GeneratorFor<TEntity>()
            where TEntity : class
        {
            return _domainGraph
                .MapFor<TEntity>()
                .GeneratorPolicy
                .Build(ValueRequest.For<TEntity>())
                .As<IEntityGenerator>();
        }

        public TEntity Generate<TEntity>()
            where TEntity : class
        {
            return GeneratorFor<TEntity>()
                .Generate()
                .As<TEntity>();
        }

        public void Fill<TEntity>(TEntity entity) 
            where TEntity : class
        {
            GeneratorFor<TEntity>()
                .Fill(entity, _domainGraph);
        }

        public TEntity AutoFill<TEntity>()
            where TEntity : class, new()
        {
            var entity = new TEntity();
            AutoFill(entity);

            return entity;
        }

        public void AutoFill<TEntity>(TEntity entity) 
            where TEntity : class
        {
            var config = _domainGraph
                .MapFor<TEntity>()
                .TestConfiguration;

            IDomainCommand<TEntity> command = new DefaultPersistEntityCommand<TEntity>();
            if (config.InsertCommandType != null)
            {
                command = _container
                    .GetInstance(config.InsertCommandType)
                    .As<IDomainCommand<TEntity>>();
            }

            command.Execute(entity);
        }

        public TEntity AutoFill<TEntity, TCommand>() 
            where TEntity : class, new()
            where TCommand : IDomainCommand<TEntity>
        {
            var entity = new TEntity();
            AutoFill<TEntity, TCommand>(entity);
            return entity;
        }

        public void AutoFill<TEntity, TCommand>(TEntity entity) 
            where TEntity : class where TCommand : IDomainCommand<TEntity>
        {
            var command = _container.GetInstance<TCommand>();
            command.Execute(entity);
        }

        public InvocationResult<TEntity> GenerateAndPersist<TEntity>() 
            where TEntity : class
        {
            return GenerateAndPersist<TEntity>(false);
        }

        public InvocationResult<TEntity> GenerateAndPersist<TEntity>(bool autoDelete) 
            where TEntity : class
        {
            var config = _domainGraph
                .MapFor<TEntity>()
                .TestConfiguration;

            IDomainCommand<TEntity> command = new DefaultPersistEntityCommand<TEntity>();
            if(config.InsertCommandType != null)
            {
                command = _container
                    .GetInstance(config.InsertCommandType)
                    .As<IDomainCommand<TEntity>>();
            }

            return GenerateAndPersist(command, autoDelete);
        }

        public InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command) 
            where TEntity : class
        {
            return GenerateAndPersist(command, false);
        }

        public InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command, bool autoDelete) 
            where TEntity : class
        {
            var entity = Generate<TEntity>();
            var result = _invoker
                .ForNew(ctx => ctx.Set(entity), command);

            if (autoDelete)
            {
                Delete(result.Entity);
            }

            return result;
        }

        public InvocationResult<TEntity> Persist<TEntity>(TEntity entity) 
            where TEntity : class
        {
            return Persist(entity, new DefaultPersistEntityCommand<TEntity>());
        }

        public InvocationResult<TEntity> Persist<TEntity>(TEntity entity, IDomainCommand<TEntity> command) 
            where TEntity : class
        {
            return _invoker
                .ForNew(ctx => ctx.Set(entity), command);
        }

        public TEntity Retrieve<TEntity>(object id) 
            where TEntity : class
        {
            return _container
                .GetInstance<IRepository>()
                .Find<TEntity>(id);
        }

        public void Delete<TEntity>(TEntity entity) 
            where TEntity : class
        {
            var config = _domainGraph
                .MapFor<TEntity>()
                .TestConfiguration;

            IDomainCommand<TEntity> command = _container.GetInstance<DefaultDeleteEntityCommand<TEntity>>();
            if (config.DeleteCommandType != null)
            {
                command = _container
                    .GetInstance(config.DeleteCommandType)
                    .As<IDomainCommand<TEntity>>();
            }

            Delete(command, entity);
        }

        public void Delete<TEntity>(IDomainCommand<TEntity> command, TEntity entity) 
            where TEntity : class
        {
            command.Execute(entity);
        }
        #endregion

        #region Testing
        public void Test<TEntity>()
            where TEntity : class
        {
            var mapping = _container
                            .GetInstance<NHibernate.Cfg.Configuration>()
                            .GetClassMapping(typeof(TEntity));

            var prop = typeof(TEntity).GetProperty(mapping.IdentifierProperty.Name);

            Func<TEntity, object> identifier = e => prop.GetValue(e, null);
            Test(identifier);
        }

        public void Test<TEntity>(Expression<Func<TEntity, object>> identifier)
            where TEntity : class
        {
            Func<TEntity, object> func = e => identifier.ToAccessor().GetValue(e);
            Test(func);
        }

        private void Test<TEntity>(Func<TEntity, object> identifer)
            where TEntity : class
        {
            var config = _domainGraph.MapFor<TEntity>().TestConfiguration;
            var insertCommand = _container.GetInstance(config.InsertCommandType).As<IDomainCommand<TEntity>>();
            var verifyCommand = _container.GetInstance(config.VerificationCommandType).As<IVerificationCommand<TEntity>>();

            var result = GenerateAndPersist(insertCommand);
            AssertionException failedAssertion = null;
            try
            {
                if (result.HasProblems)
                {
                    Assert.Fail(result.Problems.First().Exception.Message);
                }

                var afterInsert = Retrieve<TEntity>(identifer(result.Entity));
                verifyCommand.Verify(result.Entity, afterInsert);
            }
            catch (AssertionException exc)
            {
                failedAssertion = exc;
            }

            if (config.DeleteCommandType != null)
            {
                _container
                    .GetInstance(config.DeleteCommandType)
                    .As<IDomainCommand<TEntity>>()
                    .Execute(result.Entity);
            }

            if (failedAssertion != null)
            {
                throw failedAssertion;
            }

        }
        #endregion
    }
}