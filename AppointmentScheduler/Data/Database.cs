using MySql.Data.MySqlClient;

namespace AppointmentScheduler.Data
{
    public abstract class Database
    {
        private readonly string _connectionString =
            "Server=127.0.0.1;" +
            "Port=3306;" +
            "Database=client_schedule;" +
            "Uid=sqlUser;" +
            "Pwd=Passw0rd!;";

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        protected static void ExecuteNonQuery(string sql, MySqlConnection conn, params object?[] parameters)
        {
            using var cmd = new MySqlCommand(sql, conn);

            foreach (var p in parameters)
                cmd.Parameters.AddWithValue("", p);

            cmd.ExecuteNonQuery();
        }

        protected static int GetCreatedId(MySqlConnection conn)
        {
            using var cmd = new MySqlCommand("SELECT LAST_INSERT_ID();", conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
