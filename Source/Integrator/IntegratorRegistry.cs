using System;
using System.Collections.Generic;
using Commander.Registration.Dsl;
using Integrator.Bootstrapping;
using Integrator.Commands.Conventions;
using Integrator.Registration;
using Integrator.Registration.Conventions;
using Integrator.Registration.Dsl;
using ProAceFx.Commander.Conventions;
using AppliesToExpression = Integrator.Registration.Dsl.AppliesToExpression;
using EntityMatcher = Integrator.Registration.Dsl.EntityMatcher;
using PoliciesExpression = Integrator.Registration.Dsl.PoliciesExpression;
using TypeCandidateExpression = Integrator.Registration.Dsl.TypeCandidateExpression;

namespace Integrator
{
    public class IntegratorRegistry
    {
        private readonly CommandRegistry _commandRegistry = new CommandRegistry();
        private readonly TypePool _types = new TypePool();
        private readonly TypePool _generatorTypes = new TypePool();
        private readonly EntityMatcher _entityMatcher;
        private readonly DefaultGeneratorRegistry _defaultGeneratorRegistry = new DefaultGeneratorRegistry();
        private readonly GeneratorResolver _generatorResolver = new GeneratorResolver();
        private readonly List<IConfigurationAction> _conventions = new List<IConfigurationAction>();
        private readonly IList<IConfigurationAction> _policies = new List<IConfigurationAction>();
        private readonly List<Action<DomainGraph>> _explicits = new List<Action<DomainGraph>>();
        private readonly IList<IGeneratorRegistryModification> _generatorModifications = new List<IGeneratorRegistryModification>();
        
        public IntegratorRegistry()
        {
            _entityMatcher = new EntityMatcher(_types);

            _generatorTypes.AddAssembly(typeof(IntegratorRegistry).Assembly);
            _generatorModifications.Add(new RegisterDefaultEntityGeneratorConvention());
            _generatorModifications.Add(new RegisterDefaultGenerators(_generatorTypes));


            addConvention(graph => _entityMatcher.BuildMaps(graph));
            addConvention(graph => _generatorResolver.RegisterGeneratorPolicy(_defaultGeneratorRegistry));
            _explicits.Add(graph => _generatorTypes.ImportAssemblies(_types));
            addConvention(graph => _generatorResolver.ApplyToAll(graph));

            Commands
                .ApplyConvention<InitializeUnitOfWorkConvention>()
                .ApplyConvention<CommitUnitOfWorkConvention>()
                .ApplyConvention<InsertNewEntitiesConvention>()
                .ApplyConvention<UpdateExistingEntitiesConvention>()
                .ApplyConvention<FindEntitiesFromRepositoryConvention>();
        }

        public IntegratorRegistry(Action<IntegratorRegistry> configure)
            : this()
        {
            configure(this);
        }

        public AppliesToExpression Applies { get { return new AppliesToExpression(_types); } }
        public TypeCandidateExpression Entities { get { return new TypeCandidateExpression(_entityMatcher, _types, new TypeCandidateExpressionAdapter(_commandRegistry.Entities)); } }
        public PoliciesExpression Policies { get { return new PoliciesExpression(_policies); } }
        public GeneratorExpression Generators { get { return new GeneratorExpression(_generatorResolver); } }
        public MapAlterationExpression Maps { get { return new MapAlterationExpression(_policies); } }
        public GeneratorRegistryExpression GeneratorRegistry { get { return new GeneratorRegistryExpression(_generatorModifications);}}
        public CommandRegistry CommandRegistry { get { return _commandRegistry; } }
        public CommandsExpression Commands { get { return new CommandsExpression(_commandRegistry); } }


        private void addConvention(Action<DomainGraph> convention)
        {
            _conventions.Add(new LambdaConfigurationAction(convention));
        }

        public void ApplyConvention<TConvention>()
           where TConvention : IConfigurationAction, new()
        {
            ApplyConvention(new TConvention());
        }

        public void ApplyConvention<TConvention>(TConvention convention)
            where TConvention : IConfigurationAction
        {
            _conventions.Add(convention);
        }

        public DomainGraph BuildGraph()
        {
            var graph = new DomainGraph();

            _conventions.Configure(graph);
            _policies.Configure(graph);

            _explicits.Each(action => action(graph));

            _generatorModifications.Each(modification => modification.Modify(graph, _defaultGeneratorRegistry));

            _types.EachAssembly(assembly => _commandRegistry.Applies.ToAssembly(assembly));

            return graph;
        }
    }
}