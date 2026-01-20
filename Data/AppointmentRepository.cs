
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data
{
    public class AppointmentRepository : Repository
    {
/// <summary>
/// Returns an appointment by ID, or null if not found.
/// </summary>
        public Appointment? GetById(int appointmentId)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE appointmentId = ?;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", appointmentId);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new Appointment
            {
                AppointmentId = Convert.ToInt32(r["appointmentId"]),
                CustomerId    = Convert.ToInt32(r["customerId"]),
                UserId        = Convert.ToInt32(r["userId"]),
                Title         = r["title"] is DBNull ? null : Convert.ToString(r["title"]),
                Type          = Convert.ToString(r["type"])!,
                StartUtc      = Convert.ToDateTime(r["start"]),
                EndUtc        = Convert.ToDateTime(r["end"])
            };
        }

/// <summary>
/// Inserts an appointment. Returns the new appointmentId.
/// </summary>
        public int Add(Appointment a)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                INSERT INTO appointment
                    (customerId, userId, title, type, start, `end`, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES
                    (?, ?, ?, ?, ?, ?, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');";

            using (var cmd = new OdbcCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("", a.CustomerId);
                cmd.Parameters.AddWithValue("", a.UserId);
                cmd.Parameters.AddWithValue("", (object?)a.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("", a.Type.Trim());
                cmd.Parameters.AddWithValue("", a.StartUtc);
                cmd.Parameters.AddWithValue("", a.EndUtc);
                cmd.ExecuteNonQuery();
            }

            using var lastId = new OdbcCommand("SELECT LAST_INSERT_ID();", conn);
            return Convert.ToInt32(lastId.ExecuteScalar());
        }

/// <summary>
/// Updates an appointment by ID.
/// </summary>
        public void Update(Appointment a)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                UPDATE appointment
                SET customerId = ?,
                    userId     = ?,
                    title      = ?,
                    type       = ?,
                    start      = ?,
                    `end`      = ?,
                    lastUpdate = UTC_TIMESTAMP(),
                    lastUpdateBy = 'app'
                WHERE appointmentId = ?;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", a.CustomerId);
            cmd.Parameters.AddWithValue("", a.UserId);
            cmd.Parameters.AddWithValue("", (object?)a.Title ?? DBNull.Value);
            cmd.Parameters.AddWithValue("", a.Type.Trim());
            cmd.Parameters.AddWithValue("", a.StartUtc);
            cmd.Parameters.AddWithValue("", a.EndUtc);
            cmd.Parameters.AddWithValue("", a.AppointmentId);
            cmd.ExecuteNonQuery();
        }

/// <summary>
/// Deletes an appointment by ID.
/// </summary>
        public void Delete(int appointmentId)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = "DELETE FROM appointment WHERE appointmentId = ?;";
            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", appointmentId);
            cmd.ExecuteNonQuery();
        }

/// <summary>
/// Returns appointments with start between [startUtc, endUtc) (UTC range), inclusive start, exclusive end.
/// </summary>
        public List<Appointment> GetForDayRangeUtc(DateTime startUtc, DateTime endUtc)
        {
            var list = new List<Appointment>();
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE start >= ? AND start < ?
                ORDER BY start;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", startUtc);
            cmd.Parameters.AddWithValue("", endUtc);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Appointment
                {
                    AppointmentId = Convert.ToInt32(r["appointmentId"]),
                    CustomerId    = Convert.ToInt32(r["customerId"]),
                    UserId        = Convert.ToInt32(r["userId"]),
                    Title         = r["title"] is DBNull ? null : Convert.ToString(r["title"]),
                    Type          = Convert.ToString(r["type"])!,
                    StartUtc      = Convert.ToDateTime(r["start"]),
                    EndUtc        = Convert.ToDateTime(r["end"])
                });
            }
            return list;
        }

/// <summary>
/// Returns the first upcoming appointment for a user between [startUtc, endUtc] (UTC).
/// Used for the 15-minute alert on login.
/// </summary>
        public Appointment? GetFirstForUserBetweenUtc(int userId, DateTime startUtc, DateTime endUtc)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE userId = ? AND start BETWEEN ? AND ?
                ORDER BY start
                LIMIT 1;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", userId);
            cmd.Parameters.AddWithValue("", startUtc);
            cmd.Parameters.AddWithValue("", endUtc);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new Appointment
            {
                AppointmentId = Convert.ToInt32(r["appointmentId"]),
                CustomerId    = Convert.ToInt32(r["customerId"]),
                UserId        = Convert.ToInt32(r["userId"]),
                Title         = r["title"] is DBNull ? null : Convert.ToString(r["title"]),
                Type          = Convert.ToString(r["type"])!,
                StartUtc      = Convert.ToDateTime(r["start"]),
                EndUtc        = Convert.ToDateTime(r["end"])
            };
        }
    }
}
