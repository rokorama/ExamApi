using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models.UploadRequests;
using Moq;

namespace ExamApi.Tests.BusinessLogic;

public class ImageConverterTests
{
    private readonly ImageConverter _sut;
    private readonly IFixture _fixture;
    
    public ImageConverterTests()
    {
        _sut = new ImageConverter();
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

    }
    [Fact]
    public void ConvertImage_ReturnsResizedImage_WithValidUpload()
    {
        // Arrange
        var mockResizedImage = _fixture.Create<byte[]>();
        var sampleImageUpload = _fixture.Create<ImageUploadRequest>();

            // Create new subject under testing so that ResizeImage() returns byte[] without
            // uploading an actual valid image file
        var mockSut = new Mock<ImageConverter>();
        mockSut.CallBase = true;
        mockSut.Setup(x => x.ResizeImage(It.IsAny<byte[]>())).Returns(mockResizedImage);
        ImageConverter sutOverride = mockSut.Object;

        // Act
        var expected = sutOverride.ConvertImage(sampleImageUpload);

        // Assert
        Assert.Equal(expected, mockResizedImage);
    }

    [Fact]
    public void ConvertImage_ThrowsUnknownImageFormatException_WithInvalidUpload()
    {
        // Arrange
        var sampleImageUpload = _fixture.Create<ImageUploadRequest>();

        // Assert
        Assert.Throws<SixLabors.ImageSharp.UnknownImageFormatException>(() => _sut.ConvertImage(sampleImageUpload));
    }

    [Fact]
    public void ConvertImage_NullReferenceException_WithNullInput()
    {
        // Arrange
        var sampleImageUpload = _fixture.Create<ImageUploadRequest>();

        // Assert
        Assert.Throws<NullReferenceException>(() => _sut.ConvertImage(null!));
    }
}