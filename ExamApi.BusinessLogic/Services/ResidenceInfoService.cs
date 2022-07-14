using ExamApi.DataAccess;
using ExamApi.Models;

namespace ExamApi.BusinessLogic;

public class ResidenceInfoService : IResidenceInfoService
{
    private readonly IResidenceInfoRepository _residenceInfoRepo;

    public ResidenceInfoService(IResidenceInfoRepository residenceInfoRepo)
    {
        _residenceInfoRepo = residenceInfoRepo;
    }

    public ResidenceInfo AddInfo(ResidenceInfoDto residenceInfoDto)
    {
        var entry = new ResidenceInfo()
        {
            Id = Guid.NewGuid(),
            City = residenceInfoDto.City,
            Street = residenceInfoDto.Street,
            House = residenceInfoDto.House,
            Flat = residenceInfoDto.Flat
        };
        _residenceInfoRepo.AddInfo(entry);
        return entry;
    }

    public void DeleteInfo(Guid id)
    {
        _residenceInfoRepo.DeleteInfo(id);
    }

    public ResidenceInfo GetInfo(Guid id)
    {
        return _residenceInfoRepo.GetInfo(id);
    }
}