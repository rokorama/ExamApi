using ExamApi.Models;

namespace ExamApi.BusinessLogic.Helpers;

public interface IPropertyChanger
{
        public object UpdatePersonalInfo<T>(PersonalInfo obj, string propertyToUpdate, T newValue);
        public object UpdateAddress<T>(Address obj, string propertyToUpdate, T newValue);
}