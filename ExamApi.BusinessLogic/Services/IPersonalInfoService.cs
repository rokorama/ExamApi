using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public PersonalInfo AddPersonalInfo(Guid userId, PersonalInfoDto personalInfoDto);
    public PersonalInfo GetPersonalInfo(Guid userId);
}