using AppointmentScheduler.Domain;
using System;
using System.Data.Odbc;

namespace AppointmentScheduler.Logic
{
    public class AuthService
    {
        private readonly string connectionString =
            "Driver={MySQL ODBC 8.0 Driver};Server=127.0.0.1;Port=3306;Database=client_schedule;User=sqlUser;Password=Passw0rd!;";
        
        public User? ValidateCredentials(string username, string password)
        {
            using (var conn = new OdbcConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT userId, userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy " +
                    "FROM user WHERE userName = ? AND password = ? AND active = 1";
                
                using (var cmd = new OdbcCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", username); // Replaces the ? placeholders above.
                    cmd.Parameters.AddWithValue("@password", password); // Prevents SQL injection.
                    
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
    }
}
