namespace AppointmentSchedler.Domain
{
  public class User
  {
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Language { get; set; } = "en";
  }
}
