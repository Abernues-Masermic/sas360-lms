using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace sas360_test
{

    public class FormatToInfoConverter : IValueConverter
    {
        public const double CSV_NO_DEFINED_FORMAT = 512;
        public const double CSV_UTC_FORMAT = 255;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double)value == CSV_NO_DEFINED_FORMAT)
                return "NO DEFINED";

            else if ((double)value == CSV_UTC_FORMAT)
                return "UTC";

            else
                return value.ToString()!;

        }
        public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
        {
            if ((string)value == "NO DEFINED")
                return CSV_NO_DEFINED_FORMAT;

            else if ((string)value == "UTC")
                return CSV_UTC_FORMAT;

            else
                return double.Parse((string)value);

        }
    }

    public class IntToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i_value = (int)value;
            return $"0X{i_value:X8}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse((string)value);
        }
    }

    public class UIntToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint ui_value = (uint)value;
            return $"0X{ui_value:X8}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return uint.Parse((string)value);
        }
    }

    public class MetricaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            short short_value = (short)value;
            return decimal.Divide(short_value, 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal d_value = (decimal)value;
            return decimal.Multiply(d_value, 100);
        }
    }

    public class ValuesToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brushes = Brushes.Black;
            if (values[0] != null && values[1] != null)
            {
                string value1 = values[0].ToString()!;
                string value2 = values[1].ToString()!;
                brushes = value1 != value2 ? Brushes.Red : Brushes.Black;
            }

            return brushes;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
