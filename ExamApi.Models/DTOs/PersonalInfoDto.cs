namespace ExamApi.Models.DTOs;

public class PersonalInfoDto
{
    public string FirstName  { get; set; }
    public string LastName  { get; set; }
    public ulong PersonalNumber { get; set; }
    public string Email { get; set; }
    public byte[] Photo { get; set; }
    public AddressDto Address { get; set; }
}