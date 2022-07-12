using ExamApi.Models;

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

    // no nulls please
    public User AddNewUser(User user)
    {
        _dbContext.Users.Add(user);
        if (_dbContext.SaveChanges() > 0)
            return user;
        else
            return null;
    }

    public bool GrantAdminRights(User user)
    {
        throw new NotImplementedException();
    }

    public bool RevokeUserRights(User user)
    {
        throw new NotImplementedException();
    }
}