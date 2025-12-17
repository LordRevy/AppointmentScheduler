using System;
using System.IO;

public class LoginRecord
{
    private const string FileName = "LoginHistory.txt";
    private readonly string message;

    public Log(string username, bool result)
    {
        DateTime loginTime = DateTime.Now;
        message = $"Login Attempt | Username: {username} | Time: {loginTime:u} | Success? {result}";
      
        File.AppendAllText(FileName, message + Environment.NewLine);
    }
}
