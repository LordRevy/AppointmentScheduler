namespace AppointmentScheduler.Domain
{
    public class User
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string Language { get; set; }
        public string? Timezone { get; set; } = null;
    }
}
