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
            UpdateAppointmentTable();
            UpdateCustomerTable();
        }

        private void AddAptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                var apt = GetAppointmentFromInput();

                // Validate appointment times are within business hours and do not overlap with existing appointments
                if (!_validator.ValidateAppointment(apt))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "InvalidTime", MessageBoxIcon.Warning);
                    return;
                }

                if (_appointmentRepo.CheckAppointmentOverlap(apt))
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
                MessageService.DisplayMessage(_currentUser.Language, "FailedToAddAppointment", MessageBoxIcon.Error, ex.ToString());
            }

            UpdateAppointmentTable();
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            var cust = GetCustomerFromInput();

            if (!_validator.ValidateCustomer(cust))
            {
                MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                return;
            }

            // Attempt to add customer to repository
            try
            {
                var customerId = _customerRepo.Add(cust).ToString();
                MessageService.DisplayMessage(_currentUser.Language, "AddedCustomer", MessageBoxIcon.Information, customerId);
            }
            catch (Exception ex)
            {
                MessageService.DisplayMessage(_currentUser.Language, "FailedToAddCustomer", MessageBoxIcon.Error, ex.ToString());
            }

            UpdateCustomerTable();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get appointment ID and updated info from input fields
                int appointmentId = Convert.ToInt32(AptIdText.Text);
                var updatedAppointment = GetAppointmentFromInput();
                updatedAppointment.AppointmentId = appointmentId;

                if (!_validator.ValidateAppointment(updatedAppointment))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "InvalidTime", MessageBoxIcon.Warning);
                    return;
                }

                if (_appointmentRepo.CheckAppointmentOverlap(updatedAppointment))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "AppointmentOverlap", MessageBoxIcon.Warning);
                    return;
                }

                // Confirm update with user and attempt to update appointment in repository
                if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                    return;

                _appointmentRepo.Update(updatedAppointment);

                MessageService.DisplayMessage(_currentUser.Language, "UpdatedAppointment", MessageBoxIcon.Information);
                UpdateAppointmentTable();
            }
            catch (Exception ex)
            {
                MessageService.DisplayMessage(_currentUser.Language, "FailedToUpdateAppointment", MessageBoxIcon.Error, ex.ToString());
            }
        }

        private void UpdateCustBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(CustTableId.Text.Trim());

                // Get updated customer info and validate the inputs
                var updatedCustomer = GetCustomerFromInput();
                updatedCustomer.Id = customerId;

                if (!_validator.ValidateCustomer(updatedCustomer))
                {
                    MessageService.DisplayMessage(_currentUser.Language, "InvalidInput", MessageBoxIcon.Warning);
                    return;
                }

                // Confirm update with user and attempt to update customer in repository
                if (!MessageService.DisplayYesOrNo(_currentUser.Language, "AreYouSure", MessageBoxIcon.Warning))
                    return;

                _customerRepo.Update(updatedCustomer);

                MessageService.DisplayMessage(_currentUser.Language, "UpdatedCustomer", MessageBoxIcon.Information);
                UpdateCustomerTable();
            }
            catch (Exception ex)
            {
                MessageService.DisplayMessage(_currentUser.Language, "FailedToUpdateCustomer", MessageBoxIcon.Error, ex.ToString());
            }
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
                MessageService.DisplayMessage(_currentUser.Language, "AppointmentIDMissing", MessageBoxIcon.Error, ex.ToString());
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
                MessageService.DisplayMessage(_currentUser.Language, "FailedToDeleteAppointment", MessageBoxIcon.Error, ex.ToString());
            }

            UpdateAppointmentTable();
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
                MessageService.DisplayMessage(_currentUser.Language, "CustomerIDMissing", MessageBoxIcon.Error, ex.ToString());
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
                MessageService.DisplayMessage(_currentUser.Language, "FailedToDeleteCustomer", MessageBoxIcon.Error, ex.ToString());
            }

            UpdateCustomerTable();
        }

        private void GenRptBtn_Click(object sender, EventArgs e)
        {
            new Reports(_currentUser, _appointmentRepo).Show();
        }

        /// <summary>
        /// Helper method to set up the columns for the appointment table and populate them with data from the database
        /// </summary>
        private void UpdateAppointmentTable()
        {
            AppointmentTable.Columns.Clear();

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
                HeaderText = $"Start {_currentUser.Timezone}",
                DataPropertyName = "Start",
                DefaultCellStyle = { Format = "g" }
            });
            AppointmentTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = $"End {_currentUser.Timezone}",
                DataPropertyName = "End",
                DefaultCellStyle = { Format = "g" }
            });

            // Get data from repositories and convert appointment times to local time for display
            List<Appointment> appointmentList;

            if (!SeeAllApts.Checked)
                appointmentList = _appointmentRepo.GetDateRange(monthCalendar1.SelectionStart, monthCalendar1.SelectionEnd);

            else
                appointmentList = _appointmentRepo.GetAll();

            foreach (var apt in appointmentList)
            {
                apt.Start = apt.Start.ToLocalTime();
                apt.End = apt.End.ToLocalTime();
            }

            AppointmentTable.DataSource = appointmentList;
        }

        /// <summary>
        /// Helper method to set up the columns for the customer table and populate them with data from the database
        /// </summary>
        private void UpdateCustomerTable()
        {
            CustomerTable.Columns.Clear();

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

            var customerList = _customerRepo.GetAll();

            CustomerTable.DataSource = customerList;
        }

        /// <summary>
        ///Helper method to combine date and time inputs into a single DateTime object
        /// </summary>
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

        /// <summary>
        /// Populate appointment fields when an appointment is selected from the table
        /// </summary>
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

        /// <summary>
        /// Populate customer fields when a customer is selected from the table
        /// </summary>
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

        /// <summary>
        /// Get appointment object from input fields
        /// </summary>
        private Appointment GetAppointmentFromInput()
        {
            var startUtc = GetDateTime(AptDate.Value, AptStartTime.Value).ToUniversalTime();
            var endUtc = GetDateTime(AptDate.Value, AptEndTime.Value).ToUniversalTime();
            var customerId = Convert.ToInt32(CustIdText.Text);
            var userId = Convert.ToInt32(UserIdText.Text);
            var title = TitleText.Text.Trim();
            var type = TypeText.Text;

            return new Appointment
            {
                CustomerId = customerId,
                UserId = userId,
                Title = title,
                Type = type,
                Start = startUtc,
                End = endUtc
            };
        }

        /// <summary>
        /// Get customer object from input fields
        /// </summary>
        private Customer GetCustomerFromInput()
        {
            var name = NameText.Text.Trim();
            var address = AddressText.Text.Trim();
            var phone = PhoneText.Text.Trim();
            return new Customer
            {
                Name = name,
                Address = address,
                Phone = phone
            };
        }

        /// <summary>
        /// Updates the appointment list with the selected date from the calendar.
        /// </summary>
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            SeeAllApts.Checked = false;
            UpdateAppointmentTable();
        }

        private void SeeAllApts_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAppointmentTable();
        }
    }
}
