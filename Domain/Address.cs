namespace AppointmentScheduler.Domain;

public class Address
{
    public int AddressId { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; } // Null if there is no second aaddress
    public int CityId { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastUpdate { get; set; }
    public string LastUpdateBy { get; set; }
}
