using ExamApi.DataAccess;
using ExamApi.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo)
    {
        _personalInfoRepo = personalInfoRepo;
    }

    public PersonalInfo AddInfo(Guid userId, PersonalInfoDto personalInfoDto)
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
            Photo = ConvertImage(personalInfoDto.ImageUpload),
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

    public byte[] ConvertImage(ImageUploadRequest imageUploadRequest)
    {
        using var memoryStream = new MemoryStream();
        imageUploadRequest.Image.CopyTo(memoryStream);
        return ResizeImage(memoryStream.ToArray());
    }

    private byte[] ResizeImage(byte[] imageBytes)
    {
        using (Image image = Image.Load(imageBytes))
        {
            image.Mutate(x => x.Resize(200, 200));
            var outStream = new MemoryStream();
            image.Save(outStream, new JpegEncoder());
            return outStream.ToArray();
        }
    }
}