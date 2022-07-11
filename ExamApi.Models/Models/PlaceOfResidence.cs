namespace ExamApi.Models;

public class PlaceOfResidence
{
    public Guid Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public int? Flat { get; set; }
}