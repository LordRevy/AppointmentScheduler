namespace AppointmentScheduler.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Language { get; set; }
        public required string Country { get; set; }
        public required string Timezone { get; set; }
    }
}
