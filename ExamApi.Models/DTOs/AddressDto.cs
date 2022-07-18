namespace ExamApi.Models;

public class AddressDto
{
    public string City { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public int? Flat { get; set; }
}