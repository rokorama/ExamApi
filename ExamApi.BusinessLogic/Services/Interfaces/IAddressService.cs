namespace ExamApi.BusinessLogic;

public interface IAddressService
{
    public bool UpdateAddress<T>(Guid userId, string propertyName, T newValue);
}