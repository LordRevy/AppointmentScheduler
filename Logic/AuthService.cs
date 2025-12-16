using AppointmentScheduler.Domain;
using System.Collections.Generic;

public class AuthService
{
  private List<User> userList = new List<User>
  {
    new User
    {
      ID = 12345,
      Username = "test",
      Password = "test"
    }
  };
  
  public bool ValidateCredentials (string username, string password)
  {
    foreach (var user in userList)
    {
      if (user.Username == username && user.Password == password)
      {
        return true;
      }
    }
    return false;
  }
}
