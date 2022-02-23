using EK.CommonUtils.Code;
using System.Reflection;

namespace EK.CommonUtils;

public static partial class ReflectionUtils
{
    public static IReadOnlyList<PropertyInfo> GetPublicInstanceGetters(object instance)
    {
        Guard.NotNull(instance);

        var flags = BindingFlags.Public | BindingFlags.Instance;
        
        return instance.GetType().GetProperties(flags).Where(x => x.CanRead).ToList();
    }

    public static IReadOnlyList<PropertyValueTuple> PublicInstanceGettersToString(object instance)
    {
        Guard.NotNull(instance);

        var result = new List<PropertyValueTuple>();

        var properties = GetPublicInstanceGetters(instance);

        foreach (PropertyInfo property in properties)
        {
            string? propertyValue = null;
            Type? exception = null;

            try
            {
                propertyValue = property.GetValue(instance)?.ToString();
            }
            catch (Exception ex)
            {
                exception = ex.GetType();
            }

            var item = new PropertyValueTuple(property.Name, propertyValue, exception);
            
            result.Add(item);
        }

        return result;
    }

    public record struct PropertyValueTuple(string Property, string? Value, Type? Exception);
}
