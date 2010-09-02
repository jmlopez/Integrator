using System.Reflection;

namespace Integrator.Commands
{
    public interface IPropertyDiffStrategy
    {
        bool Matches(PropertyInfo property);
        Diff Diff<T>(PropertyInfo property, T x, T y);
    }
}