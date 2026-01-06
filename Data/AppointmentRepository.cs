using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class AppointmentRepository : Database
{
    private const string GetSql = "SELECT * FROM appointment WHERE appointmentId = ?";
    private const string InsertSql = @"INSERT INTO appointment (
        appointmentId, customerId, userId, title, description, location,
        contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy
    )
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    private const string UpdateSql = @"UPDATE appointment SET 
        customerId = ?, userId = ?, title = ?, description = ?, location = ?,
        contact = ?, type = ?, url = ?, start = ?, end = ?, lastUpdate = ?, lastUpdateBy = ?
    WHERE appointmentId = ?";
        
    private const string DeleteSql = "DELETE FROM appointment WHERE appointmentId = ?";

    public Appointment? GetAppointment(int appointmentId)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(GetSql, conn);
        cmd.Parameters.AddWithValue("?", appointmentId);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Appointment
        {
            AppointmentId = reader.GetInt32(0),
            CustomerId = reader.GetInt32(1),
            UserId = reader.GetInt32(2),
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
        cmd.Parameters.AddWithValue("?", appointment.AppointmentId);
        cmd.Parameters.AddWithValue("?", appointment.CustomerId);
        cmd.Parameters.AddWithValue("?", appointment.UserId);
        cmd.Parameters.AddWithValue("?", appointment.Title);
        cmd.Parameters.AddWithValue("?", appointment.Description);
        cmd.Parameters.AddWithValue("?", appointment.Location);
        cmd.Parameters.AddWithValue("?", appointment.Contact);
        cmd.Parameters.AddWithValue("?", appointment.Type);
        cmd.Parameters.AddWithValue("?", appointment.Url);
        cmd.Parameters.AddWithValue("?", appointment.Start);
        cmd.Parameters.AddWithValue("?", appointment.End);
        cmd.Parameters.AddWithValue("?", appointment.CreateDate);
        cmd.Parameters.AddWithValue("?", appointment.CreatedBy);
        cmd.Parameters.AddWithValue("?", appointment.LastUpdate);
        cmd.Parameters.AddWithValue("?", appointment.LastUpdateBy);

        cmd.ExecuteNonQuery();
    }

    public void UpdateAppointment(Appointment appointment)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(UpdateSql, conn);
        cmd.Parameters.AddWithValue("?", appointment.CustomerId);
        cmd.Parameters.AddWithValue("?", appointment.UserId);
        cmd.Parameters.AddWithValue("?", appointment.Title);
        cmd.Parameters.AddWithValue("?", appointment.Description);
        cmd.Parameters.AddWithValue("?", appointment.Location);
        cmd.Parameters.AddWithValue("?", appointment.Contact);
        cmd.Parameters.AddWithValue("?", appointment.Type);
        cmd.Parameters.AddWithValue("?", appointment.Url);
        cmd.Parameters.AddWithValue("?", appointment.Start);
        cmd.Parameters.AddWithValue("?", appointment.End);
        cmd.Parameters.AddWithValue("?", appointment.LastUpdate);
        cmd.Parameters.AddWithValue("?", appointment.LastUpdateBy);
        cmd.Parameters.AddWithValue("?", appointment.AppointmentId);
        
        cmd.ExecuteNonQuery();
    }

    public void DeleteAppointment(Appointment appointment)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(DeleteSql, conn);
        cmd.Parameters.AddWithValue("?", appointment.AppointmentId);

        cmd.ExecuteNonQuery();
    }
}
}
