using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Visualization.Utils.Converters;

public class StringToInt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => 
        value?.ToString();


    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (int.TryParse(value as string, out int intValue))
            return intValue;
        return null;
    }
}