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
    private readonly IPropertyChanger _propertyChanger;
    private readonly IValidator<Address> _addressValidator;

    public AddressService(IAddressRepository addressRepo,
                          ILogger<AddressService> logger,
                          IPropertyChanger propertyChanger,
                          IValidator<Address> addressValidator)
    {
        _addressRepo = addressRepo;
        _logger = logger;
        _propertyChanger = propertyChanger;
        _addressValidator = addressValidator;
    }

    public ResponseDto UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        Address? entry = _addressRepo.GetAddress(userId);
        if (entry is null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing address for user {userId}.");
            return new ResponseDto(false, "User has no existing data to update. Please submit complete personal information first.");
        }
        _propertyChanger.UpdateAddress<T>(entry, propertyName, newValue);
        return new ResponseDto() { Success = _addressRepo.UpdateAddress(userId, entry) };
    }

    public Address? GetAddress(Guid userId)
    {
        var result = _addressRepo.GetAddress(userId);
        if (result is null)
            return null;
        return result;
    }
}