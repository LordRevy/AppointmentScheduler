using AppointmentScheduler.Data;
using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Logic;

public class AddressService
{
    private readonly AddressRepository _addressRepository;
    private readonly Validator _validator;
    
    public AddressService(AddressRepository addressRepository, Validator validator)
    {
        _addressRepository = addressRepository;
        _validator = validator;
    }
    
    public bool Create(User user, string address, string phone)
    {
    
        if (_validator.IsEmpty(address, phone))
            return false;
        
        var createDate = DateTime.Now;
        var addressObject = new Address
        {
            Address = address,
            Phone = phone,
            CreateDate = createDate,
            CreatedBy = user.UserId,
            LastUpdate = createDate,
            LastUpdateBy = user.UserId
        };
    
        _addressRepository.AddAddress(addressObject);
    
        return true;
    }

    public int GetAddressId(string address, string phone)
    {
        return _addressRepository.GetAddressId(address, phone);
    }

    }
    
