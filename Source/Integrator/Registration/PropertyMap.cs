using System;
using System.Collections.Generic;
using Integrator.Generators;

namespace Integrator.Registration
{
    public class PropertyMap
    {
        private readonly Func<IGenerator> _generatorResolver;
        public PropertyMap(ValueRequest request, Func<IGenerator> generatorResolver)
        {
            Request = request;
            _generatorResolver = generatorResolver;
        }

        public Type PropertyType()
        {
            if(Request == null)
            {
                return null;
            }

            return Request.Accessor().PropertyType;
        }

        public ValueRequest Request { get; private set; }
        public IGenerator Generator { get { return _generatorResolver(); } }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PropertyMap)) return false;
            return Equals((PropertyMap)obj);
        }

        public bool Equals(PropertyMap other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Request, Request);
        }

        public override int GetHashCode()
        {
            return (Request != null ? Request.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return string.Format("PropertyMap: {0}.{1}", Request.Type.Name, Request.Accessor().PropertyNames.Join("."));
        }
    }
}