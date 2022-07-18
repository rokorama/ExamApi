namespace ExamApi.Models;

public static class DtoEntityMapper
{
    public static PersonalInfo MapPersonalInfoEntity(PersonalInfoDto personalInfoDto)
    {
        return new PersonalInfo()
        {
            Id = Guid.NewGuid(),
            FirstName = personalInfoDto.FirstName,
            LastName = personalInfoDto.LastName,
            PersonalNumber = personalInfoDto.PersonalNumber,
            Email = personalInfoDto.Email,
            Photo = ImageConverter.ConvertImage(personalInfoDto.ImageUpload),
            ResidenceInfo = MapResidenceInfoEntity(personalInfoDto.ResidenceInfo)
        };
    }

    public static ResidenceInfo MapResidenceInfoEntity(ResidenceInfoDto residenceInfoDto)
    {
        return new ResidenceInfo()
        {
            Id = Guid.NewGuid(),
            City = residenceInfoDto.City,
            Street = residenceInfoDto.Street,
            House = residenceInfoDto.House,
            Flat = residenceInfoDto.Flat,
        };
    }

    public static PersonalInfoDto MapPersonalInfoDto(PersonalInfo personalInfo)
    {
        return new PersonalInfoDto()
        {
            FirstName = personalInfo.FirstName,
            LastName = personalInfo.LastName,
            PersonalNumber = personalInfo.PersonalNumber,
            Email = personalInfo.Email,
            // Photo = personalInfo.Photo,
            ResidenceInfo = MapResidenceInfoDto(personalInfo.ResidenceInfo)
        };
    }

    public static ResidenceInfoDto MapResidenceInfoDto(ResidenceInfo residenceInfo)
    {
        return new ResidenceInfoDto()
        {
            City = residenceInfo.City,
            Street = residenceInfo.Street,
            House = residenceInfo.House,
            Flat = residenceInfo.Flat
        };
    }
}