/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : DashBoardView.xaml.cs
 * Description     : Code-behind file for the DashBoardView UserControl. This class handles 
 *                   interactions with the dashboard, including navigation to the Stock and 
 *                   Order views based on button clicks. It serves as the entry point for 
 *                   navigating to different sections within the Inventory System.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
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

namespace InventorySystemSoftware1.Views
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashBoardView"/> class.
        /// </summary>
        public DashBoardView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the Stock button click event.
        /// Navigates to the Stock view when the button is clicked.
        /// </summary>
        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            // Load the Stock view in the main window
            ((MainWindow)Application.Current.MainWindow).LoadStockView();
        }

        /// <summary>
        /// Event handler for the Order button click event.
        /// Navigates to the Order view when the button is clicked.
        /// </summary>
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            // Load the Order view in the main window
            ((MainWindow)Application.Current.MainWindow).LoadOrderView();
        }
    }
}
