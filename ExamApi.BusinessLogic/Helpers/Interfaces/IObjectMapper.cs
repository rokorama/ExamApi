using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;

namespace ExamApi.BusinessLogic.Helpers;

public interface IObjectMapper
{
    public PersonalInfo MapPersonalInfoUpload(PersonalInfoUploadRequest uploadRequest);
    public Address MapAddressEntity(AddressDto addressDto);
    public PersonalInfoDto MapPersonalInfoDto(PersonalInfo personalInfo);
    public AddressDto MapAddressDto(Address address);
}