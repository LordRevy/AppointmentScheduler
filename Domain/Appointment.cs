public class Appointment {
  public int AppointmentID { get; set; }
  public int UserID { get; set; }
  public int CustomerID { get; set; }
  public string AppointmentType { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
}
