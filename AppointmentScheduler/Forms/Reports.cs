using AppointmentScheduler.Logic;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Forms
{
    public partial class Reports : Form
    {
        private readonly User _user;
        private readonly AppointmentRepository _appointmentRepo;
        private readonly ReportHandler _reportHandler;

        public Reports(User user, AppointmentRepository appointmentRepo)
        {
            InitializeComponent();
            AptByMonthTxt.Visible = false;
            CustIdTxt.Visible = false;

            _user = user;
            _appointmentRepo = appointmentRepo;
            _reportHandler = new ReportHandler(_appointmentRepo);
        }

        private void AptByMonth_CheckedChanged(object sender, EventArgs e)
        {
            AptByMonthTxt.Visible = AptByMonth.Checked;
        }

        private void CustSchedule_CheckedChanged(object sender, EventArgs e)
        {
            CustIdTxt.Visible = CustSchedule.Checked;
        }

        private void GetRpts_Click(object sender, EventArgs e)
        {
            if (AptByMonth.Checked)
            {
                var appointmentReport = _reportHandler.GenerateAppointmentsByMonthReport();
                MessageService.WriteReport("AppointmentsByMonthReport", appointmentReport);
            }

            if (UsrSchedules.Checked)
            {
                var userScheduleReport = _reportHandler.GenerateUserScheduleReport();
                MessageService.WriteReport("UserScheduleReport", userScheduleReport);
            }

            if (CustSchedule.Checked)
            {
                if (!int.TryParse(CustIdTxt.Text.Trim(), out int custId))
                {
                    MessageService.DisplayMessage(_user.Language, "InvalidId", MessageBoxIcon.Warning);
                    return;
                }

                var customerScheduleReport = _reportHandler.GenerateCustomerScheduleReport(custId);
                MessageService.WriteReport("CustomerScheduleReport", customerScheduleReport);
            }
        }
    }
}
