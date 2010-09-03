namespace Integrator
{
    public class DatabaseExpression
    {
        private readonly DatabaseManager _manager;

        public DatabaseExpression(DatabaseManager manager)
        {
            _manager = manager;
        }

        public DatabaseExpression Use(string dbName)
        {
            _manager.DatabaseName = dbName;
            return this;
        }

        public DatabaseExpression ConnectWith(string connectionStringName)
        {
            _manager.ConnectionStringName = connectionStringName;
            return this;
        }
    }
}