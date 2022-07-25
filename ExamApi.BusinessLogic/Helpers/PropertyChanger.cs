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


    // public static PropertyInfo GetProperty(object obj, string propertyToUpdate)
    // {
    //     //handle nulls somehow
    //     if (obj == null)
    //         throw new ArgumentException("Value cannot be null.", "src");
    //     if (propertyToUpdate == null)
    //         throw new ArgumentException("Value cannot be null.", "propertyToUpdate");

    //     Type t = obj.GetType();
    //     if(propertyToUpdate.Contains('.'))
    //     {
    //         string childProperty = propertyToUpdate.Split('.')[1];
    //         // return obj.GetNestedType(childProperty).InvokeMember();
    //         GetProperty()
    //     }
    //     else
    //     {
    //         var prop = t.GetProperty(propertyToUpdate);
    //         return prop != null ? prop : null;
    //     }
    // }
}