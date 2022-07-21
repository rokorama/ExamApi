using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly IAddressService _addressService;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo, IAddressService addressService)
    {
        _personalInfoRepo = personalInfoRepo;
        _addressService = addressService;
    }

    public bool AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo result)
    {
        if (!_personalInfoRepo.CheckForExistingInfo(userId))
            throw new Exception($"There are existing data associated with this user");
        result = ObjectMapper.MapPersonalInfoUpload(uploadRequest);
        if (!_personalInfoRepo.AddInfo(result, userId))
        {
            result = null;
            return false;

        }
        return true;
    }

    public bool GetInfo(Guid userId, out PersonalInfoDto result)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry == null)
        {
            result = null;
            return false;
        }
        result = ObjectMapper.MapPersonalInfoDto(entry);
        return true;
    }

    public bool UpdatePersonalInfo<T>(Guid userId, string propertyName, T newValue)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        PropertyChanger.UpdatePersonalInfo<T>(entry, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }

    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        PropertyChanger.UpdateAddress<T>(entry.Address, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}