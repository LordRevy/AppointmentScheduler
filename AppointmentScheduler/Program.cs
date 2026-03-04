using AppointmentScheduler.Data;

namespace AppointmentScheduler
{
    internal static class Program
    {
        static void Main()
        {
            var userRepo = new UserRepository();
            var appointmentRepo = new AppointmentRepository();

            var userList = userRepo.GetAll();
            if (!userList.Any(u => u.UserName == "test"))
                userRepo.Add("test", "test");

            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm(userRepo, appointmentRepo));
        }
    }
}