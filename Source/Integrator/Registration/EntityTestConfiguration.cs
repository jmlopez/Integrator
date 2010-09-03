using System;

namespace Integrator.Registration
{
    public class EntityTestConfiguration
    {
        public EntityTestConfiguration(Type entityType)
        {
            EntityType = entityType;
        }

        public Type EntityType { get; private set; }
        public Type InsertCommandType { get; set; }
        public Type UpdateCommandType { get; set; }
        public Type VerificationCommandType { get; set; }
        public Type DeleteCommandType { get; set; }
    }
}