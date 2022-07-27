using System.Reflection;

namespace ExamApi.BusinessLogic.Helpers;

public class PropertyChanger : IPropertyChanger
{
    public object UpdateProperty<T>(object obj, string propertyToUpdate, T newValue)
    {
    // Find out the type
    Type type = obj.GetType();

    // Get the property information based on the type
    PropertyInfo propertyInfo = type.GetProperty(propertyToUpdate)!;

    // Find and get the property type
    Type propertyType = propertyInfo.PropertyType;

    var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

    // Set the value of the property
    propertyInfo.SetValue(obj, (T)newValue);

    return obj;
    }

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }
}