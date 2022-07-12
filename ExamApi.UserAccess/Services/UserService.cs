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
            Id = Guid.NewGuid(),
            Username = username,
            Password = passwordHash,
            Role = "User",
            // placeholder data
            // PersonalInfo = new PersonalInfo()
            // {
            //     Id = Guid.NewGuid()
            // }
        };
        return _userRepo.AddNewUser(user);
    }

    public User GetUser(string username)
    {
        throw new NotImplementedException();
    }

    public bool GrantAdminRights(string username)
    {
        throw new NotImplementedException();
    }

    public string Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public bool RevokeUserRights(string username)
    {
        throw new NotImplementedException();
    }

    private void CreatePassword(string password, out string passwordHash)
    {
        passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }
}