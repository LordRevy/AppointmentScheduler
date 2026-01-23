using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic;

public class Authentication
{
    private readonly UserRepository _userRepo;
    private LoginRecord _loginRecord = new LoginRecord();
    
    public Authentication(UserRepository userRepo)
    {
        _userRepo = userRepo;
    }
    
/// <summary>
/// Querys Database for a row that matches username and password.
/// Returns a User object if successful, null if not. 
/// </summary>
    public User? ValidateCredentials(string username, string password)
    {
        var user = _userRepo.GetUser(username);
        
        if (user != null)
        {
            _loginRecord.Log(username, true);
            return user;
        }
        _loginRecord.Log(username, false);
        return null;
    }
}
