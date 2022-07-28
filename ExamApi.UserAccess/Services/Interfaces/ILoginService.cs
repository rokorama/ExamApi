using ExamApi.Models;
using ExamApi.Models.DTOs;

namespace ExamApi.UserAccess;

public interface ILoginService
{
    public string Login(string username, string password);
    public ResponseDto CreateUser(string username, string password);
}