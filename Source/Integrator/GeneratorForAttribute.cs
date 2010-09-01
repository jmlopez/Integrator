using System;

namespace Integrator
{
    public class GeneratorForAttribute : Attribute
    {
        private readonly Type _type;

        public GeneratorForAttribute(Type type)
        {
            _type = type;
        }

        public Type Type
        {
            get { return _type; }
        }
    }
}