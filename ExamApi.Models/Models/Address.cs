using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApi.Models;

public class Address
{
    [ForeignKey("PersonalInfo")]
    public Guid Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public int? Flat { get; set; }
    public virtual PersonalInfo PersonalInfo { get; set; }
}