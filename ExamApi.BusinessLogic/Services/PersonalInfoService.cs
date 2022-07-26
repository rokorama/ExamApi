using ExamApi.DataAccess;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace ExamApi.BusinessLogic;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly IPersonalInfoRepository _personalInfoRepo;
    private readonly ILogger<PersonalInfoService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly IPropertyChanger _propertyChanger;
    IValidator<PersonalInfoUploadRequest> _personalInfoUploadValidator;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo,
                               ILogger<PersonalInfoService> logger,
                               IObjectMapper mapper,
                               IPropertyChanger propertyChanger,
                               IValidator<PersonalInfoUploadRequest> personalInfoUploadValidator)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
        _mapper = mapper;
        _propertyChanger = propertyChanger;
        _personalInfoUploadValidator = personalInfoUploadValidator;
    }

    public ResponseDto AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId)
    {
        var validationResult = _personalInfoUploadValidator.Validate(uploadRequest);
        if (_personalInfoRepo.UserHasExistingPersonalInfo(userId))
            return new ResponseDto(false, "Cannot submit personal info more than one per user.");
        if (!validationResult.IsValid)
            return new ResponseDto(false, "One or more values are invalid.");

        var mappedEntry = _mapper.MapPersonalInfoUpload(uploadRequest);
        if (!_personalInfoRepo.AddInfo(mappedEntry, userId))
            return new ResponseDto(false, "Failed to add entry to the database.");
        else
            return new ResponseDto(true, null);
    }

    public PersonalInfoDto? GetInfo(Guid userId)
    {
        var entry = _personalInfoRepo.GetInfo(userId);
        if (entry is null)
            return null;
        return _mapper.MapPersonalInfoDto(entry);
    }

    public Guid? GetInfoId(Guid userId)
    {
        var entry = _personalInfoRepo.GetInfoId(userId);
        if (entry is null)
            return null;
        return entry;
    }

    public ResponseDto UpdateInfo<T>(Guid userId, string propertyName, T newValue)
    {
        PersonalInfo? entry = _personalInfoRepo.GetInfo(userId);
        if (entry is null)
        {
            _logger.LogInformation($"Failed attempt to update non-existing personal info for user {userId}.");
            return new ResponseDto(false);
        }
        _propertyChanger.UpdatePersonalInfo<T>(entry, propertyName, newValue);
        return new ResponseDto() { Success = _personalInfoRepo.UpdateInfo(userId, entry) };
    }
}