using System;
using System.Collections.Generic;
using System.Data.Odbc;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Data;

public class UserRepository : Database
{
    /// <summary>
    /// Creates a User object from a single data reader row.
    /// </summary>
    private static User MapUser(OdbcDataReader r)
    {
        return new User
        {
            UserId   = Convert.ToInt32(r["userId"]),
            UserName = Convert.ToString(r["userName"])
        };
    }
    /// <summary>
    /// Returns the user with the given username, or null if not found.
    /// </summary>
    public User? GetByUsername(string username)
    {
        using var conn = GetConnection();
        conn.Open();
        const string getUserSql = "SELECT userId, userName FROM `user` WHERE userName = ?;";

        using var cmd = new OdbcCommand(getUserSql, conn);
        cmd.Parameters.AddWithValue("", username);
        using var r = cmd.ExecuteReader();
        
        return r.Read() ? MapUser(r) : null;
    }

    /// <summary>
    /// Returns a list of all users.
    /// </summary>
    public List<User> GetAll()
    {
        var userList = new List<User>();
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
