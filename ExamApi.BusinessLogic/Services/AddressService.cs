using ExamApi.DataAccess;

namespace ExamApi.BusinessLogic;

public class AddressService : IAddressService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;

    public AddressService(IPersonalInfoRepository personalInfoRepo)
    {
        _personalInfoRepo = personalInfoRepo;
    }
    
    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        PropertyChanger.UpdateAddress<T>(entry.Address, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}