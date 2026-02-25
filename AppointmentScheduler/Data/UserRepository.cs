using MySql.Data.MySqlClient;
using System.Globalization;

namespace AppointmentScheduler.Data
{
    public class UserRepository : Database
    {
        /// <summary>
        /// Creates a User object from a single data reader row.
        /// </summary>
        private static Domain.User MapUser(MySqlDataReader r)
        {
            var _username = Convert.ToString(r["userName"]);

            if (_username == null)
                throw new InvalidOperationException("Username cannot be null.");

            return new Domain.User
            {
                Id = Convert.ToInt32(r["userId"]),
                UserName = _username,
                Language = "en"
            };
        }

        /// <summary>
        /// Returns the user with the given username so long as the password matches.
        /// Returns null if  username not found or if password is incorrect.
        /// </summary>
        public Domain.User? GetUser(string username, string password, string language)
        {
            using var conn = GetConnection();
            conn.Open();
            const string getUserSql = "SELECT userId, userName FROM `user` WHERE userName = ? AND password = ?;";

            using var cmd = new MySqlCommand(getUserSql, conn);
            cmd.Parameters.AddWithValue("", username);
            cmd.Parameters.AddWithValue("", password);
            using var r = cmd.ExecuteReader();

            if (!r.Read())
                return null;

            var user = MapUser(r);
            user.Language = language;

            var culture = CultureInfo.CurrentUICulture;
            var region = new RegionInfo(culture.Name);
            user.Country = region.EnglishName;

            var tz = TimeZoneInfo.Local;
            user.Timezone = tz.DisplayName;

            return user;
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

            using var cmd = new MySqlCommand(userListSql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                userList.Add(MapUser(r));
            }
            return userList;
        }

        /// <summary>
        /// Adding a new User to the Database. Currently only used to populate the Database with initial test user.
        /// </summary>
        public void Add(string username, string password)
        {
            using var conn = GetConnection();
            conn.Open();

            const string insertIntoUserSql = @"
            INSERT INTO user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy)
            VALUES (?, ?, true, UTC_TIMESTAMP(), 'app', UTC_TIMESTAMP(), 'app');";

            ExecuteNonQuery(insertIntoUserSql, conn, username, password);
        }
    }
}
