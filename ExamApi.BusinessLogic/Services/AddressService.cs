using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace ExamApi.BusinessLogic;

public class AddressService : IAddressService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly ILogger<AddressService> _logger;
    private readonly IPropertyChanger _propertyChanger;

    public AddressService(IPersonalInfoRepository personalInfoRepo, ILogger<AddressService> logger, IPropertyChanger propertyChanger)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
        _propertyChanger = propertyChanger;
    }

    public ResponseDto UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        PersonalInfo? entry = _personalInfoRepo.GetInfo(userId);
        if (entry is null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing address for user {userId}.");
            return new ResponseDto(false, "User has no existing data to update. Please submit complete personal information first.");
        }
        _propertyChanger.UpdateAddress<T>(entry.Address!, propertyName, newValue);
        return new ResponseDto() { Success = _personalInfoRepo.UpdateInfo(userId, entry) };
    }

    public Address? GetAddress(Guid userId)
    {
        var result = _personalInfoRepo.GetInfo(userId);
        if (result is null)
            return null;
        return result.Address;
    }
}