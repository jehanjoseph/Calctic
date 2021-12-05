using System;
using System.Globalization;
using Xamarin.Forms;

namespace Calctic.Converters
{
    public class LabelLengthMaxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string labelText = value as string;

            if (labelText == null)
                return value;

            const int MaxLength = 15;

            if (labelText.Length > MaxLength)
            {
                return labelText.Substring(0, MaxLength);
            }
            else
            {
                return labelText;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as string;
        }
    }
}
