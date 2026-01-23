using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic;

public class Authentication
{
    private readonly UserRepository _userRepo;
    
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
            LogAttempt(username, true);
            return user;
        }
        LogAttempt(username, false);
        return null;
    }

/// <summary>
/// Takes a login attempt and records it to a txt file called Login_History.
/// </summary>
    public void LogAttempt(string username, bool result)
    {
        private const string loginPath = "Login_History.txt";

        var loginTime = DateTime.UtcNow;
        var message = $@"
            Login Attempt 
            | Username: {username} 
            | Time: {loginTime:u} 
            | Result: {result}";

        File.AppendAllText(loginPath, message + Environment.NewLine);
    }
}
