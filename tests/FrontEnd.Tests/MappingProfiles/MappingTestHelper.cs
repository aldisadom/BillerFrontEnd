using AwesomeAssertions;
using System.Reflection;

namespace FrontEnd.Tests.MappingProfiles;

/// <summary>
/// Checks that a mapping set every target property from the source property with the same name,
/// like Biller's MappingTestHelper with MapStyle.MappedAllTo.
/// </summary>
public static class MappingTestHelper
{
    public static void TestMappedAllTo<TFrom, TTo>(TFrom from, TTo to, HashSet<string>? ignoreProps = null)
    {
        to.Should().NotBeNull();

        var fromProps = typeof(TFrom).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var toProps = typeof(TTo).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var toProp in toProps)
        {
            if (ignoreProps != null && ignoreProps.Contains(toProp.Name))
                continue;

            var fromProp = fromProps.FirstOrDefault(p => p.Name == toProp.Name && p.PropertyType == toProp.PropertyType);
            fromProp.Should().NotBeNull($"Property '{toProp.Name}' should exist in source type.");

            var fromValue = fromProp!.GetValue(from);
            var toValue = toProp.GetValue(to);

            IsDefault(fromValue).Should().BeFalse($"Property '{toProp.Name}' in source should not be default.");
            IsDefault(toValue).Should().BeFalse($"Property '{toProp.Name}' in target should not be default.");
            toValue.Should().BeEquivalentTo(fromValue, $"Property '{toProp.Name}' should have equivalent value.");
        }
    }

    private static bool IsDefault(object? value)
    {
        if (value == null)
            return true;

        var type = value.GetType();
        if (type.IsValueType)
            return value.Equals(Activator.CreateInstance(type));

        if (type == typeof(string))
            return string.IsNullOrEmpty((string)value);

        return false;
    }
}
