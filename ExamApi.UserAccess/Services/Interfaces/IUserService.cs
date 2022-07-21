using ExamApi.Models;

namespace ExamApi.UserAccess;

public interface IUserService
{
    public User GetUser(string username);
    public User GetUser(Guid id);
    public Guid GetUserId(string username);
    public bool DeleteUser(Guid userId);

}