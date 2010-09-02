using Integrator.Registration.Dsl;

namespace Integrator
{
    public class RegistryImportExpression
    {
        private readonly ITypeMatcher _matcher;

        public RegistryImportExpression(ITypeMatcher matcher)
        {
            _matcher = matcher;
        }

        public void ImportAll()
        {
            _matcher.TypeFilters.Includes += (type => typeof (IntegratorRegistry).IsAssignableFrom(type));
        }
    }
}