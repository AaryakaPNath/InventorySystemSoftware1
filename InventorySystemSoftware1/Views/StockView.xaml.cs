/**************************************************************************************************
 * Project       : Inventory System Software
 * File          : StockView.xaml.cs
 * Description   : Logic for the StockView UserControl, binding to the StockViewModel for 
 *                 handling stock-related functionality.
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
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView : UserControl
    {
        // Constructor for StockView
        public StockView()
        {
            InitializeComponent(); // Initialize the UI components
            DataContext = StockViewModel.Instance; // Set the DataContext to the StockViewModel instance
        }
    }
}