using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppointmentScheduler.Logic;

namespace AppointmentScheduler.Forms
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            AptByMonthTxt.Visible = false;
            CustScheduleTxt.Visible = false;
        }

        private void AptByMonth_CheckedChanged(object sender, EventArgs e)
        {
            AptByMonthTxt.Visible = AptByMonth.Checked;
        }

        private void CustSchedule_CheckedChanged(object sender, EventArgs e)
        {
            CustScheduleTxt.Visible = CustSchedule.Checked;
        }

        private void GetRpts_Click(object sender, EventArgs e)
        {
            List<Report> reports = [];

            if (AptByMonth.Checked)
                reports.Append(ReportHandler.GetAppointmentsByMonth(AptByMonthTxt.Text));

            if (UsrSchedules.Checked)
                reports.Append(ReportHandler.GetUserSchedules());

            if (CustSchedule.Checked)
                reports.Append(ReportHandler.GetCustomerSchedules(CustScheduleTxt.Text));
        }
}
