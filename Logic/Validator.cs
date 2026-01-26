namespace AppointmentScheduler.Logic;

public class Validator
{
    public string Format(string input)
    {
        return input?.Trim() ?? string.Empty;
    }

    public bool ValidateCustomer(string name, string address, string phone)
    {
        name = Format(name);
        address = Format(address);
        phone = Format(phone);

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
            return false;

        foreach (char number in phone)
        {
            if (!char.IsDigit(number) && number != '-')
                return false;
        }
        return true;
    }

    // check overlapping appointments, times are between 9 to 5 mon-fri
    public bool ValidateAppointment(
}
