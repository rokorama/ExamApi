using ExamApi.Models;

namespace ExamApi.DataAccess;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _dbContext;

    public AddressRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddInfo(Address info)
    {
        _dbContext.Addresses.Add(info);
        // try
        // {
            _dbContext.SaveChanges();
        // }
        // catch (Exception)
        // {
        //     // ????
        // }
    }

    public void DeleteInfo(Guid id)
    {
        var entryToDelete = _dbContext.Addresses.Find(id);
        _dbContext.Addresses.Remove(entryToDelete);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            // ????
        }
    }

    public void EditInfo(Address updatedInfo)
    {
        var dbEntry = _dbContext.Addresses.SingleOrDefault(a => a.Id == updatedInfo.Id);
        _dbContext.Entry(dbEntry).CurrentValues.SetValues(updatedInfo);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            // ????
        }
    }

    public Address GetInfo(Guid id)
    {
        return _dbContext.Addresses.Find(id);
    }
}