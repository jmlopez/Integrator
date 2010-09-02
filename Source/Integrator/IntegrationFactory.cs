using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Commander;
using Commander.StructureMap;
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
    public static class IntegrationFactory
    {
        private static DomainGraph _graph;

        /// <summary>
        /// Gets the currently configured domain graph.
        /// </summary>
        public static DomainGraph Graph { get { return _graph; } }

        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <typeparam name="TRegistry"></typeparam>
        /// <param name="configure"></param>
        public static void Initialize<TRegistry>(Action<IInitializationExpression> configure)
            where TRegistry : IntegratorRegistry, new()
        {
            Initialize(configure, new TRegistry());
        }

        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="registry"></param>
        public static void Initialize(Action<IInitializationExpression> configure, IntegratorRegistry registry)
        {
            lock (typeof(IntegrationFactory))
            {
                ObjectFactory.Initialize(configure);
                ObjectFactory.Configure(x => x.IncludeRegistry<IntegratorStructureMapRegistry>());

                ObjectFactory
                    .GetAllInstances<IIntegratorRegistryExtension>()
                    .Each(ext => ext.Configure(registry));

                _graph = registry.BuildGraph();

                var facility = new StructureMapContainerFacility(ObjectFactory.Container);
                CommanderFactory.Initialize(facility, registry.CommandRegistry);
            }
        }
        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <param name="smConfigure"></param>
        /// <param name="configure"></param>
        public static void Initialize(Action<IInitializationExpression> smConfigure, Action<IntegratorRegistry> configure)
        {
            Initialize(smConfigure, new IntegratorRegistry(configure));
        }

        public static IEntityGenerator GeneratorFor<TEntity>()
            where TEntity : class
        {
            return _graph
                    .MapFor<TEntity>()
                    .GeneratorPolicy
                    .Build(ValueRequest.For<TEntity>())
                    .As<IEntityGenerator>();
        }

        public static TEntity Generate<TEntity>()
            where TEntity : class
        {
            return GeneratorFor<TEntity>()
                    .Generate()
                    .As<TEntity>();
        }

        /// <summary>
        /// Fills the specified instance with random values.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void Fill<TEntity>(TEntity entity)
            where TEntity : class
        {
            GeneratorFor<TEntity>()
                .Fill(entity, Graph, Graph.MapFor<TEntity>());
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>()
            where TEntity : class
        {
            return GenerateAndPersist<TEntity>(new DefaultPersistEntityCommand<TEntity>());
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command)
            where TEntity : class
        {
            var entity = Generate<TEntity>();
            return CommanderFactory
                    .Invoker
                    .ForNew(ctx => ctx.Set(entity), command);
        }

        public static InvocationResult<TEntity> Persist<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Persist(entity, new DefaultPersistEntityCommand<TEntity>());
        }

        public static InvocationResult<TEntity> Persist<TEntity>(TEntity entity, IDomainCommand<TEntity> command)
            where TEntity : class
        {
            return CommanderFactory
                    .Invoker
                    .ForNew(ctx => ctx.Set(entity), command);
        }

        public static TEntity Retrieve<TEntity>(object id)
            where TEntity : class
        {
            return ObjectFactory
                    .GetInstance<IRepository>()
                    .Find<TEntity>(id);
        }

        /// <summary>
        /// Tests the entity by leveraging the nhibernate mappings exposed to structuremap.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static void Test<TEntity>()
            where TEntity : class
        {
            var mapping = ObjectFactory
                            .GetInstance<NHibernate.Cfg.Configuration>()
                            .GetClassMapping(typeof(TEntity));

            var prop = typeof(TEntity).GetProperty(mapping.IdentifierProperty.Name);

            Func<TEntity, object> identifier = e => prop.GetValue(e, null);
            Test(identifier);
        }

        public static void Test<TEntity>(Expression<Func<TEntity, object>> identifier)
            where TEntity : class
        {
            Func<TEntity, object> func = e => identifier.ToAccessor().GetValue(e);
            Test(func);
        }

        private static void Test<TEntity>(Func<TEntity, object> identifer)
            where TEntity : class
        {
            var config = Graph.MapFor<TEntity>().TestConfiguration;
            var insertCommand = ObjectFactory.GetInstance(config.CommandType).As<IDomainCommand<TEntity>>();
            var verifyCommand = ObjectFactory.GetInstance(config.VerificationCommandType).As<IVerificationCommand<TEntity>>();

            var result = GenerateAndPersist(insertCommand);
            if (result.HasProblems)
            {
                Assert.Fail(result.Problems.First().Exception.Message);
            }

            var afterInsert = Retrieve<TEntity>(identifer(result.Entity));
            verifyCommand.Verify(result.Entity, afterInsert);
        }
    }
}