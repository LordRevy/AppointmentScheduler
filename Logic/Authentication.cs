using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic;

public class Authentication
{
    private readonly UserRepository _userRepository;
    
    public Authentication(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User? ValidateCredentials(string username, string password)
    {
        var user = _userRepository.GetUser(username);
    
        if (user != null && user.Active && user.Password == password)
        {
            return user;
        }
        return null;
    }
}
