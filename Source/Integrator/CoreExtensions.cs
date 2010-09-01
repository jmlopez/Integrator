using System;
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
    }
}