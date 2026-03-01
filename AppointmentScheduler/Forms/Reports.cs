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
            CustIdTxt.Visible = false;

            _user = user;
            _appointmentRepo = appointmentRepo;
            _reportHandler = new ReportHandler(_appointmentRepo);
        }

        private void CustSchedule_CheckedChanged(object sender, EventArgs e)
        {
            CustIdTxt.Visible = CustSchedule.Checked;
        }

        private void GetRpts_Click(object sender, EventArgs e)
        {
            string columns;

            if (AptByMonth.Checked)
            {
                var appointmentReport = _reportHandler.GenerateAppointmentsByMonthReport();
                columns = "|--UserId--|---Type---|----Start----|----End----|";
                MessageService.WriteReport("AppointmentsByMonthReport", appointmentReport, columns);
            }

            if (UsrSchedules.Checked)
            {
                var userScheduleReport = _reportHandler.GenerateUserScheduleReport();
                columns = "|--Id--|---Type---|----Start----|----End----|";
                MessageService.WriteReport("UserScheduleReport", userScheduleReport, columns);
            }

            if (CustSchedule.Checked)
            {
                if (!int.TryParse(CustIdTxt.Text.Trim(), out int custId))
                {
                    MessageService.DisplayMessage(_user.Language, "InvalidId", MessageBoxIcon.Warning);
                    return;
                }

                var customerScheduleReport = _reportHandler.GenerateCustomerScheduleReport(custId);
                columns = "|--Id--|---Type---|----Start----|----End----|";
                MessageService.WriteReport("CustomerScheduleReport", customerScheduleReport, columns);
            }
        }
    }
}
