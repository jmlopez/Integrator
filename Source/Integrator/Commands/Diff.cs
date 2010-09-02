using System.Reflection;

namespace Integrator.Commands
{
    public class Diff
    {
        public Diff(PropertyInfo property, object previousValue, object newValue)
        {
            Property = property;
            PreviousValue = previousValue;
            NewValue = newValue;
        }

        public PropertyInfo Property { get; private set; }
        public object PreviousValue { get; private set; }
        public object NewValue { get; private set; }
    }
}