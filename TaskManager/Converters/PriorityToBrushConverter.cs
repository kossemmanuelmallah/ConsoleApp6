using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskManager1.Converters
{
    public class PriorityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = value is int i ? i : 1;
            return p switch
            {
                0 => new SolidColorBrush(Color.FromRgb(0x5C, 0xB8, 0x5C)),
                1 => new SolidColorBrush(Color.FromRgb(0x4A, 0x90, 0xD9)),
                2 => new SolidColorBrush(Color.FromRgb(0xD9, 0x53, 0x4F)),
                _ => Brushes.Gray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
