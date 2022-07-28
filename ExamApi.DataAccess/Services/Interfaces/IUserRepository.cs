using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IUserRepository
{
    public bool AddNewUser(User user);
    public User? GetUser(string username);
    public User? GetUser(Guid id);
    public Guid? GetUserId(string username);
    public bool DeleteUser(Guid id);
}