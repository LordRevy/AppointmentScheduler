public abstract class Repository
{
    protected readonly string connectionString =
        "Driver={MySQL ODBC 8.0 Driver};Server=127.0.0.1;Port=3306;Database=client_schedule;User=sqlUser;Password=Passw0rd!;";

    protected T? QuerySingle<T>(string sql, Func<OdbcDataReader, T> map, params object[] parameters)
    {
        using var conn = new OdbcConnection(connectionString);
        conn.Open();

        using var cmd = new OdbcCommand(sql, conn);

        foreach (var p in parameters)
            cmd.Parameters.AddWithValue("?", p);

        using var reader = cmd.ExecuteReader();
        return reader.Read() ? map(reader) : default;
    }
}
