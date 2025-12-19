public class UserRepository : Repository
{
    public User? GetUser(string username)
    {
        const string sql = "SELECT * FROM user WHERE username = ?";

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(sql, conn);
        cmd.Parameters.AddWithValue("?", username);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User
        {
            UserId = reader.GetInt32(0),
            UserName = reader.GetString(1),
            Password = reader.GetString(2),
            Active = reader.GetByte(3) == 1,
            CreateDate = reader.GetDateTime(4),
            CreatedBy = reader.GetString(5),
            LastUpdate = reader.GetDateTime(6),
            LastUpdateBy = reader.GetString(7)
        };
    }
}
