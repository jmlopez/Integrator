using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Commander;
using Commander.StructureMap;
using Integrator.Generators;
using Integrator.Registration;
using StructureMap;

namespace Integrator
{
    public static class IntegrationFactory
    {
        private static DomainGraph _graph;
        private static IntegrationRunner _runner;
        
        /// <summary>
        /// Gets the currently configured domain graph.
        /// </summary>
        public static DomainGraph Graph { get { return _graph; } }
        
        /// <summary>
        /// Gets the currently configured integration runner.
        /// </summary>
        public static IIntegrationRunner Runner { get { return _runner; } }

        #region Initialization
        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <typeparam name="TRegistry"></typeparam>
        /// <param name="configure"></param>
        public static void Initialize<TRegistry>(Action<ConfigurationExpression> configure)
            where TRegistry : IntegratorRegistry, new()
        {
            Initialize(configure, new TRegistry());
        }

        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="registry"></param>
        public static void Initialize(Action<ConfigurationExpression> configure, IntegratorRegistry registry)
        {
            Initialize(configure, registry, null);
        }

        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="registry"></param>
        public static void Initialize(Action<ConfigurationExpression> configure, IntegratorRegistry registry, Action<DatabaseExpression> configureDb)
        {
            lock (typeof(IntegrationFactory))
            {
                if (configureDb != null)
                {
                    var dbManager = new DatabaseManager();
                    var dbExpression = new DatabaseExpression(dbManager);
                    configureDb(dbExpression);

                    dbManager
                        .EnsureDatabaseExists();
                }

                var container = new Container(configure);
                container.Configure(x => x.IncludeRegistry<IntegratorStructureMapRegistry>());

                container
                    .GetAllInstances<IIntegratorRegistryExtension>()
                    .Each(ext => ext.Configure(registry));

                _graph = registry.BuildGraph();

                var facility = new StructureMapContainerFacility(container);
                CommanderFactory.Initialize(facility, registry.CommandRegistry);

                _runner = new IntegrationRunner(_graph, container, CommanderFactory.Invoker);
            }
        }
        /// <summary>
        /// Initializes the integration framework.
        /// </summary>
        /// <param name="smConfigure"></param>
        /// <param name="configure"></param>
        public static void Initialize(Action<ConfigurationExpression> smConfigure, Action<IntegratorRegistry> configure)
        {
            Initialize(smConfigure, new IntegratorRegistry(configure), null);
        }
        #endregion

        #region Generation and Persistence
        public static IEntityGenerator GeneratorFor<TEntity>()
            where TEntity : class
        {
            return _runner.GeneratorFor<TEntity>();
        }

        public static TEntity Generate<TEntity>()
            where TEntity : class
        {
            return _runner.Generate<TEntity>();
        }

        /// <summary>
        /// Fills the specified instance with random values.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void Fill<TEntity>(TEntity entity)
            where TEntity : class
        {
            _runner.Fill(entity);
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>()
            where TEntity : class
        {
            return _runner.GenerateAndPersist<TEntity>();
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>(bool autoDelete)
            where TEntity : class
        {
            return _runner.GenerateAndPersist<TEntity>(autoDelete);
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command)
            where TEntity : class
        {
            return _runner.GenerateAndPersist(command);
        }

        public static InvocationResult<TEntity> GenerateAndPersist<TEntity>(IDomainCommand<TEntity> command, bool autoDelete)
            where TEntity : class
        {
            return _runner.GenerateAndPersist(command, autoDelete);
        }

        public static InvocationResult<TEntity> Persist<TEntity>(TEntity entity)
            where TEntity : class
        {
            return _runner.Persist(entity);
        }

        public static InvocationResult<TEntity> Persist<TEntity>(TEntity entity, IDomainCommand<TEntity> command)
            where TEntity : class
        {
            return _runner.Persist(entity, command);
        }

        public static TEntity Retrieve<TEntity>(object id)
            where TEntity : class
        {
            return _runner.Retrieve<TEntity>(id);
        }

        public static void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            _runner.Delete(entity);
        }

        public static void Delete<TEntity>(IDomainCommand<TEntity> command, TEntity entity)
            where TEntity : class
        {
            _runner.Delete(command, entity);
        }

        #endregion

        #region Testing
        /// <summary>
        /// Tests the entity by leveraging the nhibernate mappings exposed to structuremap.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static void Test<TEntity>()
            where TEntity : class
        {
            _runner.Test<TEntity>();
        }

        public static void Test<TEntity>(Expression<Func<TEntity, object>> identifier)
            where TEntity : class
        {
            _runner.Test(identifier);
        }
        #endregion
    }
}