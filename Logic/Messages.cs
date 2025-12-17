using AppointmentScheduler.Domain;
using System.Collections.Generic;

namespace AppointmentScheduler.Logic
{
  public class Messages
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
  
    public string LoginMessage(User user, bool result)
    {
      if (result)
      {
        return messages[user.Language]["LoginSuccess"];
      }
      return messages[user.Language]["InvalidCredentials"];
    }
  }
}
