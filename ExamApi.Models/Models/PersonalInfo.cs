namespace ExamApi.Models;

public class PersonalInfo
{
    public Guid Id { get; set; }
    public string FirstName  { get; set; }
    public string LastName  { get; set; }
    public int PersonalNumber { get; set; }
    public string Email { get; set; }
    public byte[] Photo { get; set; }
    public PlaceOfResidence PlaceOfResidence { get; set; }
}