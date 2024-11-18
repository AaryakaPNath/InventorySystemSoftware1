/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : ViewModelBase.cs
 * Description     : Base class for view models that implements the INotifyPropertyChanged interface to support data binding in WPF applications.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemSoftware1.ViewModels
{
    /// <summary>
    /// Represents a base class for view models that implements the INotifyPropertyChanged interface.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
