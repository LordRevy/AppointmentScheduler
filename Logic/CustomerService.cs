using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Logic;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository = new CustomerRepository();
    private readonly Validator _validator = new Validator();
    
    public bool Create(User user, string customerName, string address, string phone)
    {
        customerName = customerName?.Trim();
        address = address?.Trim();
        phone = phone?.Trim();
    
        if (customerName == null || address == null || phone == null)
            throw new ArgumentException("Please do not leave any field empty.");

        addressService.AddAddress(address, phone);
        addressId = addressService.GetAddressId(address);

        if (addressId == null)
            return false;

        var createDate = DateTime.Now();
        
        var customer = new Customer
        {
            CustomerId = ,
            CustomerName = customerName,
            AddressId = addressId,
            Active = true,
            CreateDate = createDate,
            CreatedBy = user.UserId,
            LastUpdate = createDate,
            LastUpdateBy = user.UserId
        };
    
        _customerRepository.AddCustomer(customer);
    
        return true;
    }
}
