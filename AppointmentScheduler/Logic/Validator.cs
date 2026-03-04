using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Logic
{
    public class Validator
    {
        private AppointmentRepository _appointmentRepo;

        public Validator(AppointmentRepository appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        /// <summary>
        /// Formats Customer inputs and checks entries to ensure they are valid.
        /// </summary>
        public bool ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Address) || string.IsNullOrWhiteSpace(customer.Phone))
                return false;

            foreach (char number in customer.Phone)
            {
                if (!char.IsDigit(number) && number != '-')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Ensures the start time and end time of an appointment are within business hours.
        /// Checks to see if the appointment day is between mon-fri.
        /// Verifies that there are no overlapping appointments in the Database.
        /// </summary>
        public bool ValidateAppointment(Appointment apt)
        {
            if (apt.Start >= apt.End)
                return false;

            // Converting to Eastern Standard Time to check office hours
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var startEastern = TimeZoneInfo.ConvertTimeFromUtc(apt.Start, easternZone);
            var endEastern = TimeZoneInfo.ConvertTimeFromUtc(apt.End, easternZone);

            DayOfWeek day = startEastern.DayOfWeek;
            TimeSpan startTime = startEastern.TimeOfDay;
            TimeSpan endTime = endEastern.TimeOfDay;

            TimeSpan officeOpen = new(9, 0, 0);
            TimeSpan officeClose = new(17, 0, 0);

            if (day == DayOfWeek.Saturday ||
                day == DayOfWeek.Sunday ||
                startTime < officeOpen ||
                endTime > officeClose)
                return false;

            return true;
        }
    }
}
