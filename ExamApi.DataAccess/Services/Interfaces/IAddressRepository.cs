using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IAddressRepository
{
    public Address GetInfo(Guid id);
    public void AddInfo(Address info);
    public void EditInfo(Address info);
    public void DeleteInfo(Guid id);
}