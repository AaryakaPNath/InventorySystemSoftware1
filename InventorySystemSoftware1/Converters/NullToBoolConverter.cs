/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : NullToBoolConverter.cs
 * Description     : Converts a value to a boolean, returning true if the value is not null and false if it is.
 *                   This converter is useful for controlling UI element states based on null checks.
 *
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InventorySystemSoftware1.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
