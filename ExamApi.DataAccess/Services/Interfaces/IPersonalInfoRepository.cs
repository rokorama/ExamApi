using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IPersonalInfoRepository
{
    public PersonalInfo? GetInfo(Guid userId);
    public bool AddInfo(PersonalInfo personalInfo, Guid userId);
    public bool EditInfo(Guid userId, User user);
    public bool UpdateInfo(Guid userId, PersonalInfo entryToUpdate);
    public bool CheckForExistingPersonalInfo(Guid userId);
    public bool CheckForExistingAddress(Guid userId);
}