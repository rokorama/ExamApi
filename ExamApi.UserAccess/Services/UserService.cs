using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.UserAccess;

public class UserService : IUserService
{
    private IUserRepository _userRepo;
    private IJwtService _jwtService;

    public UserService(IUserRepository userRepo, IJwtService jwtService)
    {
        _userRepo = userRepo;
        _jwtService = jwtService;
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
        };
        return _userRepo.AddNewUser(user);
    }

    public User GetUser(string username)
    {
        return _userRepo.GetUser(username);
    }

    public User GetUser(Guid id)
    {
        return _userRepo.GetUser(id);
    }

    public string Login(string username, string password)
    {
        var user = GetUser(username);
        if (!VerifyPasswordHash(password, user.Password))
            throw new Exception("Incorrect credentials");
        return _jwtService.GetJwtToken(user.Username, user.Role);
    }

    private void CreatePassword(string password, out string passwordHash)
    {
        passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public bool GrantAdminRights(string username)
    {
        throw new NotImplementedException();
    }

    public bool RevokeUserRights(string username)
    {
        throw new NotImplementedException();
    }
}