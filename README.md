# AppointmentScheduler
Work in progress, using this to keep track of what I have done so far.

# Project Structure

**Domain**
- [x] Appointment.cs
- [x] Customer.cs
- [x] User.cs
- [x] Address.cs


**Data**
- [x] Database.cs
  * GetConnection
- [x] UserRepository.cs
  * GetUser
- [x] CustomerRepository.cs
  * GetCustomer
  * AddCustomer
  * UpdateCustomer
  * DeleteCustomer
- [x] AppointmentRepository.cs
  * GetAppointmentsByUser
  * GetAppointmentsByDay
  * AddAppointmnt
  * UpdateAppointment
  * DeleteAppointment
- [x] AddressRepository.cs
  * GetAddress


**Logic**
- [x] Authentication.cs
  * ValidateCredentials
- [x] LoginService.cs
  * LogAttempt
  * CheckAlerts
- [x] MessageService.cs
  * GetMessage
- [x] Location.cs
  * FindUser
- [ ] Calendar.cs
- [ ] CustomerService.cs
- [ ] AppointmentService.cs
- [ ] Validator.cs
  * CheckIfPhone
  * CheckIfDateTime
  * CheckIfNull
- [ ] Reports.cs
  * GetAppointmentTypes
  * GetUserSchedule
  * One Additional

**Forms**
- [ ] Login Page
  * Username and Password in a try/catch with messages
  * Call ValidateCredentials
  * Call LocateUser
- [ ] Main Page
  * Calendar View
  * Button for Customer CRUD
  * Button for Appointment CRUD
  * Button to generate Appointments Report
  * Button to generate User Schedule Report
  * Button for one additional Report
