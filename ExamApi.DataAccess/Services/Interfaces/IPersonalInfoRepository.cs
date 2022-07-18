using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IPersonalInfoRepository
{
    public PersonalInfo GetInfo(Guid userId);
    public bool AddInfo(PersonalInfo personalInfo);
    public bool EditInfo(Guid userId, User user);
    public bool DeleteInfo(User user);
}