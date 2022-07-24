using ExamApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExamApi.DataAccess;

public class UserRepository : IUserRepository
{
    private AppDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public User GetUser(string username)
    {
        return _dbContext.Users.Include(u => u.PersonalInfo)
                               .Include(u => u.PersonalInfo!.Address)
                               .SingleOrDefault(u => u.Username == username)!;
    }

    public User GetUser(Guid userId)
    {
        return _dbContext.Users.Include(u => u.PersonalInfo)
                               .Include(u => u.PersonalInfo!.Address)
                               .SingleOrDefault(i => i.Id == userId)!;
    }

    public bool AddNewUser(User user)
    {
        _dbContext.Users.Add(user);
        try
        {
            _dbContext.SaveChanges();
            _logger.LogInformation($"New user {user.Username} created at {DateTime.UtcNow}.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to save new user {user.Username} at {DateTime.UtcNow}.", ex);
            return false;
        }
    }

    public bool DeleteUser(Guid userId)
    {
        var userToRemove = _dbContext.Users.Include(u => u.PersonalInfo)
                                           .Include(u => u.PersonalInfo!.Address)
                                           .First(u => u.Id == userId);
        PersonalInfo? personalInfoToRemove = userToRemove.PersonalInfo;
        if (personalInfoToRemove != null)
        {
            _dbContext.PersonalInfos.Remove(personalInfoToRemove);
            Address? addressToRemove = userToRemove.PersonalInfo!.Address;
            if (addressToRemove != null)
            {
                _dbContext.Addresses.Remove(addressToRemove);
            }
        }
        _dbContext.Users.Remove(userToRemove);
        
        try
        {
            _dbContext.SaveChanges();
            _logger.LogInformation($"User {userToRemove.Username} deleted at {DateTime.UtcNow}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete user {userToRemove.Username} at {DateTime.UtcNow}.", ex);
            return false;
        }
    }
}