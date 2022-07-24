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
    public void GetUser_WorksCorrectly_WhenInputIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var personalInfoId = Guid.NewGuid();
        var personalInfoFirstName = "Steve";
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

    [Fact]
    public void GetUser_WorksCorrectly_WhenInputIsInvalid()
    {
        PersonalInfo? repoReturnValue = null;
        // Arrange
        var userId = Guid.NewGuid();
        // var personalInfoId = Guid.NewGuid();
        // var personalInfoFirstName = "Steve";
        // var personalInfo = new PersonalInfo()
        // {
        //     Id = personalInfoId,
        //     FirstName = personalInfoFirstName,
        //     LastName = It.IsAny<String>(),  
        //     PersonalNumber = It.IsAny<ulong>(),
        //     Email = It.IsAny<String>(),
        //     Photo = It.IsAny<byte[]>(),
        //     Address = new Address()
        // };
        _repoMock.Setup(r => r.GetInfo(userId)).Returns(repoReturnValue);

        // Act
        var mockPersonalInfoDto = _sut.GetInfo(userId);

        // Assert
        Assert.Null(mockPersonalInfoDto);
    }
}