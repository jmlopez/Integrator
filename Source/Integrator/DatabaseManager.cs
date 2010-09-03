using System.Configuration;
using System.Data.SqlClient;

namespace Integrator
{
    public class DatabaseManager
    {
        public string DatabaseName { get; set; }
        public string ConnectionStringName { get; set; }

        public void EnsureDatabaseExists()
        {
            // TODO -- clean this up
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if(connStr == null || string.IsNullOrEmpty(DatabaseName))
            {
                throw new IntegratorException(1003, "Database manager is not configured");
            }

            using(var conn = new SqlConnection(connStr.ConnectionString))
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("if db_id('{0}') is null create database {0}", DatabaseName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}