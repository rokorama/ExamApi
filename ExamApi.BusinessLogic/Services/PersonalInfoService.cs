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
    private readonly IObjectMapper _mapper;
    private readonly IPropertyChanger _propertyChanger;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo,
                               ILogger<PersonalInfoService> logger,
                               IObjectMapper mapper,
                               IPropertyChanger propertyChanger)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
        _mapper = mapper;
        _propertyChanger = propertyChanger;
    }

    public bool AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo? result)
    {
        if (_personalInfoRepo.UserHasExistingPersonalInfo(userId))
        {
            // TODO - works, but returns error for invalid values
            _logger.LogInformation($"User {userId} attempted to add duplicate personal info at {DateTime.Now}");
            result = null;
            return false;
        }
        result = _mapper.MapPersonalInfoUpload(uploadRequest);
        if (!_personalInfoRepo.AddInfo(result, userId))
        {
            result = null;
            return false;

        }
        return true;
    }

    public PersonalInfoDto? GetInfo(Guid userId)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry is null)
            return null;
        return _mapper.MapPersonalInfoDto(entry);
    }

    public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue)
    {
        PersonalInfo? entry = _personalInfoRepo.GetInfo(userId);
        if (entry is null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing personal info for user {userId}.");
            return false;
        }
        _propertyChanger.UpdatePersonalInfo<T>(entry, propertyName, newValue);
        return _personalInfoRepo.UpdateInfo(userId, entry);
    }
}