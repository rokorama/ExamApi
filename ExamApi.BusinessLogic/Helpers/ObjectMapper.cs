using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;

namespace ExamApi.BusinessLogic.Helpers;

public class ObjectMapper : IObjectMapper
{
    private readonly ImageConverter _imageConverter;

    public ObjectMapper(ImageConverter imageConverter)
    {
        _imageConverter = imageConverter;
    }

    public PersonalInfo MapPersonalInfoUpload(PersonalInfoUploadRequest uploadRequest)
    {
        return new PersonalInfo()
        {
            FirstName = uploadRequest.FirstName,
            LastName = uploadRequest.LastName,
            PersonalNumber = uploadRequest.PersonalNumber,
            Email = uploadRequest.Email,
            Photo = _imageConverter.ConvertImage(uploadRequest.ImageUpload!),
            Address = MapAddressEntity(uploadRequest.Address!),
        };
    }

    public Address MapAddressEntity(AddressDto addressDto)
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

    public PersonalInfoDto MapPersonalInfoDto(PersonalInfo personalInfo)
    {
        return new PersonalInfoDto()
        {
            FirstName = personalInfo.FirstName,
            LastName = personalInfo.LastName,
            PersonalNumber = personalInfo.PersonalNumber,
            Email = personalInfo.Email,
            Photo = personalInfo.Photo,
            Address = MapAddressDto(personalInfo.Address!),
        };
    }

    public AddressDto MapAddressDto(Address address)
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