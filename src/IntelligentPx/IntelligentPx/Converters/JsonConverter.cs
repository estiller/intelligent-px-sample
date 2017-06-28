using System;
using System.Globalization;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace IntelligentPx.Converters
{
    public class JsonValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}