using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class AddressRepository : Database
{
    private string GetAddress = "SELECT * FROM address WHERE addressId = ?";
    private string AddAddress = @"INSERT INTO address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (?, ?, ?, ?, ?, ?, ?)";
    private string GetAddressId = "SELECT * FROM address WHERE address = ? AND phone = ?";

    public Address? GetAddress(int addressId)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetAddress, conn);
        cmd.Parameters.AddWithValue("?", addressId);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Address
        {
            AddressId = reader.GetInt32(0), // Replace indexes with GetOrdinal
            Address = reader.GetString(1),
            Address2 = reader.IsDBNull(2) ? null : reader.GetString(2),
            CityId = reader.GetInt32(3),
            PostalCode = reader.GetString(4),
            Phone = reader.GetString(5),
            CreateDate = reader.GetDateTime(6),
            CreatedBy = reader.GetString(7),
            LastUpdate = reader.GetDateTime(8),
            LastUpdateBy = reader.GetString(9)
        };
    }

    public bool AddAddress(Address address)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(AddAddress, conn);
        cmd.Parameters.AddWithValue("?", address.address);
        cmd.Parameters.AddWithValue("?", address.address2);
        cmd.Parameters.AddWithValue("?", address.cityId);
        cmd.Parameters.AddWithValue("?", address.postalCode);
        cmd.Parameters.AddWithValue("?", address.phone);
        cmd.Parameters.AddWithValue("?", address.createDate);
        cmd.Parameters.AddWithValue("?", address.createdBy);
        cmd.Parameters.AddWithValue("?", address.lastUpdate);
        cmd.Parameters.AddWithValue("?", address.lastUpdateBy);

        cmd.ExecuteNonQuery();
    }

    public int GetAddressId(string address, string phone)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetAddressId, conn);
        cmd.Parameters.AddWithValue("?", address);
        cmd.Parameters.AddWithValue("?", phone);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return 0; // 0 means AddressId not found.

        return reader.GetInt32(0);
    }
}
