using AppointmentScheduler.Domain;
using System.Collections.Generic;

namespace AppointmentScheduler.Logic;

public class MessageService
{
    private readonly Dictionary<string, Dictionary<string, string>> messages =
        new Dictionary<string, Dictionary<string, string>>
    {
        ["en"] = new Dictionary<string, string>
        {
            ["LoginSuccess"] = "Login successful!",
            ["InvalidCredentials"] = "Invalid username or password."
        },
        ["ru"] = new Dictionary<string, string>
        {
            ["LoginSuccess"] = "вход выполнен успешно!",
            ["InvalidCredentials"] = "Неверное имя пользователя или пароль."
        }
    };
    
    public string GetMessage(string language, string message)
    {
        if (!messages.ContainsKey(language))
            language = "en";
        
        if (!messages[language].ContainsKey(message))
            return $"{language} is missing message: {message}";
        
        return messages[language][message];
    }
}
