using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.UserAccess;

public class UserService : IUserService
{
    private IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public Guid? GetUserId(string username)
    {
        return _userRepo.GetUserId(username);
    }

    public User? GetUser(string username)
    {
        return _userRepo.GetUser(username);
    }

    public User? GetUser(Guid id)
    {
        return _userRepo.GetUser(id);
    }

    public bool DeleteUser(Guid userId)
    {
        return _userRepo.DeleteUser(userId);
    }
}