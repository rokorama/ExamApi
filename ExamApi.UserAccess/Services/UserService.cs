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

    public User CreateUser(string username, string password)
    {
        CreatePassword(password, out string passwordHash);
        var user = new User()
        {
            Username = username,
            Password = passwordHash,
            Role = "User"
        };
        return _userRepo.AddNewUser(user);
    }

    private void CreatePassword(string password, out string passwordHash)
    {
        passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }
}