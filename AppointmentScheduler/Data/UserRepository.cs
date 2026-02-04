using System.Data.Odbc;
using System.Globalization;

namespace AppointmentScheduler.Data
{
    public class UserRepository : Database
    {
        /// <summary>
        /// Creates a User object from a single data reader row.
        /// </summary>
        private static Domain.User MapUser(OdbcDataReader r)
        {
            var c = CultureInfo.CurrentUICulture;
            var parts = c.Name.Split('-');
            var timezone = TimeZoneInfo.Local;

            return new Domain.User
            {
                Id = Convert.ToInt32(r["userId"]),
                UserName = Convert.ToString(r["userName"]) ?? string.Empty,
                Language = parts[0],
                Country = parts[1],
                Timezone = timezone.Id
            };
        }

        /// <summary>
        /// Returns the user with the given username so long as the password matches.
        /// Returns null if  username not found or if password is incorrect.
        /// </summary>
        public Domain.User? GetUser(string username, string password)
        {
            using var conn = GetConnection();
            conn.Open();
            const string getUserSql = "SELECT userId, userName FROM `user` WHERE userName = ? AND password = ?;";

            using var cmd = new OdbcCommand(getUserSql, conn);
            cmd.Parameters.AddWithValue("", username);
            cmd.Parameters.AddWithValue("", password);
            using var r = cmd.ExecuteReader();

            if (!r.Read())
                return null;

            return MapUser(r);

        }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        public List<Domain.User> GetAll()
        {
            var userList = new List<Domain.User>();
            using var conn = GetConnection();
            conn.Open();
            const string userListSql = "SELECT userId, userName FROM `user`;";

            using var cmd = new OdbcCommand(userListSql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                userList.Add(MapUser(r));
            }
            return userList;
        }
    }
}
