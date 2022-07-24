using System;
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

    public PersonalInfoRepository()
    {
    }

    public bool AddInfo(PersonalInfo personalInfo, Guid userId)
    {
        // var user = _dbContext.Users.Find(userId);
        // user.PersonalInfo = personalInfo;
        var dbEntry = _dbContext.Users.Find(userId);
        dbEntry.PersonalInfo = personalInfo;
        _dbContext.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        // _dbContext.PersonalInfos.Find(userId).
        var result = _dbContext.SaveChanges();
        return result > 0;
    }

    // public bool DeleteInfo(User user)
    // {
    //     var infoToDelete = _dbContext.PersonalInfos.Find(user.PersonalInfo.Id);
    //     _dbContext.PersonalInfos.Remove(infoToDelete);
    //     var result = _dbContext.SaveChanges();
    //     return result > 0;
    // }

    // implement these
    public bool EditInfo(Guid userId, User user)
    {
        throw new NotImplementedException();
    }

    public PersonalInfo GetInfo(Guid userId)
    {
        return _dbContext.Users.Include(u => u.PersonalInfo)
                               .Include(u => u.PersonalInfo.Address)
                               .SingleOrDefault(i => i.Id == userId)
                               .PersonalInfo;
    }

    public bool UpdateInfo(Guid userId, PersonalInfo updatedEntry)
    {
        
        var entryToUpdate = _dbContext.Users.Include(u => u.PersonalInfo)
                                            .SingleOrDefault(u => u.Id == userId)
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
            _logger.LogError($"Failed to update personal info entry {updatedEntry.Id} at {DateTime.UtcNow}.", ex);
            return false;
        }
    }

    public bool CheckForExistingPersonalInfo(Guid userId)
    {
        var personalInfo = _dbContext.Users.Include(u => u.PersonalInfo)
                                            .SingleOrDefault(u => u.Id == userId)
                                            .PersonalInfo;
        return (personalInfo != null);
    }

    public bool CheckForExistingAddress(Guid userId)
    {
        try
        {
            var address = _dbContext.Users.Include(u => u.PersonalInfo)
                                          .SingleOrDefault(u => u.Id == userId)
                                          .PersonalInfo
                                          .Address;
            return true;
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }
}