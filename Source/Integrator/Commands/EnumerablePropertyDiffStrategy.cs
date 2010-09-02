using System.Collections;
using System.Reflection;
using FubuCore;

namespace Integrator.Commands
{
    public class EnumerablePropertyDiffStrategy : IPropertyDiffStrategy
    {
        public bool Matches(PropertyInfo property)
        {
            return property.PropertyType != typeof (string) &&
                   typeof (IEnumerable).IsAssignableFrom(property.PropertyType);
        }

        public Diff Diff<T>(PropertyInfo property, T x, T y)
        {
            var propX = property.GetValue(x, null).As<IEnumerable>();
            var propY = property.GetValue(y, null).As<IEnumerable>();

            if((propX == null && propY != null) || (propX != null && propY == null))
            {
                return new Diff(property, propX, propY);
            }

            var countX = propX.Count();
            var countY = propY.Count();

            if(!countX.Equals(countY))
            {
                return new Diff(property, countX, countY);
            }

            return null;
        }
    }
}