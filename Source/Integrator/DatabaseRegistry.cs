namespace Integrator
{
    public class DatabaseRegistry
    {
        private readonly DatabaseManager _manager = new DatabaseManager();

        public DatabaseRegistry()
        {
        }

        public DatabaseExpression Database { get { return new DatabaseExpression(_manager); } }

        public DatabaseManager BuildManager()
        {
            return _manager;
        }
    }
}