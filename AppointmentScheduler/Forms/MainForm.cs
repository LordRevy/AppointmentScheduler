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
                // Validate input fields
                var apt = new Appointment
                {
                    CustomerId = Convert.ToInt32(CustIdText.Text),
                    UserId = Convert.ToInt32(UserIdText.Text),
                    Title = TitleText.Text.Trim(),
                    Type = TypeText.Text.Trim(),
                    Start = GetDateTime(AptDate.Value, AptStartTime.Value),
                    End = GetDateTime(AptDate.Value, AptEndTime.Value)
                };

                // Validate appointment times are within business hours and do not overlap with existing appointments
                if (!_validator.ValidateAppointment(apt))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "AppointmentOverlap", MessageBoxIcon.Warning);
                    return;
                }

                // Attempt to add appointment to repository
                var appointmentId = _appointmentRepo.Add(apt).ToString();
                MessageService.DisplayMessage(_currentUser.Language, "AddedAppointment", MessageBoxIcon.Information, appointmentId);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddAppointment", ex);
            }

            UpdateTables();
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            var name = NameText.Text.Trim();
            var address = AddressText.Text.Trim();
            var phone = PhoneText.Text.Trim();

            if (!_validator.ValidateCustomer(name, address, phone))
            {
                MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                return;
            }

            // Attempt to add customer to repository
            try
            {
                var customerId = _customerRepo.Add(name, address, phone).ToString();
                MessageService.DisplayMessage(_currentUser.Language, "AddedCustomer", MessageBoxIcon.Information, customerId);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddCustomer", ex);
            }

            UpdateTables();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            int appointmentId;
            Appointment appointment;

            // Find Appointment using ID
            try
            {
                appointmentId = Convert.ToInt32(AptIdText.Text);
                appointment = _appointmentRepo.GetById(appointmentId) ?? throw new Exception("Appointment not found");

            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "AppointmentIDMissing", ex);
                return;
            }

            // Update fields if new values provided
            appointment.CustomerId = UpdateInt(CustIdText, appointment.CustomerId);
            appointment.UserId = UpdateInt(UserIdText, appointment.UserId);
            appointment.Title = UpdateString(TitleText, appointment.Title);
            appointment.Type = UpdateString(TypeText, appointment.Type);
            var newStartDate = GetDateTime(AptDate.Value, AptStartTime.Value);
            var newEndDate = GetDateTime(AptDate.Value, AptEndTime.Value);

            // Validate updated appointment times if they were changed
            if (newStartDate != appointment.Start || newEndDate != appointment.End)
            {
                appointment.Start = newStartDate;
                appointment.End = newEndDate;

                if (_validator.ValidateAppointment(appointment))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "AppointmentOverlap", MessageBoxIcon.Warning);
                    return;
                }
            }

            // Confirm update with user
            if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                return;

            // Attempt to update appointment in repository
            try
            {
                _appointmentRepo.Update(appointment);
                MessageService.DisplayMessage(_currentUser.Language, "AddedAppointment", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToAddAppointment", ex);
            }

            UpdateTables();
        }

        private void UpdateCustBtn_Click(object sender, EventArgs e)
        {
            int customerId;

            // Find Customer using ID
            try
            {
                customerId = Convert.ToInt32(CustTableId.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "CustomerMissing", ex);
                System.Diagnostics.Debug.Write($"Customer ID could not be parsed from input: '{CustIdText.Text}'");
                return;
            }

            // Confirm update with user
            if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                return;

            // Load Customer from repository
            var customer = _customerRepo.GetById(customerId);
            if (customer == null)
            {
                MessageService.DisplayMessage(_currentUser.Language, "CustomerMissing", MessageBoxIcon.Error);
                System.Diagnostics.Debug.Write($"Customer with ID {customerId} not found.");
                return;
            }

            // Update fields if new values provided
            customer.Name = UpdateString(NameText, customer.Name);
            customer.Address = UpdateString(AddressText, customer.Address);
            customer.Phone = UpdateString(PhoneText, customer.Phone);

            // Validate updated customer
            if (!_validator.ValidateCustomer(customer.Name, customer.Address, customer.Phone))
            {
                MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                return;
            }

            // Attempt to update customer in repository
            try
            {
                _customerRepo.Update(customer);
                MessageService.DisplayMessage(_currentUser.Language, "UpdatedCustomer", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToUpdateCustomer", ex);
            }

            UpdateTables();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            Appointment appointment;

            // Find Appointment using ID
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

            // Confirm deletion with user
            if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                return;

            // Attempt to delete appointment
            try
            {
                _appointmentRepo.Delete(appointment);
                MessageService.DisplayMessage(_currentUser.Language, "DeletedAppointment", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToDeleteAppointment", ex);
            }

            UpdateTables();
        }

        private void DeleteCustBtn_Click(object sender, EventArgs e)
        {
            Customer customer;

            // Find Customer using ID
            try
            {
                var customerId = Convert.ToInt32(CustTableId.Text);
                customer = _customerRepo.GetById(customerId) ?? throw new Exception("Customer not found");
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "CustomerIDMissing", ex);
                return;
            }

            // Confirm deletion with user
            if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                return;

            // Attempt to delete customer
            try
            {
                _customerRepo.Delete(customer);
                MessageService.DisplayMessage(_currentUser.Language, "DeletedCustomer", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageService.DisplayErrorMessage(_currentUser.Language, "FailedToDeleteCustomer", ex);
            }

            UpdateTables();
        }

        private void GenRptBtn_Click(object sender, EventArgs e)
        {
            new Reports(_currentUser, _appointmentRepo).Show();
        }

        private void UpdateTables()
        {
            CustomerTable.Columns.Clear();
            AppointmentTable.Columns.Clear();

            //Customer Table
            CustomerTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Customer ID",
                DataPropertyName = "Id"
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

            // Appointment Table
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



        // Helper method to combine date and time inputs into a single DateTime object
        private DateTime GetDateTime(DateTime date, DateTime time)
        {
            DateTime appointmentTime = new DateTime(
                date.Year,
                date.Month,
                date.Day,
                time.Hour,
                time.Minute,
                0
            );

            return appointmentTime;
        }

        // Helper methods to update fields only if new values provided, otherwise keep existing values
        private string UpdateString(TextBox box, string defaultValue)
        {
            var userInput = box.Text.Trim();
            return string.IsNullOrWhiteSpace(userInput) ? defaultValue : userInput;
        }

        private int UpdateInt(TextBox box, int defaultValue)
        {
            var userInput = box.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
                return defaultValue;

            try
            {
                return Convert.ToInt32(userInput);
            }
            catch
            {
                return defaultValue;
            }
        }

        // Populate appointment fields when an appointment is selected from the table
        private void AppointmentTable_SelectionChanged(object sender, EventArgs e)
        {
            if (AppointmentTable.SelectedRows.Count == 0)
                return;

            Appointment selectedApt = (Appointment)AppointmentTable.SelectedRows[0].DataBoundItem;

            AptIdText.Text = selectedApt.AppointmentId.ToString();
            CustIdText.Text = selectedApt.CustomerId.ToString();
            UserIdText.Text = selectedApt.UserId.ToString();
            TitleText.Text = selectedApt.Title;
            TypeText.Text = selectedApt.Type;
            AptDate.Value = selectedApt.Start.Date;
            AptStartTime.Value = selectedApt.Start;
            AptEndTime.Value = selectedApt.End;
        }

        // Populate customer fields when a customer is selected from the table
        private void CustomerTable_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomerTable.SelectedRows.Count == 0)
                return;

            Customer selectedCust = (Customer)CustomerTable.SelectedRows[0].DataBoundItem;

            CustTableId.Text = selectedCust.Id.ToString();
            NameText.Text = selectedCust.Name;
            AddressText.Text = selectedCust.Address;
            PhoneText.Text = selectedCust.Phone;
        }
    }
}
