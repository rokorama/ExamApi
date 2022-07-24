using ExamApi.Models.DTOs;

namespace ExamApi.Models.UploadRequests;

public class PersonalInfoUploadRequest
{
    public string? FirstName  { get; set; }
    public string? LastName  { get; set; }
    public ulong? PersonalNumber { get; set; }
    public string? Email { get; set; }
    public ImageUploadRequest? ImageUpload { get; set; }
    public AddressDto? Address { get; set; }
}