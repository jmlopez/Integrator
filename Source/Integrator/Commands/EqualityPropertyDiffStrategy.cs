using System.Reflection;

namespace Integrator.Commands
{
    public class EqualityPropertyDiffStrategy : IPropertyDiffStrategy
    {
        public bool Matches(PropertyInfo property)
        {
            return true;
        }

        public Diff Diff<T>(PropertyInfo property, T x, T y)
        {
            var propX = property.GetValue(x, null);
            var propY = property.GetValue(y, null);

            if((propX == null && propY != null) || (propX != null && !propX.Equals(propY)))
            {
                return new Diff(property, propX, propY);
            }

            return null;
        }
    }
}