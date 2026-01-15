using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Logic;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;
    private readonly AddressService _addressService;
    private readonly Validator _validator;
    
    public CustomerService(CustomerRepository customerRepository, AddressService addressService, Validator validator)
    {
        _customerRepository = customerRepository;
        _addressService = addressService;
        _validator = validator;
    }
    
    public bool Create(User user, string customerName, string address, string phone)
    {
    
        if (_validator.CheckIfEmpty(customerName, address, phone))
            throw new ArgumentException("Please do not leave any field empty.");

        addressWasSuccessful = _addressService.AddAddress(address, phone);
        if !(addressWasSuccessful)
            return false;
        
        var addressId = _addressService.GetAddressId(address);
        var createDate = DateTime.Now;
        var customer = new Customer
        {
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

    public Customer? GetCustomer(string customerId)
    {
        return _customerRepository.GetCustomer(customerId);
    }

    public bool UpdateCustomer(User user, Customer customer, string customerName, int addressId, bool active)
    {   
        customer.CustomerName = customerName;
        customer.AddressId = addressId;
        customer.Active = active;
        customer.LastUpdate = DateTime.Now;
        customer.LastUpdateBy = user.UserId;
    
        _customerRepository.UpdateCustomer(customer);
    
        return true;
    }

    public bool DeleteCustomer(Customer customer)
    {
        try
        {
            _customerRepository.DeleteCustomer(customer);
        }
        catch (Exception)
        {
            return false;
        }
        
        return true;
    }
}
