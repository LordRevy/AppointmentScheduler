namespace AppointmentScheduler.Domain;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
}
