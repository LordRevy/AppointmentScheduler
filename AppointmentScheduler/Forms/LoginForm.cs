using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Forms;
using AppointmentScheduler.Logic;
using System.Globalization;

namespace AppointmentScheduler
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepo;
        private readonly AppointmentRepository _appointmentRepo;
        private readonly LoginHandler _loginHandler;

        public LoginForm(UserRepository userRepo, AppointmentRepository appointmentRepo)
        {
            InitializeComponent();

            _userRepo = userRepo;
            _appointmentRepo = appointmentRepo;
            _loginHandler = new LoginHandler(userRepo, _appointmentRepo);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            EnglishBtn.Checked = true;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var user = null as User;
            var language = "en";

            if (LatinBtn.Checked)
                language = "la";

            try
                {
                    user = _loginHandler.AttemptLogin(Username.Text, Password.Text, language);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Login Error");
                }

            if (user == null)
            {
                Logic.MessageService.DisplayMessage(language, "LoginFailed", MessageBoxIcon.Error);
                return;
            }

            var upcoming = _loginHandler.CheckUpcomingAppointments(user);

            if (upcoming != null)
                Logic.MessageService.DisplayMessage(user.Language, "UpcomingAppointment", MessageBoxIcon.Information);

            Logic.MessageService.DisplayLoginSuccessMessage(user);
            Hide();

            new MainForm(user, _userRepo, _appointmentRepo).Show();
        }
    }
}
