using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ExamApi.BusinessLogic;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepo;
    private readonly ILogger<AddressService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly IPropertyChanger _propertyChanger;
    private readonly IValidator<Address> _addressValidator;

    public AddressService(IAddressRepository addressRepo,
                          ILogger<AddressService> logger,
                          IObjectMapper mapper,
                          IPropertyChanger propertyChanger,
                          IValidator<Address> addressValidator)
    {
        _addressRepo = addressRepo;
        _logger = logger;
        _mapper = mapper;
        _propertyChanger = propertyChanger;
        _addressValidator = addressValidator;
    }

    public ResponseDto UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        Address? entry = _addressRepo.GetAddress(userId);
        if (entry is null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing address for user {userId}.");
            return new ResponseDto(false, "User has no existing address to update. Please submit complete personal information first.");
        }

        try 
        { var updatedEntry = _propertyChanger.UpdateProperty<T>(entry, propertyName, newValue); }
        catch (ArgumentException)
        { return new ResponseDto(false, "Invalid property"); }

        var change = _addressRepo.UpdateAddress(userId, entry);
        if (change is false)
            return new ResponseDto(false, $"Cannot update {propertyName} to an invalid value.");
        return new ResponseDto(true);
    }

    public AddressDto? GetAddress(Guid userId)
    {
        var result = _addressRepo.GetAddress(userId);
        if (result is null)
            return null;
        return _mapper.MapAddressDto(result);
    }

    public Guid? GetAddressId(Guid userId)
    {
        var entry = _addressRepo.GetAddressId(userId);
        if (entry is null)
            return null;
        return entry;
    }
}