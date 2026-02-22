using AppointmentScheduler.Data;

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
        public bool ValidateCustomer(string name, string address, string phone)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
                return false;

            foreach (char number in phone)
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
        public bool ValidateAppointment(DateTime start, DateTime end)
        {
            DayOfWeek day = start.DayOfWeek;
            TimeSpan startTime = start.TimeOfDay;
            TimeSpan endTime = end.TimeOfDay;

            TimeSpan officeOpen = new TimeSpan(9, 0, 0);
            TimeSpan officeClose = new TimeSpan(17, 0, 0);

            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                return false;

            if (startTime < officeOpen || endTime > officeClose)
                return false;

            return !_appointmentRepo.CheckAppointmentOverlap(start, end);
        }

    }
}
