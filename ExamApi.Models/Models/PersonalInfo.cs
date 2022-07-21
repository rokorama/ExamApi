using System.ComponentModel.DataAnnotations;

namespace ExamApi.Models;

public class PersonalInfo
{
    [Required]
    public Guid Id { get; set; }
    public string FirstName  { get; set; }
    public string LastName  { get; set; }
    public ulong PersonalNumber { get; set; }
    public string Email { get; set; }
    public byte[] Photo { get; set; }
    #nullable enable
    public virtual Address? Address { get; set; }
}