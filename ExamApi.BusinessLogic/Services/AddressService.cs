using ExamApi.DataAccess;
using Microsoft.Extensions.Logging;

namespace ExamApi.BusinessLogic;

public class AddressService : IAddressService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly ILogger<AddressService> _logger;

    public AddressService(IPersonalInfoRepository personalInfoRepo, ILogger<AddressService> logger)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
    }
    
    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry.Address == null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing address for user {userId}.");
            return false;
        }
        PropertyChanger.UpdateAddress<T>(entry.Address, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}