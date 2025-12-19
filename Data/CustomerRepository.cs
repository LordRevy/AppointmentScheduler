

namespace AppointmentScheduler.Data
{
    public class CustomerRepository : RepositoryBase
    {
        public Customer? GetCustomer(string name)
        {
            string sql = "SELECT * FROM customer WHERE customerName = ?";
            return GetRow(sql, reader => new Customer
            {
                CustomerId = reader.GetInt32(0),
                CustomerName = reader.GetString(1),
                AddressId = reader.GetInt32(2),
                Active = reader.GetByte(3) == 1,
                CreateDate = reader.GetDateTime(4),
                CreatedBy = reader.GetString(5),
                LastUpdate = reader.GetDateTime(6),
                LastUpdateBy = reader.GetString(7)
            }, name);
        }
    }
}
