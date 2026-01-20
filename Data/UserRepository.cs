using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data
{
    public class UserRepository : Database
    {
/// <summary>
/// Returns the user with the given username, or null if not found.
/// Does NOT check password; login is enforced in AuthService as "test"/"test" per rubric.
/// </summary>
        public User? GetByUsername(string username)
        {
            using var conn = GetConnection();
            conn.Open();
            const string sql = "SELECT userId, userName FROM `user` WHERE userName = ?;";

            using var cmd = new OdbcCommand(sql, conn);
            cmd.Parameters.AddWithValue("", username);
            using var r = cmd.ExecuteReader();
            
            if (!r.Read())
                return null;

            return new User
            {
                UserId   = Convert.ToInt32(r["userId"]),
                UserName = Convert.ToString(r["userName"])
            };
        }

/// <summary>
/// Returns a list of all users.
/// </summary>
        public List<User> GetAll()
        {
            var list = new List<User>();
            using var conn = GetConnection();
            conn.Open();
            const string sql = "SELECT userId, userName FROM `user`;";

            using var cmd = new OdbcCommand(sql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                list.Add(new User
                {
                    UserId   = Convert.ToInt32(r["userId"]),
                    UserName = Convert.ToString(r["userName"])
                });
            }
            return list;
        }
    }
}
