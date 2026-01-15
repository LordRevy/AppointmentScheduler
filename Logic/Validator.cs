namespace AppointmentScheduler.Logic;

public class Validator
{
    public bool CheckIfEmpty(params string[] items)
    {
        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item))
                return true;
        }
    return false;
    }
}
