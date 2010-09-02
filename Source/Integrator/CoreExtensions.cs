using System;
using System.Collections;
using FubuCore.Reflection;

namespace Integrator
{
    internal static class CoreExtensions
    {
        public static object GetDefaultInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static Type GetGeneratorType(this Type type)
        {
            if(!type.HasAttribute<GeneratorForAttribute>())
            {
                return null;
            }

            return type.GetAttribute<GeneratorForAttribute>().Type;
        }

        public static bool IsCustomRegistryExtension(this Type type)
        {
            return typeof (IIntegratorRegistryExtension).IsAssignableFrom(type)
                   && type.IsClass && !type.IsAbstract;
        }

        public static int Count(this IEnumerable enumerable)
        {
            if(enumerable == null)
            {
                return 0;
            }

            int counter = 0;
            foreach (var value in enumerable)
            {
                ++counter;
            }

            return counter;
        }
    }
}