using ExamApi.Models;
using Microsoft.AspNetCore.Http;

namespace ExamApi.DataAccess;

public static class ObjectMapper
{
    public static PersonalInfo MapPersonalInfoUpload(PersonalInfoUploadRequest uploadRequest)
    {
        return new PersonalInfo()
        {
            // Id = Guid.NewGuid(),
            FirstName = uploadRequest.FirstName,
            LastName = uploadRequest.LastName,
            PersonalNumber = uploadRequest.PersonalNumber,
            Email = uploadRequest.Email,
            Photo = ImageConverter.ConvertImage(uploadRequest.ImageUpload),
            Address = MapAddressEntity(uploadRequest.Address),
        };
    }

    // public static PersonalInfo MapPersonalInfoEntity(PersonalInfoDto personalInfoDto)
    // {
    //     return new PersonalInfo()
    //     {
    //         Id = Guid.NewGuid(),
    //         FirstName = personalInfoDto.FirstName,
    //         LastName = personalInfoDto.LastName,
    //         PersonalNumber = personalInfoDto.PersonalNumber,
    //         Email = personalInfoDto.Email,
    //         Photo = personalInfoDto.Photo,
    //         Address = MapAddressEntity(personalInfoDto.Address)
    //     };
    // }

    public static Address MapAddressEntity(AddressDto addressDto)
    {
        return new Address()
        {
            // Id = Guid.NewGuid(),
            City = addressDto.City,
            Street = addressDto.Street,
            House = addressDto.House,
            Flat = addressDto.Flat,
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
            Photo = personalInfo.Photo,
            Address = MapAddressDto(personalInfo.Address),
        };
    }

    public static AddressDto MapAddressDto(Address address)
    {
        return new AddressDto()
        {
            City = address.City,
            Street = address.Street,
            House = address.House,
            Flat = address.Flat
        };
    }
}