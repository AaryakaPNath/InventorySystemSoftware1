/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : BoolToVisibililityConverter.cs
 * Description     : Converts boolean values to Visibility for UI element control in WPF, showing elements when true and hiding when false. 
 *                   Supports reverse conversion.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InventorySystemSoftware1.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert boolean to Visibility
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed; // Adjust as needed
            }
            return Visibility.Collapsed; // Default if value is not a boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert Visibility back to boolean
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible; // Return true if Visible
            }
            return false; // Default if value is not a Visibility
        }
    }
}
