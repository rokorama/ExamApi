using System.Reflection;

namespace ExamApi.BusinessLogic.Helpers;

public class PropertyChanger : IPropertyChanger
{
    public object UpdateProperty<T>(object obj, string propertyToUpdate, T newValue)
    {
    // //find out the type
    Type type = obj.GetType();

    // //get the property information based on the type
    // System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyToUpdate)!;
    dynamic propertyInfo = type.GetProperty(propertyToUpdate)!;

    // //find the property type
    Type propertyType = propertyInfo.PropertyType;

    // //Convert.ChangeType does not handle conversion to nullable types
    // //if the property type is nullable, we need to get the underlying type of the property
    var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

    // //Returns an System.Object with the specified System.Type and whose value is
    // //equivalent to the specified object.
    // newValue = Convert.ChangeType(newValue, targetType!);

    // //Set the value of the property
    propertyInfo.SetValue(obj, (T)newValue);
       
        // Type t = obj.GetType();
        // Type typeOfNewValue = newValue!.GetType();
        // PropertyInfo? targetProperty = t.GetProperty(propertyToUpdate);
        
        // if (targetProperty is null)
        //     throw new ArgumentException();
        // typeOfNewValue = Nullable.GetUnderlyingType(t)!;
        // targetProperty!.SetValue(obj, newValue);

    return obj;
    }

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }
}