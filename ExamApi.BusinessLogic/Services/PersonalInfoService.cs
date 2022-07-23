using ExamApi.DataAccess;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using Microsoft.Extensions.Logging;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly ILogger<PersonalInfoService> _logger;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo,
                               ILogger<PersonalInfoService> logger)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
    }

    public bool AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo result)
    {
        if (_personalInfoRepo.CheckForExistingPersonalInfo(userId))
        {
            // TODO - works, but returns error for invalid values
            _logger.LogInformation($"User {userId} attempted to add duplicate personal info at {DateTime.Now}");
            result = null;
            return false;
        }
        result = ObjectMapper.MapPersonalInfoUpload(uploadRequest);
        if (!_personalInfoRepo.AddInfo(result, userId))
        {
            result = null;
            return false;

        }
        return true;
    }

    public bool GetInfo(Guid userId, out PersonalInfoDto result)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry == null)
        {
            result = null;
            return false;
        }
        result = ObjectMapper.MapPersonalInfoDto(entry);
        return true;
    }

    public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry.Address == null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing personal info for user {userId}.");
            return false;
        }
        PropertyChanger.UpdatePersonalInfo<T>(entry, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}