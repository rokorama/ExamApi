using ExamApi.DataAccess;
using ExamApi.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly IResidenceInfoService _residenceInfoService;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo, IResidenceInfoService residenceInfoService)
    {
        _personalInfoRepo = personalInfoRepo;
        _residenceInfoService = residenceInfoService;
    }

    public PersonalInfo AddInfo(Guid userId, PersonalInfoDto personalInfoDto)
    {
        // if (_personalInfoRepo.GetInfo(userId) != null)
        //     throw new Exception($"There are existing data associated with this user");
        var personalInfo = new PersonalInfo()
        {
            Id = Guid.NewGuid(),
            FirstName = personalInfoDto.FirstName,
            LastName = personalInfoDto.LastName,
            PersonalNumber = personalInfoDto.PersonalNumber,
            Email = personalInfoDto.Email,
            Photo = ImageConverter.ConvertImage(personalInfoDto.ImageUpload),
            ResidenceInfo = new ResidenceInfo()
            {
                Id = Guid.NewGuid(),
                City = personalInfoDto.ResidenceInfo.City,
                Street = personalInfoDto.ResidenceInfo.Street,
                House = personalInfoDto.ResidenceInfo.House,
                Flat = personalInfoDto.ResidenceInfo.Flat,
            }
        };
        if (_personalInfoRepo.AddInfo(personalInfo))
            return personalInfo;
        // handle this
        else
            throw new Exception("The information could not be added, please try again");
    }

    public PersonalInfo GetInfo(Guid userId)
    {
        return _personalInfoRepo.GetInfo(userId);
    }
}