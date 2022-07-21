using ExamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApi.DataAccess;

public class UserRepository : IUserRepository
{
    private AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetUser(string username)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Username == username);
    }

    public User GetUser(Guid userId)
    {
        return _dbContext.Users.Include(u => u.PersonalInfo).SingleOrDefault(i => i.Id == userId);
    }

    // no nulls please
    public User AddNewUser(User user)
    {
        _dbContext.Users.Add(user);
        if (_dbContext.SaveChanges() > 0)
            return user;
        else
            return null;
    }

    public bool DeleteUser(Guid userId)
    {
        var userToRemove = _dbContext.Users.Find(userId);
        _dbContext.Users.Remove(userToRemove);
        var result = _dbContext.SaveChanges();

        return result > 0;
    }
}