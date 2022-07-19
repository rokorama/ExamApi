using System.Reflection;

namespace ExamApi.BusinessLogic;

public static class PropertyChanger
{
    public static object UpdateProperty<T>(object obj, string propName, T newValue)
    {
        Type t = obj.GetType();
        PropertyInfo props = t.GetProperty(propName);
        props.SetValue(obj, newValue);
        return obj;
    }
}