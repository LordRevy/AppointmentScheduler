using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

    public class CustomerRepository : Database
    {
    /// <summary>
    /// Creates an Appointment object from a single data reader row.
    /// </summary>
    private static Customer MapCustomer(OdbcDataReader r)
    {
        return new Customer
        {
            CustomerId = Convert.ToInt32(r["customerId"]),
            Name = Convert.ToString(r["customerName"]),
            Address = Convert.ToString(r["address"]),
            Phone = Convert.ToString(r["phone"])
        };
    }
        
    /// <summary>
    /// Takes a Customer's ID and returns a Customer object if found in the Database, null otherwise.
    /// </summary>
    public Customer? GetById(int customerId)
    {
        using var conn = GetConnection();
        conn.Open();
        const string getByIdSql = @"
            SELECT c.customerId,
                   c.customerName,
                   a.address,
                   a.phone
            FROM customer c
            JOIN address a ON a.addressId = c.addressId
            WHERE c.customerId = ?;";

        using var cmd = new OdbcCommand(getByIdSql, conn);
        cmd.Parameters.AddWithValue("", customerId);
        
        using var r = cmd.ExecuteReader();
        return r.Read() ? MapCustomer(r) : null;
    }

    /// <summary>
    /// Returns a list of all Customers within the Database.
    /// </summary>
    public List<Customer> GetAll()
    {
        var customerList = new List<Customer>();
        using var conn = GetConnection();
        conn.Open();
        const string getAllSql = @"
            SELECT c.customerId,
                   c.customerName,
                   a.address,
                   a.phone
            FROM customer c
            JOIN address a ON a.addressId = c.addressId
            ORDER BY c.customerName;";

        using var cmd = new OdbcCommand(getAllSql, conn);
        using var r = cmd.ExecuteReader();
        
        while (r.Read())
        {
            customerList.Add(MapCustomer(r);
        }
        return customerList;
    }

    /// <summary>
    /// Adding a new Customer to the Database and attaching an address to it. Creates a new Address if it does not currently exist.
    /// </summary>
    public int Add(string name, string phone, string address)
    {
        using var conn = GetConnection();
        conn.Open();

        const string checkIfAddressExistsSql = "SELECT addressId FROM address WHERE address = ? AND phone = ?;";
        using var cmd = new OdbcCommand(checkIfAddressExistsSql, conn);
        cmd.Parameters.AddWithValue("", address);
        cmd.Parameters.AddWithValue("", phone);
    
        int addressId;
        using (var r = cmd.ExecuteReader())
        {
            if (r.Read())
                addressId = r.GetInt32(0);
            else
            {
                const string insertIntoAddressSql = @"
                    INSERT INTO address (address, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (?, ?, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');";
        
                ExecuteNonQuery(insertIntoAddressSql, conn, address, phone);
                addressId = GetCreatedId();
            }
        }
        
        const string insertIntoCustomerSql = @"
            INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
            VALUES (?, ?, 1, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');";
        
        ExecuteNonQuery(insertIntoCustomerSql, conn, name, addressId);
        return GetCreatedId();
    }

    /// <summary>
    /// Updates a Customer in the Database using the updated Customer object as a parameter.
    /// </summary>
    public void Update(Customer c)
    {
        using var conn = GetConnection();
        conn.Open();

        const string getAddressIdSql = "SELECT addressId FROM customer WHERE customerId = ?;";
        
        const string updateAddressSql = @"
            UPDATE address 
            SET address = ?,
                phone = ?,
                lastUpdate = UTC_TIMESTAMP(),
                lastUpdateBy = 'app'
            WHERE addressId = ?;"

        const string updateCustomerSql = @"
            UPDATE customer
            SET customerName = ?,
                lastUpdate = UTC_TIMESTAMP(),
                lastUpdateBy = 'app'
            WHERE customerId = ?;"

        // Get Address ID and Update Address Table
        using var cmd = new OdbcCommand(getAddressIdIdSql, conn);
        cmd.Parameters.AddWithValue("", customerId);
        using var addressId = cmd.ExecuteReader();
        ExecuteNonQuery(updateAddressSql, conn, c.Address, c.Phone, addressId);

        // Update Customer Table
        ExecuteNonQuery(updateCustomerSql, conn, c.Name);
    }

    /// <summary>
    /// Takes Customer object and deletes associated Database row.
    /// </summary>
    public void Delete(Customer c)
    {
        using var conn = GetConnection();
        conn.Open();

        const string deleteCustomer = "DELETE FROM customer WHERE customerId = ?;"
        ExecuteNonQuery(deleteCustomerSql, conn, c.Id);    
    }
}
