using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using Moq;

namespace ExamApi.Tests.BusinessLogic;

public class ObjectMapperTests
{
    private readonly ObjectMapper _sut;
    private readonly IFixture _fixture;
    private readonly Mock<IImageConverter> _imageConverterMock = new Mock<IImageConverter>();

    public ObjectMapperTests()
    {
        _sut = new ObjectMapper(_imageConverterMock.Object);
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Fact]
    public void MapPersonalInfoUpload_WorksCorrectly()
    {
        // Arrange
        var sampleUpload = _fixture.Create<PersonalInfoUploadRequest>();
        
        // Act
        var result = _sut.MapPersonalInfoUpload(sampleUpload);

        // Assert
        Assert.Equal(sampleUpload.Email, result.Email);
        Assert.Equal(sampleUpload.Address!.City, result.Address!.City);
    }

    [Fact]
    public void MapPersonalInfoUpload_NoInput_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.MapPersonalInfoUpload(null!));
    }

    [Fact]
    public void MapAddressEntity_WorksCorrectly()
    {
        // Arrange
        var sampleAddressDto = _fixture.Create<AddressDto>();
        
        // Act
        var result = _sut.MapAddressEntity(sampleAddressDto);

        // Assert
        Assert.Equal(sampleAddressDto.City, result.City);
        Assert.Equal(sampleAddressDto.Flat, result.Flat);
    }

    [Fact]
    public void MapAddressEntity_NoInput_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.MapAddressEntity(null!));
    }

    [Fact]
    public void MapPersonalInfoDto_WorksCorrectly()
    {
        // Arrange
        var samplePersonalInfo = _fixture.Create<PersonalInfo>();
        
        // Act
        var result = _sut.MapPersonalInfoDto(samplePersonalInfo);

        // Assert
        Assert.Equal(samplePersonalInfo.LastName, result.LastName);
        Assert.Equal(samplePersonalInfo.Address!.City, result.Address!.City);
    }

    [Fact]
    public void MapPersonalInfoDto_NoInput_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.MapPersonalInfoDto(null!));
    }


    [Fact]
    public void MapAddressDto_WorksCorrectly()
    {
        // Arrange
        var sampleAddress = _fixture.Create<Address>();
        
        // Act
        var result = _sut.MapAddressDto(sampleAddress);

        // Assert
        Assert.Equal(sampleAddress.City, result.City);
        Assert.Equal(sampleAddress.Flat, result.Flat);
    }
    
    [Fact]
    public void MapAddressDto_NoInput_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.MapAddressDto(null!));
    }
}