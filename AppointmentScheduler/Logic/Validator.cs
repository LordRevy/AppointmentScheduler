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
        public bool ValidateAppointment(Appointment apt)
        {
            if (apt.Start >= apt.End)
                return false;
            
            DayOfWeek day = apt.Start.DayOfWeek;
            TimeSpan startTime = apt.Start.TimeOfDay;
            TimeSpan endTime = apt.End.TimeOfDay;

            TimeSpan officeOpen = new (9, 0, 0);
            TimeSpan officeClose = new (17, 0, 0);

            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday 
                || startTime < officeOpen || endTime > officeClose)
                return false;

            var isOverlapping = _appointmentRepo.CheckAppointmentOverlap(apt);
            return !isOverlapping;
        }

    }
}
