using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public bool AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo createdEntry);
    public bool GetInfo(Guid userId, out PersonalInfoDto entry);
    public bool UpdatePersonalInfo<T>(Guid userId, string propertyName, T newValue);
    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue);
}