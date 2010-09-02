using System;
using Commander;
using Commander.StructureMap;
using FubuCore;
using Integrator.Generators;
using Integrator.Infrastructure;
using Integrator.Registration;
using StructureMap;

namespace Integrator
{
    public static class Integrator
    {
        private static DomainGraph _graph;

        public static DomainGraph Graph { get { return _graph; } }

        public static void Initialize<TRegistry>(Action<IInitializationExpression> configure)
            where TRegistry : IntegratorRegistry, new()
        {
            Initialize(configure, new TRegistry());
        }


        public static void Initialize(Action<IInitializationExpression> configure, IntegratorRegistry registry)
        {
            lock (typeof(Integrator))
            {
                ObjectFactory.Initialize(configure);
                ObjectFactory.Configure(x => x.For<IRepository>().Use<Repository>());
                _graph = registry.BuildGraph();

                var facility = new StructureMapContainerFacility(ObjectFactory.Container);
                CommanderFactory.Initialize(facility, registry.CommandRegistry);
            }
        }

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

        public static void Fill<TEntity>(TEntity entity)
            where TEntity : class
        {
            GeneratorFor<TEntity>()
                .Fill(entity, Graph, Graph.MapFor<TEntity>());
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>()
            where TEntity : class
        {
            return GenerateAndPersist<TEntity>(new PersistEntityCommand<TEntity>());
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
            return Persist(entity, new PersistEntityCommand<TEntity>());
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
    }
}