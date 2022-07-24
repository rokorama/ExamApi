using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ExamApi.Tests.BusinessLogic;

public class PersonalInfoServiceTests
{
    private readonly PersonalInfoService _sut;
    private readonly Mock<IPersonalInfoRepository> _repoMock = new Mock<IPersonalInfoRepository>();
    private readonly Mock<ILogger<PersonalInfoService>> _loggerMock = new Mock<ILogger<PersonalInfoService>>();

    
    public PersonalInfoServiceTests()
    {
        _sut = new PersonalInfoService(_repoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void GetInfo_WorksCorrectly_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var personalInfoId = Guid.NewGuid();
        var personalInfoFirstName = "Steve";
        // ???
        var personalInfo = new PersonalInfo()
        {
            Id = personalInfoId,
            FirstName = personalInfoFirstName,
            LastName = It.IsAny<String>(),  
            PersonalNumber = It.IsAny<ulong>(),
            Email = It.IsAny<String>(),
            Photo = It.IsAny<byte[]>(),
            Address = new Address()
        };
        _repoMock.Setup(r => r.GetInfo(userId)).Returns(personalInfo);

        // Act
        var mockPersonalInfoDto = _sut.GetInfo(userId);
        
        // Assert
        Assert.Equal(mockPersonalInfoDto!.FirstName, personalInfoFirstName);
    }

    // get info when info does not exist

    [Fact]
    public void GetInfo_WorksCorrectly_WhenUserIsNonexistent()
    {
        // Arrange
        _repoMock.Setup(r => r.GetInfo(It.IsAny<Guid>())).Returns(value: null);

        // Act
        var mockPersonalInfoDto = _sut.GetInfo(It.IsAny<Guid>());
        
        // Assert
        Assert.Null(mockPersonalInfoDto);
    }

    // AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo? createdEntry);
        // with valid info
    [Fact]
    public void AddInfo
    // AddInfo(PersonalInfoUploadRequest uploadRequest, Guid userId, out PersonalInfo? createdEntry);
        // with invalid info



    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with valid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with invalid info
    // public bool UpdateInfo<T>(Guid userId, string propertyName, T newValue);
        // with no existing previous info
}