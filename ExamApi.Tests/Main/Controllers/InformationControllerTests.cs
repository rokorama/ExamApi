using System.Security.Claims;
using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Controllers;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class InformationControllerTests
{
    private InformationController _sut;
    private readonly Mock<IPersonalInfoService> _personalInfoServiceMock = new Mock<IPersonalInfoService>();
    private readonly Mock<IAddressService> _addressServiceMock = new Mock<IAddressService>();
    private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
    private readonly Mock<ILogger<InformationController>> _loggerMock = new Mock<ILogger<InformationController>>();
    private readonly Mock<IImageConverter> _imageConverterMock = new Mock<IImageConverter>();
    private readonly IFixture _fixture;
    
    public InformationControllerTests()
    {
        // Create a fake HTTP context for the controller to contain claims and create URLs
        var claims = new List<Claim>() 
        { 
            new Claim(ClaimTypes.Name, "username"),
            // Eventually figure out how to mock Admin as well
            new Claim(ClaimTypes.Role, "User"),
            new Claim("name", "John Doe"),
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication")); 

        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Scheme).Returns("https");
        request.Setup(x => x.Host).Returns(HostString.FromUriComponent("localhost:7031"));

        var httpContext = Mock.Of<HttpContext>(
            _ => _.Request == request.Object &&
            _.User == user);

        var controllerContext = new ControllerContext() {
            HttpContext = httpContext,
        };

        // Finally construct a controller and pass in the context created above
        _sut = new InformationController(_personalInfoServiceMock.Object,
                                         _addressServiceMock.Object,
                                         _userServiceMock.Object,
                                         _loggerMock.Object,
                                         _imageConverterMock.Object)

                                         { ControllerContext = controllerContext};

        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Fact]
    public void AddPersonalInfo_Returns201_WithValidUploadRequest()
    {
        // Arrange
        var sampleUploadRequest = _fixture.Create<PersonalInfoUploadRequest>();

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(It.IsAny<Guid>());
        _personalInfoServiceMock.Setup(pis => pis.AddInfo(sampleUploadRequest, It.IsAny<Guid>())).Returns(new ResponseDto(true));
        
        // Act
        var result = _sut.AddPersonalInfo(sampleUploadRequest).Result as ObjectResult;

        // Assert
        Assert.Equal(201, result!.StatusCode);

    }

    [Fact]
    public void AddPersonalInfo_Returns400_WithInvalidUploadRequest()
    {
        // Arrange
        var sampleUploadRequest = _fixture.Create<PersonalInfoUploadRequest>();

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(It.IsAny<Guid>());
        _personalInfoServiceMock.Setup(pis => pis.AddInfo(sampleUploadRequest, It.IsAny<Guid>())).Returns(new ResponseDto(false));
        
        // Act
        var result = _sut.AddPersonalInfo(sampleUploadRequest).Result as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void GetPersonalInfo_Returns200_WhenInfoExists()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedInfo = _fixture.Create<PersonalInfoDto>();

        _personalInfoServiceMock.Setup(pis => pis.GetInfo(sampleGuid))!.Returns(expectedInfo);
        
        // Act
        var result = _sut.GetPersonalInfo(sampleGuid).Result as ObjectResult;

        // Assert
        Assert.Equal(200, result!.StatusCode);
        Assert.Equal(expectedInfo, result.Value);
    }

    [Fact]
    public void GetPersonalInfo_Returns404_WhenInfoDoesNotExist()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        _personalInfoServiceMock.Setup(pis => pis.GetInfo(sampleGuid))!.Returns(value: null);
        
        // Act
        var result = _sut.GetPersonalInfo(sampleGuid).Result;

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, ((NotFoundResult)result!).StatusCode);
    }

    [Fact]
    public void UpdateFirstName_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateFirstName("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateFirstName_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateFirstName("asdasd") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateLastName_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateLastName("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateLastName_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateLastName("asdasd") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdatePersonalNo_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<ulong>(sampleGuid, It.IsAny<string>(), It.IsAny<ulong>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdatePersonalNo(123) as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdatePersonalNo_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<ulong>(sampleGuid, It.IsAny<string>(), It.IsAny<ulong>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdatePersonalNo(123) as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateEmail_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateEmail("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateEmail_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateEmail("asdasd") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdatePhoto_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<byte[]>(sampleGuid, It.IsAny<string>(), It.IsAny<byte[]>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdatePhoto(_fixture.Create<ImageUploadRequest>()) as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdatePhoto_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _personalInfoServiceMock.Setup(pis => pis.UpdateInfo<byte[]>(sampleGuid, It.IsAny<string>(), It.IsAny<byte[]>()))!.Returns(expectedResponse);        
        
        // Act
        var result = _sut.UpdatePhoto(_fixture.Create<ImageUploadRequest>()) as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateCity_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateCity("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateCity_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateCity("asdasd") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateStreet_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateStreet("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateStreet_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateStreet("asdasd") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateHouse_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateHouse("asdasd") as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateHouse_Returns400_WhenNewValueIsInvalid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(false);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<string>(sampleGuid, It.IsAny<string>(), It.IsAny<string>()))!.Returns(expectedResponse);        
        // Act
        var result = _sut.UpdateHouse("666A") as ObjectResult;

        // Assert
        Assert.Equal(400, result!.StatusCode);
    }

    [Fact]
    public void UpdateFlat_Returns204_WhenNewValueIsValid()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<int?>(sampleGuid, It.IsAny<string>(), It.IsAny<int?>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateFlat(1) as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    [Fact]
    public void UpdateFlat_Returns204_WhenNewValueIsNull()
    {
        // Arrange
        var sampleGuid = _fixture.Create<Guid>();
        var expectedResponse = new ResponseDto(true);

        _userServiceMock.Setup(us => us.GetUserId(It.IsAny<string>())).Returns(sampleGuid);
        _addressServiceMock.Setup(pis => pis.UpdateAddress<int?>(sampleGuid, It.IsAny<string>(), It.IsAny<int?>()))!.Returns(expectedResponse);
        
        // Act
        var result = _sut.UpdateFlat(null) as StatusCodeResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }
}