using AppointmentScheduler.Domain;
using System.Data.Odbc;

namespace AppointmentScheduler.Data
{
    public class AppointmentRepository : Database
    {
        /// <summary>
        /// Creates an Appointment object from a single data reader row.
        /// </summary>
        private static Appointment MapAppointment(OdbcDataReader r)
        {
            return new Appointment
            {
                AppointmentId = Convert.ToInt32(r["appointmentId"]),
                CustomerId = Convert.ToInt32(r["customerId"]),
                UserId = Convert.ToInt32(r["userId"]),
                Title = Convert.ToString(r["title"])!,
                Type = Convert.ToString(r["type"])!,
                Start = Convert.ToDateTime(r["start"]),
                End = Convert.ToDateTime(r["end"])
            };
        }

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
            cmd.Parameters.AddWithValue(string.Empty, appointmentId);

            using var r = cmd.ExecuteReader();
            return r.Read() ? MapAppointment(r) : null;
        }

        /// <summary>
        /// Creates a new Appointment and returns the appointmentId that is created from it.
        /// </summary>
        public int Add(int customerId, int userId, string title, string type, DateTime start, DateTime end)
        {
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                INSERT INTO appointment
                    (customerId, userId, title, type, start, `end`,
                     createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES
                    (?, ?, ?, ?, ?, ?, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');";

            ExecuteNonQuery(
                sql,
                conn,
                customerId,
                userId,
                title.Trim(),
                type.Trim(),
                start,
                end
            );

            return GetCreatedId(conn);
        }

        /// <summary>
        /// Returns a list of all appointments in the database.
        /// </summary>
        public List<Appointment> GetAll()
        {
            var appointments = new List<Appointment>();
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                SELECT 
                    appointmentId, 
                    customerId, 
                    userId, 
                    title, 
                    type, 
                    start, 
                    `end`
                FROM appointment;";

            using var cmd = new OdbcCommand(sql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                appointments.Add(MapAppointment(r));
            }

            return appointments;
        }

        /// <summary>
        /// Updates an appointment from the ID.
        /// </summary>
        public void Update(Appointment a)
        {
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                UPDATE appointment
                SET customerId   = ?,
                    userId       = ?,
                    title        = ?,
                    type         = ?,
                    start        = ?,
                    `end`        = ?,
                    lastUpdate   = UTC_TIMESTAMP(),
                    lastUpdateBy = 'app'
                WHERE appointmentId = ?;";

            ExecuteNonQuery(
                sql,
                conn,
                a.CustomerId,
                a.UserId,
                a.Title.Trim(),
                a.Type.Trim(),
                a.Start,
                a.End,
                a.AppointmentId
            );
        }

        /// <summary>
        /// Deletes an appointment, again by using its ID.
        /// </summary>
        public void Delete(Appointment a)
        {
            using var conn = GetConnection();
            conn.Open();

            const string sql = "DELETE FROM appointment WHERE appointmentId = ?;";
            ExecuteNonQuery(sql, conn, a.AppointmentId);
        }

        /// <summary>
        /// Returns appointments with start and end times that overlap in the database.
        /// </summary>
        public List<Appointment> GetDateRange(DateTime start, DateTime end)
        {
            var appointmentsInRange = new List<Appointment>();
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE start >= ? AND start < ?
                ORDER BY start;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue(string.Empty, start);
            cmd.Parameters.AddWithValue(string.Empty, end);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                appointmentsInRange.Add(MapAppointment(r));
            }

            return appointmentsInRange;
        }

        /// <summary>
        /// Similar to GetDateRange but simply returns a bool if the input appointment overlaps with database entries.
        /// </summary>
        public bool CheckAppointmentOverlap(DateTime start, DateTime end)
        {
            var appointmentsInRange = new List<Appointment>();
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE start >= ? AND start < ?
                ORDER BY start;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue(string.Empty, start);
            cmd.Parameters.AddWithValue(string.Empty, end);

            using var r = cmd.ExecuteReader();
            return r.Read();
        }

        /// <summary>
        /// Returns the first upcoming appointment for a user within the given UTC window.
        /// Used for the 15-minute alert on login.
        /// </summary>
        public Appointment? GetEarliest(Domain.User user)
        {
            using var conn = GetConnection();
            conn.Open();

            const string sql = @"
                SELECT appointmentId, customerId, userId, title, type, start, `end`
                FROM appointment
                WHERE userId = ?
                ORDER BY start";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue(string.Empty, user.Id);

            using var r = cmd.ExecuteReader();
            return r.Read() ? MapAppointment(r) : null; // this may return more than one appointment. need to fix.
        }
    }
}
