/**************************************************************************************************
 * Project       : Inventory System Software
 * File          : OrderView.xaml.cs
 * Description   : Logic for the OrderView UserControl, binding to the OrderViewModel for 
 *                 handling order-related functionality.
 * Author        : Aaryaka P Nath
 * Created       : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventorySystemSoftware1.ViewModels;

namespace InventorySystemSoftware1.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// This UserControl represents the view for placing orders in the inventory system.
    /// </summary>
    public partial class OrderView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderView"/> class.
        /// Sets the DataContext to the singleton instance of the OrderViewModel.
        /// </summary>
        public OrderView()
        {
            InitializeComponent();
            // Set the DataContext to the OrderViewModel for data binding.
            DataContext = OrderViewModel.Instance;
        }
    }
}