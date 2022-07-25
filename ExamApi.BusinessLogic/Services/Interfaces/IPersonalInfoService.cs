using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;

namespace ExamApi.BusinessLogic;

public interface IPersonalInfoService
{
    public ResponseDto AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId);
    public PersonalInfoDto? GetInfo(Guid userId);
    public Guid? GetInfoId(Guid userId);
    public ResponseDto UpdateInfo<T>(Guid userId, string propertyName, T newValue);
}