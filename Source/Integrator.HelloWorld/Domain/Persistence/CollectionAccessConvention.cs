using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Integrator.HelloWorld.Domain.Persistence
{
    public class CollectionAccessConvention : ICollectionConvention
    {
        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(ICollectionInstance instance)
        {
            instance.Access.CamelCaseField(CamelCasePrefix.Underscore);
        }
    }
}