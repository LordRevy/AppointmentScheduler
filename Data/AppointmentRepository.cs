using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class AppointmentRepository : Repository
{
    private const string GetSql = "SELECT * FROM appointment WHERE appointmentId = ?";
    private const string InsertSql = @"INSERT INTO appointment (
        appointmentId, customerId, userId, title, description, location,
        contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy
    )
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    private const string UpdateSql = @"UPDATE appointment SET 
        appointmentId = ?, customerId = ?, userId = ?, title = ?, description = ?, location = ?,
        contact = ?, type = ?, url = ?, start = ?, end = ?, lastUpdate = ?, lastUpdateBy = ?
        WHERE appointmentId = ?";
        
    private const string DeleteSql = "DELETE FROM appointment WHERE appointmentId = ?";

    public Appointment? GetAppointment(int ID)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetSql, conn);
        cmd.Parameters.AddWithValue("?", ID);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Appointment
        {
            AppointmentId = reader.GetInt32(0),
            CustomerId = reader.GetString(1),
            userId = reader.GetInt32(2),
            Title = reader.GetString(3),
            Description = reader.GetString(4),
            Location = reader.GetString(5),
            Contact = reader.GetString(6),
            Type = reader.GetString(7),
            Url = reader.GetString(8),
            Start = reader.GetDateTime(9),
            End = reader.GetDateTime(10),
            CreateDate = reader.GetDateTime(11),
            CreatedBy = reader.GetString(12),
            LastUpdate = reader.GetDateTime(13),
            LastUpdateBy = reader.GetString(14)
        };
    }

        public void AddAppointment(Appointment appointment)
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

    public void UpdateAppointment(Appointment appointment)
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

    public void DeleteAppointment(Appointment appointment)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(DeleteSql, conn);
        cmd.Parameters.AddWithValue("?", customer.CustomerId);

        cmd.ExecuteNonQuery();
    }
}
}
