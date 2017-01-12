using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Sansagol.ArithmeticMaster
{
    class TextOutputConverter : IMultiValueConverter
    {
        private static int _Accuracy = 0;
        internal static int Accuracy { get { return _Accuracy; } set { _Accuracy = value; } }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() < 3)
                return DependencyProperty.UnsetValue;

            decimal[] arr = new decimal[values.Count()];

            try
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    arr[i] = (decimal)values[i];
                }
            }
            catch (Exception ex)
            {
            }

            OperationType type = values[2] as OperationType;
            if (type == null)
                return DependencyProperty.UnsetValue;

            StringBuilder resValue = new StringBuilder();

            resValue.Append(string.Format("{0:F" + Accuracy + "}", decimal.Round(arr[0], Accuracy)));
            resValue.Append(" " + type.GetSymbol() + " ");
            resValue.Append(string.Format("{0:F" + Accuracy + "}", decimal.Round(arr[1], Accuracy)));
            return resValue.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class BoolInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    class EnableOpacityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? val = value as bool?;
            if (val.HasValue)
            {
                if (val.Value)
                    return (double)1;
            }
            return (double)0.3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
