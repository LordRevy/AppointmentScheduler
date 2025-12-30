using System;
using System.IO;

namespace AppointmentScheduler.Logic;

public class LoginService
{
    private const string FileName = "Records/Login_History.txt";
    private readonly AppointmentService _appointmentService;

    public LoginService(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public void LogAttempt(string username, bool result)
    {
        Directory.CreateDirectory("Records");

        var loginTime = DateTime.UtcNow;
        var message = $"Login Attempt | Username: {username} | Time: {loginTime:u} | Success? {result}";

        File.AppendAllText(FileName, message + Environment.NewLine);
    }

    public DateTime? CheckAlerts(User user)
    {
        var appointmentTime = _appointmentService.GetEarliestAppointment(user);
        
        if (appointmentTime == null)
            return null;

        var timeUntilAppointment = (appointmentTime.Value - DateTime.Now).TotalMinutes;

        if (timeUntilAppointment >= 0 && timeUntilAppointment <= 15)
            return appointmentTime.Value;

        return null;
    }
}
