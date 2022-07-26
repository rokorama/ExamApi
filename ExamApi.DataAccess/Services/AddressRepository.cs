using ExamApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExamApi.DataAccess;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<AddressRepository> _logger;

    public AddressRepository(AppDbContext dbContext, ILogger<AddressRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public bool AddAddress(Address address, Guid userId)
    {
        var dbEntry = _dbContext.Users.Find(userId);
        if (dbEntry is null || dbEntry.PersonalInfo is null)
        {
            _logger.LogInformation($"Failed attempt to append address to a nonexistent user/personal info entry. User ID = {userId}");
            return false;
        }
        dbEntry.PersonalInfo.Address = address;
        _dbContext.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        
        try
        {
            _dbContext.SaveChanges();
            _logger.LogInformation($"Address entry {dbEntry.Id} updated at {DateTime.UtcNow}.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update address entry {dbEntry.Id} at {DateTime.UtcNow}. {ex.Message}");
            return false;
        }
    }

    public Address? GetAddress(Guid userId)
    {
        var result = _dbContext.Users.Include(u => u.PersonalInfo)
                               .Include(u => u.PersonalInfo!.Address)
                               .SingleOrDefault(i => i.Id == userId);
        if (result is null)
            return null;
        
        return result.PersonalInfo!.Address;
    }

    public Guid? GetAddressId(Guid userId)
    {
        var result = GetAddress(userId);
        if (result is null)
            return null;
        return result.Id;
    }

    public bool UserHasExistingAddress(Guid userId)
    {
        var result = GetAddress(userId);
        return (result is not null);
    }

    public bool UpdateAddress(Guid userId, Address updatedEntry)
    {
        
        var entryToUpdate = GetAddress(userId);
        if (entryToUpdate is null)
        {
            _logger.LogInformation($"Failed attempt to update nonexistent address entry. User ID: {userId}");
            return false;
        }
        try 
        {
            _dbContext.SaveChanges();
            _logger.LogInformation($"Personal information entry {updatedEntry.Id} updated at {DateTime.UtcNow}.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update personal info entry {updatedEntry.Id} at {DateTime.UtcNow}. {ex.Message}");
            return false;
        }
    }
}