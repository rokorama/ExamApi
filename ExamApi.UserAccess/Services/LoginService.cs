using ExamApi.DataAccess;
using ExamApi.Models;
using ExamApi.Models.DTOs;

namespace ExamApi.UserAccess;

public class LoginService : ILoginService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepo;

    public LoginService(IJwtService jwtService, IUserRepository userRepo)
    {
        _jwtService = jwtService;
        _userRepo = userRepo;
    }
    public ResponseDto CreateUser(string username, string password)
    {
        if (_userRepo.GetUser(username) is not null)
            return new ResponseDto(false, "This username is taken, please try another");
        
        CreatePassword(password, out string passwordHash);
        var user = new User()
        {
            Username = username,
            Password = passwordHash,
            Role = "User",
        };
        
        if (_userRepo.AddNewUser(user) is not true)
            return new ResponseDto(false, "Failed to add user.");
        else
            return new ResponseDto(true);
    }

    public string Login(string username, string password)
    {
        var user = _userRepo.GetUser(username);
        if (user is null)
            throw new ArgumentException("Incorrect credentials.");
        if (!VerifyPasswordHash(password, user.Password!))
            throw new ArgumentException("Incorrect credentials.");
        return _jwtService.GetJwtToken(user.Username!, user.Role!);
    }

    private void CreatePassword(string password, out string passwordHash)
    {
        passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}