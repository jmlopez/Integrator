using System;
using Integrator.Registration;

namespace Integrator
{
    public static class Integrator
    {
        private static DomainGraph _graph;

        public static DomainGraph Graph { get { return _graph; } }

        public static void Initialize<TRegistry>()
            where TRegistry : IntegratorRegistry, new()
        {
            Initialize(new TRegistry());
        }


        public static void Initialize(IntegratorRegistry registry)
        {
            lock (typeof(Integrator))
            {
                _graph = registry.BuildGraph();
            }
        }

        public static void Initialize(Action<IntegratorRegistry> configure)
        {
            Initialize(new IntegratorRegistry(configure));
        }
    }
}