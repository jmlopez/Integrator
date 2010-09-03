using System.Collections.Generic;
using System.Reflection;

namespace Integrator.Commands
{
    public class ObjectDiffStrategy : IObjectDiffStrategy
    {
        private readonly IPropertyDiffRegistry _registry;

        public ObjectDiffStrategy(IPropertyDiffRegistry registry)
        {
            _registry = registry;
        }

        public DiffResult Diff<T>(T x, T y)
            where T : class
        {
            var diffs = new List<Diff>();
            typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Each(prop =>
                          {
                              var diff = _registry
                                  .StrategyFor(prop)
                                  .Diff(prop, x, y);
                              if(diff != null)
                              {
                                  diffs.Add(diff);
                              }
                          });

            return new DiffResult(diffs);
        }
    }
}