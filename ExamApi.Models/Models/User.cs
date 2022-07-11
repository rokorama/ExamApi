namespace ExamApi.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    [ForeignKey("PersonalInfo")]
    public Guid PersonalInfoId { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
}