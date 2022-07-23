using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public bool AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo createdEntry);
    public bool GetInfo(Guid userId, out PersonalInfoDto entry);
    public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
}