using ExamApi.Models;

namespace ExamApi.DataAccess;

public class ResidenceInfoRepository : IResidenceInfoRepository
{
    private readonly AppDbContext _dbContext;

    public ResidenceInfoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddInfo(ResidenceInfo info)
    {
        _dbContext.ResidenceInfos.Add(info);
        // try
        // {
            _dbContext.SaveChanges();
        // }
        // catch (Exception)
        // {
        //     // ????
        // }
    }

    public void DeleteInfo(Guid id)
    {
        var entryToDelete = _dbContext.ResidenceInfos.Find(id);
        _dbContext.ResidenceInfos.Remove(entryToDelete);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            // ????
        }
    }

    public void EditInfo(ResidenceInfo updatedInfo)
    {
        var dbEntry = _dbContext.ResidenceInfos.SingleOrDefault(a => a.Id == updatedInfo.Id);
        _dbContext.Entry(dbEntry).CurrentValues.SetValues(updatedInfo);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            // ????
        }
    }

    public ResidenceInfo GetInfo(Guid id)
    {
        return _dbContext.ResidenceInfos.Find(id);
    }
}