using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public interface IResidenceInfoService
{
    public ResidenceInfo AddInfo(ResidenceInfoDto residenceInfoDto);
    public ResidenceInfo GetInfo(Guid id);
    public void DeleteInfo(Guid id);
}