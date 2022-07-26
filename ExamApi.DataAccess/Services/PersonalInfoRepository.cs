using ExamApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExamApi.DataAccess;

public class PersonalInfoRepository : IPersonalInfoRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PersonalInfoRepository> _logger;

    public PersonalInfoRepository(AppDbContext dbContext, ILogger<PersonalInfoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public bool AddInfo(PersonalInfo personalInfo, Guid userId)
    {
        var dbEntry = _dbContext.Users.Find(userId);
        if (dbEntry is null)
        {
            _logger.LogInformation($"Failed attempt to append address to a nonexistent user. User ID: {userId}");
            return false;
        }
        dbEntry.PersonalInfo = personalInfo;
        _dbContext.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        
        try
        {
            _dbContext.SaveChanges();
            _logger.LogInformation($"Address entry {dbEntry.Id} updated at {DateTime.UtcNow}.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update personal info entry {dbEntry.Id} at {DateTime.UtcNow}. {ex.Message}");
            return false;
        }
    }

    public PersonalInfo? GetInfo(Guid userId)
    {
        var result = _dbContext.Users.Include(u => u.PersonalInfo)
                               .Include(u => u.PersonalInfo!.Address)
                               .SingleOrDefault(i => i.Id == userId);
        if (result is null)
            return null;
        
        return result.PersonalInfo;
    }

    public Guid? GetInfoId(Guid userId)
    {
        var result = GetInfo(userId);
        if (result is null)
            return null;
        return result.Id;
    }

    public bool UpdateInfo(Guid userId, PersonalInfo updatedEntry)
    {
        
        var entryToUpdate = _dbContext.Users.Include(u => u.PersonalInfo)
                                            .SingleOrDefault(u => u.Id == userId)!
                                            .PersonalInfo;
        entryToUpdate = updatedEntry;
        _dbContext.Entry(entryToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
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

    public bool UserHasExistingPersonalInfo(Guid userId)
    {
        var personalInfo = _dbContext.Users.Include(u => u.PersonalInfo)
                                            .SingleOrDefault(u => u.Id == userId)!
                                            .PersonalInfo;
        return (personalInfo != null);
    }
}