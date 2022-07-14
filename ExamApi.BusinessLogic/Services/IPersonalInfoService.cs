using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public PersonalInfo AddInfo(Guid userId, PersonalInfoDto personalInfoDto);
    public PersonalInfo GetInfo(Guid userId);
}