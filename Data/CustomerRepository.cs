using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class CustomerRepository : Repository
{
    public Customer? GetCustomer(string name)
    {
        const string sql = "SELECT * FROM customer WHERE customerName = ?";

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(sql, conn);
        cmd.Parameters.AddWithValue("?", name);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Customer
        {
            CustomerId = reader.GetInt32(0),
            CustomerName = reader.GetString(1),
            AddressId = reader.GetInt32(2),
            Active = reader.GetByte(3) == 1,
            CreateDate = reader.GetDateTime(4),
            CreatedBy = reader.GetString(5),
            LastUpdate = reader.GetDateTime(6),
            LastUpdateBy = reader.GetString(7)
        };
    }
}
