using AppointmentScheduler.Data;

namespace AppointmentScheduler
{
    internal static class Program
    {
        static void Main()
        {
            var userRepo = new UserRepository();
            var appointmentRepo = new AppointmentRepository();

            userRepo.Add("test", "test");

            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm(userRepo, appointmentRepo));
        }
    }
}