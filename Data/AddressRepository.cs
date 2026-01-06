using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class AddressRepository : Database
{
    private string GetSql = "SELECT * FROM address WHERE addressId = ?";

    public Address? GetAddress(int addressId)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetSql, conn);
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
}
