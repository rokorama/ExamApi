using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IAddressRepository
{
    public Address? GetAddress(Guid userId);
    public Guid? GetAddressId(Guid userId);
    // public bool AddAddress(Address address, Guid userId);
    public bool UpdateAddress(Guid userId, Address entryToUpdate);
    public bool UserHasExistingAddress(Guid userId);
}