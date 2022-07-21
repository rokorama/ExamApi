using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IUserRepository
{
    public User AddNewUser(User user);
    public User GetUser(string username);
    public User GetUser(Guid id);
    public bool DeleteUser(Guid id);
}