using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace ClaseBase
{
    public class ConversorDeEstados : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int num = int.Parse(value.ToString());

                if (num == 0)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else
                {
                    if ((num > 0) && (num <= 30))
                    {
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#FE8177");
                    }
                    else
                    {
                        if ((num > 30) && (num <= 60))
                        {
                            return (SolidColorBrush)new BrushConverter().ConvertFrom("#FF6558");
                        }
                        else
                        {
                            return (SolidColorBrush)new BrushConverter().ConvertFrom("#D61505");
                        }
                    }
                }
            }
            return new SolidColorBrush(Colors.Transparent); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
