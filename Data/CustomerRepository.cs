using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class CustomerRepository : Database
{
    private const string GetSql = "SELECT * FROM customer WHERE customerId = ?";
    private const string InsertSql = @"INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?, ?, ?)";
    private const string UpdateSql = @"UPDATE customer SET customerName = ?, addressId = ?, active = ?, lastUpdate = ?, lastUpdateBy = ? WHERE customerId = ?";
    private const string DeleteSql = "DELETE FROM customer WHERE customerId = ?";

    public Customer? GetCustomer(string customerId)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetSql, conn);
        cmd.Parameters.AddWithValue("?", customerId);

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

    public void AddCustomer(Customer customer)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(InsertSql, conn);
        cmd.Parameters.AddWithValue("?", customer.CustomerName);
        cmd.Parameters.AddWithValue("?", customer.AddressId);
        cmd.Parameters.AddWithValue("?", customer.Active ? 1 : 0);
        cmd.Parameters.AddWithValue("?", customer.CreateDate);
        cmd.Parameters.AddWithValue("?", customer.CreatedBy);
        cmd.Parameters.AddWithValue("?", customer.LastUpdate);
        cmd.Parameters.AddWithValue("?", customer.LastUpdateBy);

        cmd.ExecuteNonQuery();
    }

    public void UpdateCustomer(Customer customer)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(UpdateSql, conn);
        cmd.Parameters.AddWithValue("?", customer.CustomerName);
        cmd.Parameters.AddWithValue("?", customer.AddressId);
        cmd.Parameters.AddWithValue("?", customer.Active ? 1 : 0);
        cmd.Parameters.AddWithValue("?", customer.LastUpdate);
        cmd.Parameters.AddWithValue("?", customer.LastUpdateBy);
        cmd.Parameters.AddWithValue("?", customer.CustomerId);

        cmd.ExecuteNonQuery();
    }

    public void DeleteCustomer(Customer customer)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(DeleteSql, conn);
        cmd.Parameters.AddWithValue("?", customer.CustomerId);

        cmd.ExecuteNonQuery();
    }
}
