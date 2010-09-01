using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore.Reflection;

namespace Integrator.Registration
{
    public class EntityMap
    {
        private readonly List<PropertyMap> _propertyMaps;
        protected EntityMap(Type entityType)
        {
            EntityType = entityType;
            _propertyMaps = new List<PropertyMap>();
        }

        public Type EntityType { get; private set; }
        public IEnumerable<PropertyMap> PropertyMaps { get { return _propertyMaps; } }
        public IGeneratorPolicy GeneratorPolicy { get; private set; }

        public void GenerateWith(IGeneratorPolicy policy)
        {
            GeneratorPolicy = policy;
        }

        public void AddPropertyMap(PropertyMap propertyMap)
        {
            _propertyMaps.Fill(propertyMap);
        }

        public void IgnoreProperty(PropertyInfo property)
        {
            var propertyMap = MapFor(property);
            if(propertyMap == null)
            {
                return;
            }

            _propertyMaps.Remove(propertyMap);
        }

        public PropertyMap MapFor(PropertyInfo property)
        {
            var request = ValueRequest.For(EntityType, property);
            return _propertyMaps.FirstOrDefault(p => p.Request.Equals(request));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EntityMap)) return false;
            return Equals((EntityMap)obj);
        }

        public bool Equals(EntityMap other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EntityType, EntityType);
        }

        public override int GetHashCode()
        {
            return (EntityType != null ? EntityType.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return string.Format("EntityMap: {0}", EntityType.Name);
        }

        public static EntityMap For(Type entityType)
        {
            return (EntityMap) Activator.CreateInstance(typeof (EntityMap<>).MakeGenericType(entityType));
        }
    }

    public class EntityMap<TEntity> : EntityMap
        where TEntity : class
    {
        public EntityMap()
            : base(typeof(TEntity))
        {
        }

        public PropertyMap MapFor(Expression<Func<TEntity, object>> expression)
        {
            return MapFor(ReflectionHelper.GetProperty(expression));
        }

        public void Ignore(Expression<Func<TEntity, object>> expression)
        {
            IgnoreProperty(ReflectionHelper.GetProperty(expression));
        }
    }
}