using System.Reflection;
using ExamApi.Models;

namespace ExamApi.BusinessLogic.Helpers;

public class PropertyChanger : IPropertyChanger
{
    public object UpdatePersonalInfo<T>(PersonalInfo obj, string propertyToUpdate, T newValue)
    {
        Type t = obj.GetType();
        PropertyInfo? targetProperty = t.GetProperty(propertyToUpdate);
        // throw error if null
        targetProperty!.SetValue(obj, newValue);
        return obj;
    }

    public object UpdateAddress<T>(Address obj, string propertyToUpdate, T newValue)
    {
        Type t = obj.GetType();
        PropertyInfo? targetProperty = t.GetProperty(propertyToUpdate);
        // throw error if null
        targetProperty!.SetValue(obj, newValue);
        return obj;
    }
}