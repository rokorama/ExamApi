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

    public Address GetInfo(Guid id)
    {
        return _addressRepo.GetInfo(id);
    }
}