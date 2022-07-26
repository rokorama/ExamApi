using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Tests.TestHelpers;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExamApi.Tests.BusinessLogic;

public class AddressServiceTests
{
    private readonly AddressService _sut;
    private readonly IFixture _fixture;
    private readonly Mock<IAddressRepository> _repoMock = new Mock<IAddressRepository>();
    private readonly Mock<ILogger<AddressService>> _loggerMock = new Mock<ILogger<AddressService>>();
    private readonly Mock<IObjectMapper> _mapperMock = new Mock<IObjectMapper>();
    private readonly Mock<IPropertyChanger> _propertyChangerMock = new Mock<IPropertyChanger>();
    private readonly IValidator<Address> _addressValidator;

    public AddressServiceTests()
    {
        _addressValidator = new InlineValidator<Address>();
        _sut = new AddressService(_repoMock.Object,
                                  _loggerMock.Object,
                                  _mapperMock.Object,
                                  _propertyChangerMock.Object,
                                  _addressValidator);
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }
    
    public void GetAddress_ReturnsDto_WhenUserExists()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid addressId = Guid.NewGuid();
        var address = _fixture.Create<Address>();
        address.City = "Perth";

        _repoMock.Setup(r => r.GetAddress(userId)).Returns(address);
        _mapperMock.Setup(m => m.MapAddressDto(address)).Returns(new AddressDto() { City = address.City} );

        // Act
        AddressDto? mockAddressDto = _sut.GetAddress(userId);
        
        // Assert
        Assert.Equal(mockAddressDto!.City, address.City);
    }

    [Fact]
    public void GetAddress_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAddress(It.IsAny<Guid>())).Returns(value: null);

        // Act
        AddressDto? mockAddressDto = _sut.GetAddress(It.IsAny<Guid>());
        
        // Assert
        Assert.Null(mockAddressDto);
    }

    [Fact]
    public void GetAddress_ReturnsNull_WhenUserExistsAndAddressIsNull()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAddress(It.IsAny<Guid>())).Returns(value: null);

        // Act
        AddressDto? mockAddressDto = _sut.GetAddress(It.IsAny<Guid>());

        //Assert
        Assert.Null(mockAddressDto);
    }


    [Fact]
    public void GetAddressId_ReturnsId_WhenDataExists()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        var address = _fixture.Create<Address>();
        _repoMock.Setup(r => r.GetAddressId(userId)).Returns(address.Id);

        // Act
        var returnedId = _sut.GetAddressId(userId);

        // Assert
        Assert.Equal(address.Id, returnedId);
    }

    [Fact]
    public void GetAddressId_ReturnsNull_WhenAddressDoesNotExist()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        _repoMock.Setup(r => r.GetAddressId(userId)).Returns(value: null);

        // Act
        var returnedId = _sut.GetAddressId(userId);

        // Assert
        Assert.Null(returnedId);
    }


    [Fact]
    public void UpdateAddress_WorksCorrectly_WhenNewValueIsValid()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        var infoBeforeChange = _fixture.Create<Address>();
        _repoMock.Setup(r => r.GetAddress(userId)).Returns(infoBeforeChange);
        _repoMock.Setup(r => r.UpdateAddress(userId, It.IsAny<Address>())).Returns(true);
        var propertyToChange = "City";
        var newValue = "test@aaaaa.com";

        var infoAfterChange = infoBeforeChange;
        infoAfterChange.City = newValue;

        _propertyChangerMock.Setup(pc => pc.UpdateAddress(infoBeforeChange, propertyToChange, newValue))
                            .Returns(infoAfterChange);

        
        // Act
        var change = _sut.UpdateAddress<string>(userId, propertyToChange, newValue);

        // Assert
        Assert.True(change.Success);
    }

    [Fact]
    public void UpdateAddress_ReturnsFailure_WhenInputIsInvalid()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        var propertyToChange = "City";
        string newValue = null!;
        var expectedResponseMessage = $"Cannot update {propertyToChange} to an invalid value.";
        
        var infoBeforeChange = _fixture.Create<Address>();
        _repoMock.Setup(r => r.GetAddress(userId)).Returns(infoBeforeChange);
        _repoMock.Setup(r => r.UpdateAddress(userId, It.IsAny<Address>())).Returns(false);

        var infoAfterChange = (Address)ObjectCloner.CloneObject(infoBeforeChange);
        infoAfterChange.City = null;

            // Override the subject under testing - the validator does not fail during testing 
            // and has to be set up specifically
        var validatorOverride = new InlineValidator<Address>();
        validatorOverride.RuleFor(x => x.City).Must(city => false); 
        var sutOverride = new AddressService(_repoMock.Object,
                                                  _loggerMock.Object,
                                                  _mapperMock.Object,
                                                  _propertyChangerMock.Object,
                                                  validatorOverride);

        // Act
        var actualDto = sutOverride.UpdateAddress<string>(userId, propertyToChange, newValue);

        // Assert
        Assert.False(actualDto.Success);
        Assert.Equal(expectedResponseMessage, actualDto.Message);
    }

    [Fact]
    public void UpdateAddress_ReturnsFailure_WhenNoPreviousAddressExists()
    {
        // Arrange
        var userId = _fixture.Create<Guid>();
        var infoBeforeChange = _fixture.Create<Address>();
        var expectedResponseMessage = "User has no existing address to update. Please submit complete personal information first.";
        _repoMock.Setup(r => r.GetAddress(userId)).Returns(value: null!);
        _repoMock.Setup(r => r.UserHasExistingAddress(userId)).Returns(true);

        // Act
        var change = _sut.UpdateAddress<string>(userId, It.IsAny<string>(), It.IsAny<string>());

        // Assert
        Assert.False(change.Success);
        Assert.Equal(expectedResponseMessage, change.Message);
    }
}