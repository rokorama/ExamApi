using ExamApi.Models;

namespace ExamApi.DataAccess;

public class PersonalInfoRepository : IPersonalInfoRepository
{
    private readonly AppDbContext _dbContext;

    public PersonalInfoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AddInfo(PersonalInfo personalInfo)
    {
        _dbContext.PersonalInfos.Add(personalInfo);
        var result = _dbContext.SaveChanges();
        return result > 0;
    }

    public bool DeleteInfo(User user)
    {
        var infoToDelete = _dbContext.PersonalInfos.Find(user.PersonalInfo.Id);
        _dbContext.PersonalInfos.Remove(infoToDelete);
        var result = _dbContext.SaveChanges();
        return result > 0;
    }

    // implement these
    public bool EditInfo(Guid userId, User user)
    {
        throw new NotImplementedException();
    }

    public PersonalInfo GetInfo(Guid userId)
    {
        return _dbContext.PersonalInfos.SingleOrDefault(i => i.Id == userId);
        // return _dbContext.PersonalInfos.Find(userId);
    }
}