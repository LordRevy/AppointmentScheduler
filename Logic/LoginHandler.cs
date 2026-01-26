using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic;

public class LoginHandler
{
    private readonly UserRepository _userRepo;
    private readonly AppointmentRepository _appointmentRepo
    
    public LoginHandler(UserRepository userRepo, AppointmentRepository appointmentRepo)
    {
        _userRepo = userRepo;
        _appointmentRepo = appointmentRepo;
    }
    
    /// <summary>
    /// Querys Database for a row that matches username and password.
    /// Returns a User object if successful, null if not. 
    /// </summary>    
    public User? AttemptLogin(string username, string password)
    {
        username = username.Trim();
        password = password.Trim();

        var user = _userRepo.GetUser(username, password);

        if (user is null)
        {
            LogAttempt(username, false);
            return null;
        }

        LogAttempt(username, true);
        CheckUpcomingAppointments(user);
        return user;
    }

    /// <summary>
    /// Checks if the logged in user has an appointment within 15 minutes.
    /// </summary>
    public Appointment? CheckUpcomingAppointments(User user)
    {
        var appointment = _appointmentRepo.GetEarliest(user);
        if (appointment is null)
            return null;

        var minutesUntil = (appointment.Start - DateTime.UtcNow).TotalMinutes;
        if (minutesUntil <= 15 && minutesUntil > 0)
            return appointment;

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
