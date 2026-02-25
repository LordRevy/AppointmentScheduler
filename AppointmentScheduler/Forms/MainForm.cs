using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Logic;

namespace AppointmentScheduler.Forms
{
    public partial class MainForm : Form
    {
        private readonly Domain.User _currentUser;
        private readonly UserRepository _userRepo;
        private readonly AppointmentRepository _appointmentRepo;
        private readonly CustomerRepository _customerRepo;
        private readonly Validator _validator;

        public MainForm(Domain.User user, UserRepository userRepo, AppointmentRepository appointmentRepository)
        {
            InitializeComponent();

            _currentUser = user;
            _userRepo = userRepo;
            _appointmentRepo = appointmentRepository;
            _customerRepo = new CustomerRepository();
            _validator = new Validator(_appointmentRepo);

            CustomerTable.AutoGenerateColumns = false;
            AppointmentTable.AutoGenerateColumns = false;
            UpdateTables();
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

                if (!_validator.ValidateAppointment(start, end))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "AppointmentOverlap", MessageBoxIcon.Warning);
                    return;
                }

                var appointmentId = _appointmentRepo.Add(customerId, userId, title, type, start, end);
                MessageService.DisplayMessage(_currentUser.Language, "AddedAppointment", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddAppointment", ex);
            }
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            var name = NameText.Text.Trim();
            var address = AddressText.Text.Trim();
            var phone = PhoneText.Text.Trim();

            if (!_validator.ValidateCustomer(name, address, phone))
            {
                MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var customerId = _customerRepo.Add(name, address, phone);
                MessageService.DisplayMessage(_currentUser.Language, "AddedCustomer", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddCustomer", ex);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            int appointmentId;
            Appointment appointment;

            try
            {
                appointmentId = Convert.ToInt32(AptIdText.Text);
                appointment = _appointmentRepo.GetById(appointmentId) ?? throw new Exception("Appointment not found");

                if (!string.IsNullOrWhiteSpace(CustIdText.Text))
                    appointment.CustomerId = Convert.ToInt32(CustIdText.Text);

                if (!string.IsNullOrWhiteSpace(UserIdText.Text))
                    appointment.UserId = Convert.ToInt32(UserIdText.Text);

                if (!string.IsNullOrWhiteSpace(TitleText.Text))
                    appointment.Title = TitleText.Text.Trim();

                if (!string.IsNullOrWhiteSpace(TypeText.Text))
                    appointment.Type = TypeText.Text.Trim();

                if (!string.IsNullOrWhiteSpace(StartText.Text))
                    appointment.Start = DateTime.Parse(StartText.Text);

                if (!string.IsNullOrWhiteSpace(EndText.Text))
                    appointment.End = DateTime.Parse(EndText.Text);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "AppointmentIDMissing", ex);
                return;
            }

            if (!_validator.ValidateAppointment(appointment.Start, appointment.End))
            {
                MessageService.DisplayMessage(_currentUser.Language, "AppointmentOverlap", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _appointmentRepo.Update(appointment);
                MessageService.DisplayMessage(_currentUser.Language, "AddedAppointment", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddAppointment", ex);
            }
        }

        private void UpdateCustBtn_Click(object sender, EventArgs e)
        {
            int customerId;
            Customer customer;

            try
            {
                customerId = Convert.ToInt32(CustIdText.Text);
                customer = _customerRepo.GetById(customerId) ?? throw new Exception("Customer not found");

                if (!string.IsNullOrWhiteSpace(NameText.Text))
                    customer.Name = NameText.Text.Trim();

                if (!string.IsNullOrWhiteSpace(AddressText.Text))
                    customer.Address = AddressText.Text.Trim();

                if (!string.IsNullOrWhiteSpace(PhoneText.Text))
                    customer.Phone = PhoneText.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "CustomerIDMissing", ex);
                return;
            }

            if (!_validator.ValidateCustomer(customer.Name, customer.Address, customer.Phone))
            {
                MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _customerRepo.Update(customer);
                MessageService.DisplayMessage(_currentUser.Language, "UpdatedCustomer", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToUpdateCustomer", ex);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            Appointment appointment;

            try
            {
                var appointmentId = Convert.ToInt32(AptIdText.Text);
                appointment = _appointmentRepo.GetById(appointmentId) ?? throw new Exception("Appointment not found");
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "AppointmentIDMissing", ex);
                return;
            }

            try
            {
                _appointmentRepo.Delete(appointment);
                MessageService.DisplayMessage(_currentUser.Language, "DeletedAppointment", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToDeleteAppointment", ex);
            }
        }

        private void DeleteCustBtn_Click(object sender, EventArgs e)
        {
            Customer customer;

            try
            {
                var customerId = Convert.ToInt32(CustIdText.Text);
                customer = _customerRepo.GetById(customerId) ?? throw new Exception("Customer not found");
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "CustomerIDMissing", ex);
                return;
            }

            try
            {
                _customerRepo.Delete(customer);
                MessageService.DisplayMessage(_currentUser.Language, "DeletedCustomer", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToDeleteCustomer", ex);
            }
        }

        private void GenRptBtn_Click(object sender, EventArgs e)
        {
            new Reports(_currentUser, _appointmentRepo).Show();
        }

        private void UpdateTables()
        {
            CustomerTable.Columns.Clear();
            AppointmentTable.Columns.Clear();

            CustomerTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Customer ID",
                DataPropertyName = "CustomerId"
            });
            CustomerTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                DataPropertyName = "Name"
            });
            CustomerTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Address",
                DataPropertyName = "Address"
            });
            CustomerTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Phone",
                DataPropertyName = "Phone"
            });

            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Appointment ID",
                DataPropertyName = "AppointmentId"
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Customer ID",
                DataPropertyName = "CustomerId"
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "User ID",
                DataPropertyName = "UserId"
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Title",
                DataPropertyName = "Title"
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Type",
                DataPropertyName = "Type"
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Start",
                DataPropertyName = "Start",
                DefaultCellStyle = { Format = "g" }
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "End",
                DataPropertyName = "End",
                DefaultCellStyle = { Format = "g" }
            });

            CustomerTable.DataSource = _customerRepo.GetAll();
            AppointmentTable.DataSource = _appointmentRepo.GetAll();
        }
    }
}
