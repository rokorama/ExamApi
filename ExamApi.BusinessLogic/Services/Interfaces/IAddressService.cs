using ExamApi.Models;
using ExamApi.Models.DTOs;

namespace ExamApi.BusinessLogic;

public interface IAddressService
{
    public ResponseDto UpdateAddress<T>(Guid userId, string propertyName, T newValue);
    public Address? GetAddress(Guid userId);
}