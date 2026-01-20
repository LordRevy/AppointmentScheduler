using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data
{
    public class CustomerRepository : Database
    {
    
/// <summary>
/// Takes a Customer's ID and returns a Customer object if found in the Database, null otherwise.
/// </summary>
        public Customer? GetById(int customerId)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                SELECT c.customerId,
                       c.customerName,
                       a.address,
                       a.phone
                FROM customer c
                JOIN address a ON a.addressId = c.addressId
                WHERE c.customerId = ?;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", customerId);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new Customer
            {
                CustomerId = Convert.ToInt32(r["customerId"]),
                Name       = Convert.ToString(r["customerName"]),
                Address    = Convert.ToString(r["address"]),
                Phone      = Convert.ToString(r["phone"])
            };
        }

/// <summary>
/// Returns a list of all Customers within the Database.
/// </summary>
        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                SELECT c.customerId,
                       c.customerName,
                       a.address,
                       a.phone
                FROM customer c
                JOIN address a ON a.addressId = c.addressId
                ORDER BY c.customerName;";

            using var cmd = new OdbcCommand(sql, conn);
            using var r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                list.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(r["customerId"]),
                    Name       = Convert.ToString(r["customerName"]),
                    Address    = Convert.ToString(r["address"]),
                    Phone      = Convert.ToString(r["phone"])
                });
            }
            return list;
        }

/// <summary>
/// Adding a new Customer to the Database and attaching an address to it. Creates a new Address if it does not currently exist.
/// </summary>
        public int Add(string name, string address, string phone)
        {
            using var conn = GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            
            try
            {
                // Create Address
                using (var cmdAddr = new OdbcCommand(
                    "INSERT INTO address (address, phone, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');",
                    conn, tx))
                {
                    cmdAddr.Parameters.AddWithValue("", address.Trim());
                    cmdAddr.Parameters.AddWithValue("", phone.Trim());
                    cmdAddr.ExecuteNonQuery();
                }

                // Get the Address ID
                int addressId;
                using (var cmdGetAddrId = new OdbcCommand("SELECT LAST_INSERT_ID();", conn, tx))
                {
                    addressId = Convert.ToInt32(cmdGetAddrId.ExecuteScalar());
                }

                // Insert into Customer table
                using (var cmdCust = new OdbcCommand(
                    "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, 1, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');",
                    conn, tx))
                {
                    cmdCust.Parameters.AddWithValue("", name.Trim());
                    cmdCust.Parameters.AddWithValue("", addressId);
                    cmdCust.ExecuteNonQuery();
                }

                int customerId;
                using (var cmdGetCustId = new OdbcCommand("SELECT LAST_INSERT_ID();", conn, tx))
                {
                    customerId = Convert.ToInt32(cmdGetCustId.ExecuteScalar());
                }
                tx.Commit();
                return customerId;
            }
            
            catch
            {
                tx.Rollback();
                throw;
            }
        }

/// <summary>
/// Updates a Customer in the Database. ID must match the Customer, the other parameters are what is available to change both the Address and Customer tables.
/// </summary>
        public void Update(int customerId, string name, string address, string phone)
        {
            using var conn = GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            
            try
            {
                // Lookup addressId
                int addressId;
                using (var find = new OdbcCommand("SELECT addressId FROM customer WHERE customerId = ?;", conn, tx))
                {
                    find.Parameters.AddWithValue("", customerId);
                    addressId = Convert.ToInt32(find.ExecuteScalar());
                }

                // Update address
                using (var upAddr = new OdbcCommand(
                    "UPDATE address SET address = ?, phone = ?, lastUpdate = UTC_TIMESTAMP(), lastUpdateBy = 'app' WHERE addressId = ?;", conn, tx))
                {
                    upAddr.Parameters.AddWithValue("", address.Trim());
                    upAddr.Parameters.AddWithValue("", phone.Trim());
                    upAddr.Parameters.AddWithValue("", addressId);
                    upAddr.ExecuteNonQuery();
                }

                // Update customer
                using (var upCust = new OdbcCommand(
                    "UPDATE customer SET customerName = ?, lastUpdate = UTC_TIMESTAMP(), lastUpdateBy = 'app' WHERE customerId = ?;", conn, tx))
                {
                    upCust.Parameters.AddWithValue("", name.Trim());
                    upCust.Parameters.AddWithValue("", customerId);
                    upCust.ExecuteNonQuery();
                }
                tx.Commit();
            }
            
            catch
            {
                tx.Rollback();
                throw;
            }
        }

/// <summary>
/// Deletes a Customer from the Database using the Customer's ID as a parameter.
/// </summary>
        public void Delete(int customerId)
        {
            using var conn = GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            
            try
            {
                using (var delCust = new OdbcCommand("DELETE FROM customer WHERE customerId = ?;", conn, tx))
                {
                    delCust.Parameters.AddWithValue("", customerId);
                    delCust.ExecuteNonQuery();
                }
            }
            
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
