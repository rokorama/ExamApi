using ExamApi.Models;

namespace ExamApi.DataAccess;

public interface IResidenceInfoRepository
{
    public ResidenceInfo GetInfo(Guid id);
    public void AddInfo(ResidenceInfo info);
    public void EditInfo(ResidenceInfo info);
    public void DeleteInfo(Guid id);
}