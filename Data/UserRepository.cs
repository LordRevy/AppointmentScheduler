using AppointmentScheduler.Domain;
using System.Data.Odbc;

namespace AppointmentScheduler.Data
{
    public class UserRepository : Repository
    {
        public User? GetUser(string username)
        {
            string sql = "SELECT * FROM user WHERE username = ?";
            return GetRow(sql, reader => new User
            {
                UserId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                Password = reader.GetString(2),
                Active = reader.GetByte(3) == 1, // Gets the 0 or 1 and compares it to 1. If true it evaluates to the boolean True, if not, it becomes False.
                CreateDate = reader.GetDateTime(4),
                CreatedBy = reader.GetString(5),
                LastUpdate = reader.GetDateTime(6),
                LastUpdateBy = reader.GetString(7)
            }, username);
        }
    }
}
