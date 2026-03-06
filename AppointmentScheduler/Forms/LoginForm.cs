using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Forms;
using AppointmentScheduler.Logic;

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
            var username = Username.Text.Trim();
            var password = Password.Text.Trim();
            var language = "en";

            if (LatinBtn.Checked)
                language = "la";

            if (username == "" || password == "")
            {
                Logic.MessageService.DisplayMessage(language, "LoginFailed", MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Id = 0,
                UserName = username,
                Language = language,
                Timezone = MapSelectionToTimeZone(comboBox1.Text)
            };

            try
            {
                user = _userRepo.GetUser(user, password);

                if (user == null)
                {
                    LoginHandler.LogAttempt(username, false);
                    Logic.MessageService.DisplayMessage(language, "LoginFailed", MessageBoxIcon.Error);
                    return;
                }

                LoginHandler.LogAttempt(username, true);
                var upcoming = _loginHandler.CheckUpcomingAppointments(user);

                if (upcoming != null)
                    Logic.MessageService.DisplayMessage(user.Language, "UpcomingAppointment", MessageBoxIcon.Information);

                Logic.MessageService.DisplayLoginSuccessMessage(user);
                Hide();

                new MainForm(user, _userRepo, _appointmentRepo).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error");
            }
        }

        private TimeZoneInfo MapSelectionToTimeZone(string selection)
        {
            return selection switch
            {
                "User Location" => TimeZoneInfo.Local,
                "Mountain" => TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time"),
                "Eastern" => TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"),
                "Pacific" => TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"),
                "Central European" => TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time"),
                _ => TimeZoneInfo.Utc
            };
        }
    }
}
