using System;
using System.Collections.Generic;
using System.Linq;

namespace Integrator.Registration
{
    public class DomainGraph
    {
        private readonly List<EntityMap> _entityMaps;

        public DomainGraph()
        {
            _entityMaps = new List<EntityMap>();
        }

        public IEnumerable<EntityMap> EntityMaps { get { return _entityMaps; } }

        public EntityMap MapFor(Type entityType)
        {
            return _entityMaps.FirstOrDefault(m => m.EntityType == entityType);
        }

        public EntityMap<TEntity> MapFor<TEntity>()
            where TEntity : class
        {
            return (EntityMap<TEntity>) MapFor(typeof (TEntity));
        }

        public void AddEntityMap(EntityMap map)
        {
            _entityMaps.Fill(map);
        }
    }
}