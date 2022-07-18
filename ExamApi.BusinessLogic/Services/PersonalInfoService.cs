using ExamApi.DataAccess;
using ExamApi.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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

    public PersonalInfo AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId)
    {
        // if (_personalInfoRepo.GetInfo(userId) != null)
        //     throw new Exception($"There are existing data associated with this user");
        var personalInfo = ObjectMapper.MapPersonalInfoUpload(uploadRequest);
        if (_personalInfoRepo.AddInfo(personalInfo, userId))
            return personalInfo;
        // handle this
        else
            throw new Exception("The information could not be added, please try again");
    }

    public bool GetInfo(Guid userId)
    {
        var result = _personalInfoRepo.GetInfo(userId);
        if (result == null)
            return false;
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
}