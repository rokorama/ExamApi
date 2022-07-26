using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit;
using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ExamApi.Tests.BusinessLogic;

public class PersonalInfoServiceTests
{
    private readonly PersonalInfoService _sut;
    private readonly IFixture _fixture;
    private readonly Mock<IPersonalInfoRepository> _repoMock = new Mock<IPersonalInfoRepository>();
    private readonly Mock<ILogger<PersonalInfoService>> _loggerMock = new Mock<ILogger<PersonalInfoService>>();
    private readonly Mock<IObjectMapper> _mapperMock = new Mock<IObjectMapper>();
    private readonly Mock<IPropertyChanger> _propertyChangerMock = new Mock<IPropertyChanger>();
    private readonly IValidator<PersonalInfoUploadRequest> _infoUploadValidator;

    
    public PersonalInfoServiceTests()
    {
        _infoUploadValidator = new InlineValidator<PersonalInfoUploadRequest>();
        _sut = new PersonalInfoService(_repoMock.Object,
                                       _loggerMock.Object,
                                       _mapperMock.Object,
                                       _propertyChangerMock.Object,
                                       _infoUploadValidator);
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Fact]
    public void GetInfo_ReturnsDto_WhenUserExists()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid personalInfoId = Guid.NewGuid();
        var personalInfo = _fixture.Create<PersonalInfo>();
        personalInfo.FirstName = "Steve";
        
        _repoMock.Setup(r => r.GetInfo(userId)).Returns(personalInfo);
        _mapperMock.Setup(m => m.MapPersonalInfoDto(personalInfo)).Returns(new PersonalInfoDto() { FirstName = personalInfo.FirstName} );

        // Act
        PersonalInfoDto? mockPersonalInfoDto = _sut.GetInfo(userId);
        
        // Assert
        Assert.Equal(mockPersonalInfoDto!.FirstName, personalInfo.FirstName);
    }

    [Fact]
    public void GetInfo_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetInfo(It.IsAny<Guid>())).Returns(value: null);

        // Act
        PersonalInfoDto? mockPersonalInfoDto = _sut.GetInfo(It.IsAny<Guid>());
        
        // Assert
        Assert.Null(mockPersonalInfoDto);
    }

    [Fact]
    public void GetInfo_ReturnsNull_WhenUserExistsAndInfoIsNull()
    {
        // Arrange
        _repoMock.Setup(r => r.GetInfo(It.IsAny<Guid>())).Returns(value: null);

        // Act
        PersonalInfoDto? mockPersonalInfoDto = _sut.GetInfo(It.IsAny<Guid>());

        //Assert
        Assert.Null(mockPersonalInfoDto);
    }


    [Fact]
    public void GetInfoId_ReturnsId_WhenDataExists()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        var personalInfo = _fixture.Create<PersonalInfo>();
        _repoMock.Setup(r => r.GetInfoId(userId)).Returns(personalInfo.Id);

        // Act
        var returnedId = _sut.GetInfoId(userId);

        // Assert
        Assert.Equal(personalInfo.Id, returnedId);
    }

    [Fact]
    public void GetInfoId_ReturnsNull_WhenInfoDoesNotExist()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        _repoMock.Setup(r => r.GetInfoId(userId)).Returns(value: null);

        // Act
        var returnedId = _sut.GetInfoId(userId);

        // Assert
        Assert.Null(returnedId);
    }

    [Fact]
    public void AddInfo_WorksCorrectly_WithValidInput()
    {
        // Arrange
        Guid userId = _fixture.Create<Guid>();
        var uploadRequest = _fixture.Create<PersonalInfoUploadRequest>();
        uploadRequest.Email = "test@asdasd.asd";

            // set up repo returns
        _repoMock.Setup(r => r.AddInfo(It.IsAny<PersonalInfo>(), userId)).Returns(true);
        _repoMock.Setup(r => r.UserHasExistingPersonalInfo(userId)).Returns(false);
        
        //     // set up image converter returns
        var _imageConverterMock = new Mock<IImageConverter>();
        var imageUploadRequestMock = _fixture.Create<ImageUploadRequest>();
        _imageConverterMock.Setup(i => i.ConvertImage(imageUploadRequestMock)).Returns(It.IsAny<byte[]>());

        var testPersonalInfo = _fixture.Create<PersonalInfo>();
        testPersonalInfo.Email = "test@asdasd.asd";
        _mapperMock.Setup(m => m.MapPersonalInfoUpload(It.IsAny<PersonalInfoUploadRequest>())).Returns(testPersonalInfo);
        
        // Act
        var entrySubmisson = _sut.AddInfo(uploadRequest, userId);
        
        // Assert
        Assert.True(entrySubmisson.Success);
    }
    
    [Fact]
    public void AddInfo_ReturnsFailure_WhenInfoIsInvalid()
    {
        // Arrange
        Guid userId = _fixture.Create<Guid>();
        var uploadRequest = _fixture.Create<PersonalInfoUploadRequest>();
        uploadRequest.Email = null;

            // Override the subject under testing - the validator is not working properly in testing 
            // and has to be set up specifically
        var validatorOverride = new InlineValidator<PersonalInfoUploadRequest>();
        validatorOverride.RuleFor(x => x.Email).Must(id => false); 
        var sutOverride = new PersonalInfoService(_repoMock.Object,
                                       _loggerMock.Object,
                                       _mapperMock.Object,
                                       _propertyChangerMock.Object,
                                       validatorOverride);
        
        // Act
        var entrySubmisson = sutOverride.AddInfo(uploadRequest, userId);
        
        // Assert
        Assert.False(entrySubmisson.Success);
        Assert.Equal("One or more values are invalid.", entrySubmisson.Message);      
    }

    [Fact]
    public void AddInfo_ReturnsFailure_UserHasExistingInfo()
    {
            // Arrange
        Guid userId = _fixture.Create<Guid>();
        var uploadRequest = _fixture.Create<PersonalInfoUploadRequest>();
        uploadRequest.Email = null;

        _repoMock.Setup(x => x.UserHasExistingPersonalInfo(userId)).Returns(true);
        
        // Act
        var entrySubmisson = _sut.AddInfo(uploadRequest, userId);
        
        // Assert
        Assert.False(entrySubmisson.Success);
        Assert.Equal("Cannot submit personal info more than one per user.", entrySubmisson.Message);   
    }


    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with valid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with invalid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with no existing previous info
}