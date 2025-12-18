using AppointmentScheduler.Domain;
using System.Data.Odbc;

namespace AppointmentScheduler.Data
{
    public class UserRepository
    {
        private readonly string connectionString =
            "Driver={MySQL ODBC 8.0 Driver};Server=127.0.0.1;Port=3306;Database=client_schedule;User=sqlUser;Password=Passw0rd!;";
        
        public User? GetUser(string username)
        {
            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM user WHERE userName = ?";
                    
                    using (var cmd = new OdbcCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userName", username); // Replaces the ? placeholders above. Prevents SQL injection.
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserId = reader.GetInt32(0),
                                    UserName = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Active = reader.GetBoolean(3),
                                    CreateDate = reader.GetDateTime(4),
                                    CreatedBy = reader.GetString(5),
                                    LastUpdate = reader.GetDateTime(6),
                                    LastUpdateBy = reader.GetString(7)
                                };
                            }
                        }
                    }
                }
                return null; // login failed.
            }
            catch (OdbcException ex)
            {
                throw new ApplicationException("Database error while retrieving user.", ex);
            }
        }
    }
}
