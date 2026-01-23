# AppointmentScheduler
Work in progress, using this to keep track of what I have done so far.

# Project Structure

**Domain**

- [x] User (UserId, Username, Culture, Region, Timezone)
- [x] Customer (CustomerId, Name, Address, Phone)
- [x] Appointment (AppointmentId, CustomerId, UserId, Title, Description, Location, Contact, Type, Url, Start, End)

**Data**

- [x] Database (GetConnection, ExecuteNonQuery, GetLastId)
- [x] UserRepository (MapUser, GetUser, GetAll)
- [x] CustomerRepository (MapCustomer, GetById, GetAll, Add, Update, Delete)
- [x] AppointmentRepository (MapAppointment, GetById, Add, Update, Delete, GetDateRange, GetEarliest)

**Logic**

- [x] Authentication
- ValidateCredentials(u, p) (must be "test" / "test")
- LogLogin(username) (append to Login_History.txt)

- [ ] MessageService
- List of message dictionaries based on language.
- GetMessage(language, message)

- [ ] Validator
- Format(params [])
- Check(params [])

**UI**

- [ ] LoginForm (depends on AuthService, AppointmentService, Localization)
- [ ] MainForm (depends on services; hosts tabs/buttons)
- [ ] CustomerForm (create/update, depends on CustomerService)
- [ ] AppointmentForm (create/update, depends on AppointmentService, CustomerService)
