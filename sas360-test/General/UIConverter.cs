using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace sas360_test
{

    public class FormatToInfoConverter : IValueConverter
    {
        public const double CSV_NO_DEFINED_FORMAT = 512;
        public const double CSV_UTC_FORMAT = 255;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((double)value == CSV_NO_DEFINED_FORMAT)
                return "NO DEFINED";

            else if ((double)value == CSV_UTC_FORMAT)
                return "UTC";

            else
                return value.ToString();

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)value == "NO DEFINED")
                return CSV_NO_DEFINED_FORMAT;

            else if ((string)value == "UTC")
                return CSV_UTC_FORMAT;

            else
                return double.Parse((string)value);

        }
    }

    public class DataToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ushort_value = (int)value;
            return $"0X{ushort_value.ToString("X8")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return int.Parse((string)value);
        }
    }
}
