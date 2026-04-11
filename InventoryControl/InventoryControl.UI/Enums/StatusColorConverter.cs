using System.Globalization;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI.Enums;

public class StatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "OK"  => Colors.Green,
            "LOW" => Colors.Orange,
            "OUT" => Colors.Red,
            _     => Colors.Gray
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
