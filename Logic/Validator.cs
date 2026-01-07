namespace AppointmentScheduler.Logic;

public class Validator
{
    public bool CheckIfPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;
    
        return phone.All(c => char.IsDigit(c) || c == '-');
    }
    
    public bool CheckIfDateTime(object date)
    {
        if (date is DateTime)
            return true;

        if (date is string s && DateTime.TryParse(s, out _))
            return true;

        return false;
    }

    public bool IsNull(object dataItem)
    {
        return dataItem is null;
    }
}
