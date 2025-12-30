using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic;

///<summary>
/// Compares usernames to the database to find a match.
/// If a match is found, returns user only if the Password matches and they are currently Active.
///</summary>
public class Authentication
{
    private readonly UserRepository _users;
    private LoginRecord _loginAttempts;
    public Authentication(UserRepository users, LoginRecord loginAttempts)
    {
        _users = users;
        _loginAttempts = loginAttempts;
    }
    
    
    public User? ValidateCredentials(string username, string password)
    {
        var user = _users.GetUser(username);
        if (user != null && user.Active && user.Password == password)
        {
            _loginAttempts.Log(username, true);
            return user;
        }
        _loginAttempts.Log(username, false);
        return null;
    }
}
