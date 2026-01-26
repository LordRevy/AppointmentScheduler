namespace AppointmentScheduler.Logic;

public class Validator
{
/// <summary>
/// Formats Customer inputs and checks entries to ensure they are valid.
/// </summary>
    public bool ValidateCustomer(string name, string address, string phone)
    {
        name = name.Trim();
        address = address.Trim();
        phone = phone.Trim();

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
        date = start.DayOfWeek();
        startTime = start.TimeOfDay();
        endTime = end.TimeOfDay();
        var officeOpen = 09:00:00;
        var officeClose = 17:00:00;

        if (date == "Saturday" || date == "Sunday")
            return false;

        if (startTime < officeOpen || endTime > officeClose)
            return false;

        return appointmentRepo.CheckAppointmentOverlap(start, end);
    }
}
