using System;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class AppointmentRepository : Repository
{
    public Appointment? GetAppointment(int ID)
    {
        const string sql = "SELECT * FROM customer WHERE appointmentId = ?";

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new OdbcCommand(sql, conn);
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
}
