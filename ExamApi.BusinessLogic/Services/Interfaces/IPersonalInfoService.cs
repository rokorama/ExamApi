using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public PersonalInfo AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId);
    public bool GetInfo(Guid userId);
    public bool GetInfo(Guid userId, out PersonalInfoDto entry);
}