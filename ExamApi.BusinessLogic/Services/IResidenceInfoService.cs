using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IAddressService
{
    public Address AddInfo(AddressDto addressDto);
    public Address GetInfo(Guid id);
    public void DeleteInfo(Guid id);
}