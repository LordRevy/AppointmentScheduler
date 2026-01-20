# AppointmentScheduler
Work in progress, using this to keep track of what I have done so far.

# Project Structure

**Domain**

- [x] User (UserId, Username)
- [x] Customer (CustomerId, Name, Address, Phone)
- [x] Appointment (AppointmentId, CustomerId, UserId, Title, Description, Location, Contact, Type, Url, Start, End)

**Data**

- [x] Database (GetConnection)
- [ ] UserRepository (GetByUsername, GetAll)
- [x] CustomerRepository (Add, Update, Delete, GetAll, GetById)
- [ ] AppointmentRepository (Add, Update, Delete, GetById, GetForDayRangeUtc, GetForUserBetweenUtc, GetAll)

**Application**

- [ ] AuthService
- ValidateCredentials(u, p) (must be "test" / "test")
- LogLogin(username) (append to Login_History.txt)
- (Optionally) returns the User object

- [ ] TimeService
- ToUtc(local) and ToLocal(utc)
- ValidateBusinessHoursEastern(startLocal, endLocal)

- [ ] AppointmentService
- Add/Update/Delete (enforce business hours, overlap)
- GetAppointmentsForDay(localDate) (for calendar day view)
- GetUpcomingForUserWithinMinutes(userId, minutes) (for 15-min alert)

- [ ] CustomerService
- Add/Update/Delete w/ validation rules

- [ ] ReportService
- GetAppointmentTypesByMonth() (lambda)
- GetUserSchedule() (lambda)
- GetAppointmentsPerDayOfWeek() (lambda)


- [ ] Validation (static helpers for fields, phone rule)
- Localization via Strings.resx + Strings.es.resx
- (Drop MessageService; use resource strings directly.)

**UI**

- [ ] LoginForm (depends on AuthService, AppointmentService, Localization)
- [ ] MainForm (depends on services; hosts tabs/buttons)
- [ ] CustomerForm (create/update, depends on CustomerService)
- [ ] AppointmentForm (create/update, depends on AppointmentService, CustomerService)
