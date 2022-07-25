using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit;
using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
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

    
    public PersonalInfoServiceTests()
    {
        _sut = new PersonalInfoService(_repoMock.Object,
                                       _loggerMock.Object,
                                       _mapperMock.Object,
                                       _propertyChangerMock.Object);
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Fact]
    public void GetInfo_ReturnsDto_WhenUserExists()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid personalInfoId = Guid.NewGuid();
        string expectedFirstName = "Steve";
        // ???
        var personalInfo = new PersonalInfo()
        {
            Id = personalInfoId,
            FirstName = expectedFirstName,
            LastName = It.IsAny<String>(),  
            PersonalNumber = It.IsAny<ulong>(),
            Email = It.IsAny<String>(),
            Photo = It.IsAny<byte[]>(),
            Address = new Address()
        };
        _repoMock.Setup(r => r.GetInfo(userId)).Returns(personalInfo);
        _mapperMock.Setup(m => m.MapPersonalInfoDto(personalInfo)).Returns(new PersonalInfoDto() { FirstName = expectedFirstName} );

        // Act
        PersonalInfoDto? mockPersonalInfoDto = _sut.GetInfo(userId);
        
        // Assert
        Assert.Equal(mockPersonalInfoDto!.FirstName, expectedFirstName);
    }

    [Fact]
    public void GetInfo_ReturnsNull_WhenUserIsNonexistent()
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

    [Fact, AutoData]
    public void AddInfo_WorksCorrectly_WithValidInput()
    {
        // Arrange
        Guid userId = _fixture.Create<Guid>();
        var uploadRequest = _fixture.Create<PersonalInfoUploadRequest>();
        uploadRequest.Email = "test@asdasd.asd";

            // set up repo returns
        _repoMock.Setup(r => r.AddInfo(It.IsAny<PersonalInfo>(), userId)).Returns(true);
        _repoMock.Setup(r => r.UserHasExistingPersonalInfo(userId)).Returns(false);
        
            // set up image converter returns
        var _imageConverterMock = new Mock<IImageConverter>();
        var imageUploadRequestMock = _fixture.Create<ImageUploadRequest>();
        _imageConverterMock.Setup(i => i.ConvertImage(imageUploadRequestMock)).Returns(It.IsAny<byte[]>());

        var testPersonalInfo = _fixture.Create<PersonalInfo>();
        testPersonalInfo.Email = "test@asdasd.asd";
        _mapperMock.Setup(m => m.MapPersonalInfoUpload(It.IsAny<PersonalInfoUploadRequest>())).Returns(testPersonalInfo);
        
        // Act
        _sut.AddInfo(uploadRequest, userId, out PersonalInfo? result);
        
        // Assert
        Assert.Equal(uploadRequest.Email, result!.Email);
    }

    // AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo? createdEntry);
        // with invalid info
    


    // AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo? createdEntry);
        // with existing info



    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with valid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with invalid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with no existing previous info
}