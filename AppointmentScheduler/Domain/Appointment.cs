namespace AppointmentScheduler.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
