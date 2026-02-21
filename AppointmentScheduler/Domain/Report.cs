namespace AppointmentScheduler.Domain
{
    public class AppointTypeByMonthReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public required string Type { get; set; }
        public int Count { get; set; }
    }

    public class ScheduleReport
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Other";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
