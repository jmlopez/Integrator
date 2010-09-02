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
        public Type CommandType { get; private set; }
        public Type VerificationCommandType { get; private set; }

        public void InsertWith(Type commandType)
        {
            CommandType = commandType;
        }

        public void VerifyWith(Type commandType)
        {
            VerificationCommandType = commandType;
        }
    }
}