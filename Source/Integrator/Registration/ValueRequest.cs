using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore.Reflection;

namespace Integrator.Registration
{
    public class ValueRequest
    {
        private readonly Type _type;
        private readonly Accessor _accessor;

        private ValueRequest(Type type, Accessor accessor)
        {
            _type = type;
            _accessor = accessor;
        }

        public Accessor Accessor()
        {
            return _accessor;
        }

        public Type Type
        {
            get { return _type; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ValueRequest)) return false;
            return Equals((ValueRequest)obj);
        }

        public bool Equals(ValueRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._type, _type) && Equals(other._accessor, _accessor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_type != null ? _type.GetHashCode() : 0) * 397) ^ (_accessor != null ? _accessor.GetHashCode() : 0);
            }
        }

        public static ValueRequest For<T>()
        {
            return For(typeof (T));
        }

        public static ValueRequest For(Type type)
        {
            return new ValueRequest(type, null);
        }

        public static ValueRequest For<T>(Expression<Func<T, object>> expression)
        {
            return new ValueRequest(typeof(T), expression.ToAccessor());
        }

        public static ValueRequest For(Type type, PropertyInfo property)
        {
            return new ValueRequest(type, new SingleProperty(property));
        }
    }
}