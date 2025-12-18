using System;
using System.IO;

namespace AppointmentScheduler.Logic
{
    public class LoginRecord
    {
        private const string FileName = "Records/LoginHistory.txt";
        private readonly string message;
    
        public void Log(string username, bool result)
        {
            DateTime loginTime = DateTime.Now;
            message = $"Login Attempt | Username: {username} | Time: {loginTime:u} | Success? {result}";
          
            File.AppendAllText(FileName, message + Environment.NewLine);
        }
    }
}
