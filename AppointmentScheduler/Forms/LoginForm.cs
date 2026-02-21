using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Forms;
using AppointmentScheduler.Logic;
using System.Globalization;

namespace AppointmentScheduler
{
    public partial class LoginForm : Form
    {
        private readonly LoginHandler _loginHandler;
        private readonly AppointmentRepository _appointmentRepo;

        public LoginForm()
        {
            InitializeComponent();

            var userRepo = new UserRepository();
            _appointmentRepo = new AppointmentRepository();
            _loginHandler = new LoginHandler(userRepo, _appointmentRepo);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var user = null as User;

            try
            {
                user = _loginHandler.AttemptLogin(Username.Text, Password.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error");
            }

            if (user == null)
            {
                var message = Logic.MessageService.GetMessage("la", "InvalidCredentials"); //make language dynamic
                MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var upcoming = _loginHandler.CheckUpcomingAppointments(user);
            if (upcoming != null)
            {
                var appointmentMessage = Logic.MessageService.GetMessage(user.Language, "UpcomingAppointment");

                MessageBox.Show(appointmentMessage, "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Hide();
            new MainForm(user, _appointmentRepo).Show();
        }
    }
}
