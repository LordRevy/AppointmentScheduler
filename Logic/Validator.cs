namespace AppointmentScheduler.Logic;

public class Validator
{
    public bool CheckIfPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        phone = phone.Trim()
                     .Replace("-", "")
                     .Replace(" ", "")
                     .Replace("(", "")
                     .Replace(")", "");

        return phone.All(char.IsDigit) && phone.Length == 10;
    }
    
    public bool CheckIfDateTime(object date)
    {
        if (date is DateTime)
            return true;

        if (date is string s && DateTime.TryParse(s, out _))
            return true;

        return false;
    }

    public bool CheckIfNull(object dataItem)
    {
        return dataItem is null;
    }
}
