using AutoFixture;
using AutoFixture.AutoMoq;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models;
using Moq;

namespace ExamApi.Tests.BusinessLogic;

public class PropertyChangerTests
{
    private readonly PropertyChanger _sut;
    private readonly IFixture _fixture;

    public PropertyChangerTests()
    {
        _sut = new PropertyChanger();
        _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
    }

    [Theory]
    [MemberData(nameof(AddressData))]
    public void UpdateProperty_WorksCorrectly_OnAddress(string property, object newVal)
    {
        // Arrange
        var sampleAddress = _fixture.Create<Address>();

        // Act
        var changedAddress = (Address)_sut.UpdateProperty(sampleAddress, property, newVal!);
        
        // Assert
        Assert.Equal(newVal, changedAddress.GetType().GetProperty(property)!.GetValue(changedAddress, null));
    }

    [Theory]
    [MemberData(nameof(PersonalInfoData))]
    public void UpdateProperty_WorksCorrectly_OnPersonalInfo<T>(string property, T newVal)
    {
        // Arrange
        var samplePersonalInfo = _fixture.Create<PersonalInfo>();

        // Act
        var changedPersonalInfo = (PersonalInfo)_sut.UpdateProperty<T>(samplePersonalInfo, property, newVal);
        
        // Assert
        Assert.Equal(newVal, changedPersonalInfo.GetType().GetProperty(property)!.GetValue(changedPersonalInfo, null));
    }

    [Fact]
    public void UpdateProperty_ThrowsException_WithNoObject()
    {
        Assert.Throws<NullReferenceException>(() => _sut.UpdateProperty(null!, It.IsAny<string>(), It.IsAny<string>()));
    }
    
    [Fact]
    public void UpdateProperty_ThrowsException_WithNoPropertyName()
    {
        Assert.Throws<NullReferenceException>(() => _sut.UpdateProperty(It.IsAny<object>(), null!, It.IsAny<object>()));
    }

    [Fact]
    public void UpdateProperty_ThrowsException_WithNoNewValue()
    {
        Assert.Throws<NullReferenceException>(() => _sut.UpdateProperty(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<object>()));
    }

    // MemberData generators

    public static IEnumerable<object[]> AddressData =>
    new List<object[]>
    {
        new object[] { "City", "Buenos Aires" },
        new object[] { "Street", "Avenida de 9 julio" },
        new object[] { "House", "93" },
        new object[] { "Flat", null! },
    };

    public static IEnumerable<object[]> PersonalInfoData =>
    new List<object[]>
    {
        new object[] { "FirstName", "Test" },
        new object[] { "LastName", "Testsson" },
        new object[] { "PersonalNumber", (ulong)393949596979 },
        new object[] { "Email", "hello@test.test" },
        new object[] { "Photo", new byte[] {}}
    };
}