using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IAddressService
{
    public Address AddInfo(AddressDto addressDto);
    public bool GetInfo(Guid id, out AddressDto entry);
    public void DeleteInfo(Guid id);
}