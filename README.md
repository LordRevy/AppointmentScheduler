# AppointmentScheduler
Work in progress, using this to keep track of what I have done so far.

# Project Structure
Domain
- [x] Appointment.cs
- [x] Customer.cs
- [x] User.cs
- [x] Address.cs

Data
- [x] Repository.cs
- [ ] UserRepository.cs
  * GetUser
  * ValidateCredentials
- [ ] CustomerRepository.cs
  * GetCustomer
  * AddCustomer
  * UpdateCustomer
  * DeleteCustomer
  * GetAllCustomers
- [ ] AppointmentRepository.cs
  * GetAppointmentsByUser
  * GetAppointmentsByDay
  * UpdateAppointment
  * DeleteAppointment
  * GetOverlappingAppointments
- [ ] AddressRepository.cs
  * GetAddress

Logic
- [ ] Authentication.cs
- [ ] LoginRecord.cs
- [ ] Messages.cs
- [ ] LocalizationService.cs
- [ ] LoggingService.cs
- [ ] CustomerService.cs
- [ ] AppointmentService.cs
- [ ] TimeZoneService.cs
- [ ] AlertService.cs
- [ ] ReportService.cs
- [ ] ValidationHelper.cs
