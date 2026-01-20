
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data
{
    public class CustomerRepository : Repository
    {
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
            cmd.Parameters.AddWithValue(string.Empty, customerId);
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

        public int Add(string name, string address, string phone)
        {
            using var conn = GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            
            try
            {
                // 1) Insert into address
                int addressId;
                using (var cmdAddr = new OdbcCommand(
                    "INSERT INTO address (address, phone, createDate, createdBy, lastUpdate, lastUpdatedBy) VALUES (?, ?, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');",
                    conn, tx))
                {
                    cmdAddr.Parameters.AddWithValue(string.Empty, address.Trim());
                    cmdAddr.Parameters.AddWithValue(string.Empty, phone.Trim());
                    cmdAddr.ExecuteNonQuery();
                }

                // Get LAST_INSERT_ID() for addressId
                using (var cmdGetAddrId = new OdbcCommand("SELECT LAST_INSERT_ID();", conn, tx))
                {
                    addressId = Convert.ToInt32(cmdGetAddrId.ExecuteScalar());
                }

                // 2) Insert into customer
                using (var cmdCust = new OdbcCommand(
                    "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdatedBy) VALUES (?, ?, 1, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');",
                    conn, tx))
                {
                    cmdCust.Parameters.AddWithValue(string.Empty, name.Trim());
                    cmdCust.Parameters.AddWithValue(string.Empty, addressId);
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
                    find.Parameters.AddWithValue('id', customerId);
                    addressId = Convert.ToInt32(find.ExecuteScalar());
                }

                // Update address
                using (var upAddr = new OdbcCommand(
                    "UPDATE address SET address = ?, phone = ?, lastUpdate = UTC_TIMESTAMP(), lastUpdatedBy = 'app' WHERE addressId = ?;", conn, tx))
                {
                    upAddr.Parameters.AddWithValue('address', address.Trim());
                    upAddr.Parameters.AddWithValue('phone', phone.Trim());
                    upAddr.Parameters.AddWithValue('id', addressId);
                    upAddr.ExecuteNonQuery();
                }

                // Update customer
                using (var upCust = new OdbcCommand(
                    "UPDATE customer SET customerName = ?, lastUpdate = UTC_TIMESTAMP(), lastUpdatedBy = 'app' WHERE customerId = ?;", conn, tx))
                {
                    upCust.Parameters.AddWithValue('name', name.Trim());
                    upCust.Parameters.AddWithValue('id', customerId);
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
                    delCust.Parameters.AddWithValue('id', customerId);
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
