using System.Reflection;
using ExamApi.Models;

namespace ExamApi.BusinessLogic.Helpers;

public class PropertyChanger : IPropertyChanger
{
    public object UpdateProperty<T>(object obj, string propertyToUpdate, T newValue)
    {
        Type t = obj.GetType();
        PropertyInfo? targetProperty = t.GetProperty(propertyToUpdate);
        // throw error if null
        targetProperty!.SetValue(obj, newValue);
        return obj;
    }
}