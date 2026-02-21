using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Logic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentScheduler.Forms
{
    public partial class MainForm : Form
    {
        private readonly Domain.User _currentUser;
        private readonly UserRepository _userRepo;
        private readonly AppointmentRepository _appointmentRepo;
        public MainForm(Domain.User user, UserRepository userRepo, AppointmentRepository appointmentRepository)
        {
            InitializeComponent();

            _currentUser = user;
            _userRepo = userRepo;
            _appointmentRepo = appointmentRepository;
            var _customerRepo = new CustomerRepository();
        }

        private void AddAptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var customerId = Convert.ToInt32(CustIdText.Text);
                var userId = Convert.ToInt32(UserIdText.Text);
                var title = TitleText.Text.Trim();
                var type = TypeText.Text.Trim();
                var start = DateTime.Parse(StartText.Text);
                var end = DateTime.Parse(EndText.Text);

                var appointmentId = _appointmentRepo.Add(customerId, userId, title, type, start, end);

                MessageBox.Show($"Appointment added with ID: {appointmentId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Adding Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenRptBtn_Click(object sender, EventArgs e)
        {
            new Reports(_currentUser, _appointmentRepo).Show();
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
