namespace ExamApi.Models;

public class PersonalInfo
{
    public Guid Id { get; set; }
    public string FirstName  { get; set; }
    public string LastName  { get; set; }
    public ulong PersonalNumber { get; set; }
    public string Email { get; set; }
    public byte[] Photo { get; set; }
    public Guid ResidenceInfoGuid { get; set; }
    public virtual ResidenceInfo ResidenceInfo { get; set; }
}