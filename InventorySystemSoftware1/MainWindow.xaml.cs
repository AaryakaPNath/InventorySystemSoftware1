/**************************************************************************************************
 * Project       : Inventory System Software
 * File          : MainWindow.xaml.cs
 * Description   : Logic for the main window of the inventory system. It handles navigation to
 *                 different views (Dashboard, Stock, Order) and application exit.
 * Author        : Aaryaka P Nath
 * Created       : [2024-11-01]
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
using InventorySystemSoftware1.Views;

namespace InventorySystemSoftware1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //LoadDashboard(); // Load the dashboard by default
        }

        private void LoadDashboard()
        {
            MainContent.Content = new DashBoardView(); // Set the dashboard UserControl
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboard(); // Load the dashboard UserControl
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Exit the application
        }

        // Method to load StockView
        public void LoadStockView()
        {
            MainContent.Content = new StockView(); // Set the Stock UserControl
        }

        // Method to load OrderView
        public void LoadOrderView()
        {
            MainContent.Content = new OrderView(); // Set the Order UserControl
        }

    }
}

