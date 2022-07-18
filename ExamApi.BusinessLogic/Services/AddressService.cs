using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepo;

    public AddressService(IAddressRepository addressRepo)
    {
        _addressRepo = addressRepo;
    }

    public Address AddInfo(AddressDto addressDto)
    {
        var entry = new Address()
        {
            Id = Guid.NewGuid(),
            City = addressDto.City,
            Street = addressDto.Street,
            House = addressDto.House,
            Flat = addressDto.Flat
        };
        _addressRepo.AddInfo(entry);
        return entry;
    }

    public void DeleteInfo(Guid id)
    {
        _addressRepo.DeleteInfo(id);
    }

    public bool GetInfo(Guid id)
    {
        var result = _addressRepo.GetInfo(id);
        if (result == null)
            return false;
        return true;
    }

    public bool GetInfo(Guid id, out AddressDto result)
    {
        var entry = _addressRepo.GetInfo(id);
        if (entry == null)
        {
            result = null;
            return false;
        }
        result = ObjectMapper.MapAddressDto(entry);
        return true;
    }
}