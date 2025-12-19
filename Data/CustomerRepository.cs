

namespace AppointmentScheduler.Data
{
    public class CustomerRepository
    {
        private readonly string connectionString =
            "Driver={MySQL ODBC 8.0 Driver};Server=127.0.0.1;Port=3306;Database=client_schedule;User=sqlUser;Password=Passw0rd!;";
        
        public Customer? GetCustomer(string customerName)
        {
            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM customer WHERE customerName = ?";

                    using (var cmd = new OdbcCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", customerName); // Replaces the ? placeholders above. Prevents SQL injection.
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Customer
                                {
                                    CustomerId = reader.GetInt32(0),
                                    CustomerName = reader.GetString(1),
                                    AddressId = reader.GetInt32(2),
                                    Active = reader.GetByte(3) == 1, // Gets the 0 or 1 and compares it to 1. If true it evaluates to the boolean True, if not, it becomes False.
                                    CreateDate = reader.GetDateTime(4),
                                    CreatedBy = reader.GetString(5),
                                    LastUpdate = reader.GetDateTime(6),
                                    LastUpdateBy = reader.GetString(7)
                                };
                            }
                        }
                    }
                }
                return null; // Failed to locate Customer.
            }
            catch (OdbcException ex)
            {
                throw new ApplicationException("Database error while retrieving customer.", ex);
            }
        }
    }
}
