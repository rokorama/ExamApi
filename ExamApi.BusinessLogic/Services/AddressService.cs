using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
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

    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        PersonalInfo? entry = _personalInfoRepo.GetInfo(userId);
        if (entry == null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing address for user {userId}.");
            return false;
            //throw error if false
        }
        _propertyChanger.UpdateAddress<T>(entry.Address!, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}