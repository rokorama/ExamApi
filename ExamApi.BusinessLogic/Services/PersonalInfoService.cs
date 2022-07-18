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
            Address = new Address()
            {
                Id = Guid.NewGuid(),
                City = personalInfoDto.Address.City,
                Street = personalInfoDto.Address.Street,
                House = personalInfoDto.Address.House,
                Flat = personalInfoDto.Address.Flat,
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