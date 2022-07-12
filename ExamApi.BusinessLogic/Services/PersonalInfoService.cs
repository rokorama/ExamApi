using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo)
    {
        _personalInfoRepo = personalInfoRepo;
    }

    public PersonalInfo AddPersonalInfo(Guid userId, PersonalInfoDto personalInfoDto)
    {
        if (_personalInfoRepo.GetInfo(userId) != null)
            throw new Exception($"There are existing data associated with this user");
        var personalInfo = new PersonalInfo()
        {
            Id = Guid.NewGuid(),
            FirstName = personalInfoDto.FirstName,
            LastName = personalInfoDto.LastName,
            PersonalNumber = personalInfoDto.PersonalNumber,
            Email = personalInfoDto.Email,
            // !! implement resizing photos
            Photo = ConvertImageUploadToBytes(personalInfoDto.ImageUpload)
        };
        if (_personalInfoRepo.AddInfo(personalInfo))
            return personalInfo;
        // handle this
        else
            throw new Exception("The information could not be added, please try again");
    }

    public PersonalInfo GetPersonalInfo(Guid userId)
    {
        return _personalInfoRepo.GetInfo(userId);
    }

    public byte[] ConvertImageUploadToBytes(ImageUploadRequest imageUploadRequest)
    {
        using var memoryStream = new MemoryStream();
        imageUploadRequest.Image.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}