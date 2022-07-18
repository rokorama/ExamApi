using ExamApi.Models;

namespace ExamApi.UserAccess;

public interface IUserService
{
    public string Login(string username, string password);
    public User CreateUser(string username, string password);
    public User GetUser(string username);
    public User GetUser(Guid id);
    public bool GrantAdminRights(string username);
    public bool RevokeUserRights(string username);
}