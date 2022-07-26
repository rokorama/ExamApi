using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IPersonalInfoRepository
{
    public PersonalInfo? GetInfo(Guid userId);
    public Guid? GetInfoId(Guid userId);
    public bool AddInfo(PersonalInfo personalInfo, Guid userId);
    public bool UpdateInfo(Guid userId, PersonalInfo entryToUpdate);
    public bool UserHasExistingPersonalInfo(Guid userId);
    public bool UserHasExistingAddress(Guid userId);
}