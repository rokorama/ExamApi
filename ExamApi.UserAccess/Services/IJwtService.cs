namespace ExamApi.UserAccess;

public interface IJwtService
{
    public string GetJwtToken(string username, string role);
}