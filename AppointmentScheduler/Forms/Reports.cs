using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppointmentScheduler.Logic;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Forms
{
    public partial class Reports : Form
    {
        private readonly Domain.User _user;
        private readonly Data.AppointmentRepository _appointmentRepository;
        private readonly ReportHandler _reportHandler;

        public Reports(Domain.User user, Data.AppointmentRepository appointmentRepository)
        {
            InitializeComponent();
            AptByMonthTxt.Visible = false;
            CustIdTxt.Visible = false;

            _user = user;
            _appointmentRepository = appointmentRepository;
            _reportHandler = new ReportHandler(_appointmentRepository);
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
            List<AppointTypeByMonthReport> appointmentReport = [];
            List<ScheduleReport> userScheduleReport = [];
            List<ScheduleReport> customerScheduleReport = [];

            if (AptByMonth.Checked)
                appointmentReport = _reportHandler.GenerateAppointmentsByMonthReport();

            if (UsrSchedules.Checked)
                userScheduleReport = _reportHandler.GenerateUserScheduleReport();

            if (CustSchedule.Checked)
            {
                var custIdText = CustIdTxt.Text.Trim();
                if (int.TryParse(custIdText , out int custId))
                    customerScheduleReport = _reportHandler.GenerateCustomerScheduleReport(custId);
                else
                    MessageService.DisplayMessage(_user.Language, "InvalidId", MessageBoxIcon.Warning);
            }
        }
    }
}
