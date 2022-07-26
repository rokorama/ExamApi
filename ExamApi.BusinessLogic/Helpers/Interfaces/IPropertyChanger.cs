using ExamApi.Models;

namespace ExamApi.BusinessLogic.Helpers;

public interface IPropertyChanger
{
        public object UpdateProperty<T>(object obj, string propertyToUpdate, T newValue);
}