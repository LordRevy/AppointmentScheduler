using AppointmentScheduler.Domain;
using System.Collections.Generic;

public class AuthService
{
  private readonly List<User> userList = new List<User>                     // TODO: Replace with a JSON-based User store.
  {                                                                         // Passwords should not be stored in plain english for real world applications. Use Hashing + Salting.
    new User
    {
      ID = 12345,
      Username = "test",
      Password = "test"
    }
  };
  
  public bool ValidateCredentials(string username, string password)
  {
    foreach(var user in userList)
    {
      if(user.Username == username && user.Password == password)
      {
        return true;
      }
    }
    return false;
  }
}
