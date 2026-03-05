using MySql.Data.MySqlClient;

namespace AppointmentScheduler.Data
{
    public class UserRepository : Database
    {
        /// <summary>
        /// Creates a User object from a single data reader row.
        /// </summary>
        private static Domain.User MapUser(MySqlDataReader r)
        {
            return new Domain.User
            {
                Id = Convert.ToInt32(r["userId"]),
                UserName = Convert.ToString(r["userName"])!,
                Language = "en"
            };
        }

        /// <summary>
        /// Returns the user with the given username so long as the password matches.
        /// Returns null if  username not found or if password is incorrect.
        /// </summary>
        public Domain.User? GetUser(Domain.User user, string password)
        {
            using var conn = GetConnection();
            conn.Open();
            const string getUserSql = "SELECT userId, userName FROM `user` WHERE userName = ? AND password = ?;";

            using var cmd = new MySqlCommand(getUserSql, conn);
            cmd.Parameters.AddWithValue("", user.UserName);
            cmd.Parameters.AddWithValue("", password);
            using var r = cmd.ExecuteReader();

            if (!r.Read())
                return null;

            user.Id = r.GetInt32("userId");

            if (user.Id <= 0)
                throw new Exception($"Invalid userId {user.Id} for user {user.UserName}.");

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
