using ExamApi.Models;

namespace ExamApi.UserAccess;

public interface ILoginService
{
    public string Login(string username, string password);
    public User CreateUser(string username, string password);
}