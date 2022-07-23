using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApi.Models;

public class User
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }

    [ForeignKey("PersonalInfoId")]
    // public virtual Guid PersonalInfoId { get; set; }
    
    public virtual PersonalInfo PersonalInfo { get; set; }
}