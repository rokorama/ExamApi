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
    IValidator<PersonalInfo> _personalInfoValidator;

    public PersonalInfoService(IPersonalInfoRepository personalInfoRepo,
                               ILogger<PersonalInfoService> logger,
                               IObjectMapper mapper,
                               IPropertyChanger propertyChanger,
                               IValidator<PersonalInfoUploadRequest> personalInfoUploadValidator,
                               IValidator<PersonalInfo> personalInfoValidator)
    {
        _personalInfoRepo = personalInfoRepo;
        _logger = logger;
        _mapper = mapper;
        _propertyChanger = propertyChanger;
        _personalInfoUploadValidator = personalInfoUploadValidator;
        _personalInfoValidator = personalInfoValidator;
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
            return new ResponseDto(true, "Info added successfully.");
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
            return new ResponseDto(false, "No data to update. Please submit the full personal info form first.");
        }
        
        try
        { var updatedEntry = _propertyChanger.UpdateProperty<T>(entry, propertyName, newValue); }
        catch (ArgumentException)
        { return new ResponseDto(false, "Invalid property"); }
        
        if (_personalInfoValidator.Validate(entry).IsValid is false)
            return new ResponseDto(false, $"Cannot update {propertyName} to an invalid value.");
    
        var change = _personalInfoRepo.UpdateInfo(userId, entry);

        return new ResponseDto(true);
    }
}