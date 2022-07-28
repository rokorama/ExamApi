using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.UserAccess;
using Moq;

namespace ExamApi.Tests.UserAccess;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
    private readonly UserService _sut;
    private readonly IFixture _fixture;

    public UserServiceTests()
    {
        _sut = new UserService(_userRepoMock.Object);
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Fact]
    public void GetUser_WorksCorrectly_WithExistingUsername()
    {
        // Arrange
        var expectedUser = _fixture.Create<User>();
        var sampleUsername = _fixture.Create<string>();
        _userRepoMock.Setup(ur => ur.GetUser(sampleUsername)).Returns(expectedUser);

        // Act
        var result = _sut.GetUser(sampleUsername);

        // Assert
        Assert.IsType<User>(result);
    }

    [Fact]
    public void GetUser_ReturnsNull_WithNonexistentUsername()
    {
        // Arrange
        var sampleUsername = _fixture.Create<string>();
        _userRepoMock.Setup(ur => ur.GetUser(sampleUsername)).Returns(value: null!);

        // Act
        var result = _sut.GetUser(sampleUsername);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUser_ReturnsNull_WithNullUsername()
    {
        // Arrange
        _userRepoMock.Setup(ur => ur.GetUser(null!)).Returns(value: null!);

        // Act
        var result = _sut.GetUser(null!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUser_WorksCorrectly_WithExistingId()
    {
        // Arrange
        var expectedUser = _fixture.Create<User>();
        var sampleId = _fixture.Create<Guid>();
        _userRepoMock.Setup(ur => ur.GetUser(sampleId)).Returns(expectedUser);

        // Act
        var result = _sut.GetUser(sampleId);

        // Assert
        Assert.IsType<User>(result);
    }

    [Fact]
    public void GetUser_ReturnsNull_WithNonexistentId()
    {
        // Arrange
        var sampleId = _fixture.Create<Guid>();
        _userRepoMock.Setup(ur => ur.GetUser(sampleId)).Returns(value: null!);

        // Act
        var result = _sut.GetUser(sampleId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUser_ReturnsNull_WithNullId()
    {
        // Arrange
        _userRepoMock.Setup(ur => ur.GetUser(null!)).Returns(value: null!);

        // Act
        var result = _sut.GetUser(null!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUserId_WorksCorrectly_WithExistingUsername()
    {
        // Arrange
        var expectedGuid = _fixture.Create<Guid>();
        var sampleUsername = _fixture.Create<string>();
        _userRepoMock.Setup(ur => ur.GetUserId(sampleUsername)).Returns(expectedGuid);

        // Act
        var result = _sut.GetUserId(sampleUsername);

        // Assert
        Assert.Equal(expectedGuid, result);
    }

    [Fact]
    public void GetUserId_ReturnsNull_WithNonexistentUsername()
    {
        // Arrange
        var sampleUsername = _fixture.Create<string>();
        _userRepoMock.Setup(ur => ur.GetUserId(sampleUsername)).Returns(value: null);

        // Act
        var result = _sut.GetUserId(sampleUsername);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetUserId_ReturnsNull_WithNullUsername()
    {
        // Arrange
        _userRepoMock.Setup(ur => ur.GetUser(null!)).Returns(value: null!);

        // Act
        var result = _sut.GetUserId(null!);

        // Assert
        Assert.Null(result);
    }

    // public User GetUser(Guid id);
    // public Guid GetUserId(string username);
    // public bool DeleteUser(Guid userId);
}