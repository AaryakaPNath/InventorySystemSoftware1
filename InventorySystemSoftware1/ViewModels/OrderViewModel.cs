/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : OrderViewModel.cs
 * Description     : ViewModel for managing order-related data and interactions in the inventory system.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Input;
using InventorySystemSoftware1.Memory;
using InventorySystemSoftware1.Models;
using InventorySystemSoftware1.Repos;

namespace InventorySystemSoftware1.ViewModels
{
    /// <summary>
    /// ViewModel for managing orders in the inventory system.
    /// Provides commands to add, delete, and place orders.
    /// </summary>
    public class OrderViewModel : ViewModelBase
    {
        private IOrderRepo _orderRepo;
        private IStockRepo _stockRepo;
        private Order _selectedOrder;
        private string _selectedStockCode;
        private int _orderQuantity;
        private decimal _totalOrderAmount;

        private ObservableCollection<Order> _orders;
        /// <summary>
        /// Gets or sets the collection of orders.
        /// </summary>
        public ObservableCollection<Order> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private ObservableCollection<string> stockCodes;
        /// <summary>
        /// Gets or sets the collection of stock codes available for selection.
        /// </summary>
        public ObservableCollection<string> StockCodes
        {
            get
            {
                return stockCodes;
            }
            set
            {
                stockCodes = value;
                OnPropertyChanged(nameof(StockCodes));
            }
        }

        private Order newOrder;
        /// <summary>
        /// Gets or sets the new order being created.
        /// </summary>
        public Order NewOrder
        {
            get { return newOrder; }
            set
            {
                newOrder = value;
                OnPropertyChanged(nameof(NewOrder));
            }
        }

        private static OrderViewModel _instance;

        /// <summary>
        /// Singleton instance of the OrderViewModel.
        /// </summary>
        public static OrderViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderViewModel();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Loads the stock codes from the stock repository.
        /// </summary>
        public void LoadStockCodes()
        {
            try
            {
                //_stockRepo = StockSQLRepo.Instance;
                //StockCodes.Clear();
                StockCodes = _stockRepo.GetAllStockCodes();
                //foreach (string stokeCode in stokeCodes)
                //{
                //    StockCodes.Add(stokeCode);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ ex.Message}");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderViewModel"/> class.
        /// </summary>
        private OrderViewModel()
        {
            _orderRepo = OrderSQLRepo.Instance;
            _stockRepo = StockSQLRepo.Instance;
            Orders = _orderRepo.GetAllOrders();
            Orders = new ObservableCollection<Order>();
            StockCodes = new ObservableCollection<string>(_stockRepo.GetAllStockCodes());
            // Assuming stock codes are loaded from stock repo

            AddOrderCommand = new RelayCommand(AddOrder);
            DeleteOrderCommand = new RelayCommand(DeleteOrder, CanDeleteOrder);
            PlaceOrderCommand = new RelayCommand(PlaceOrder);
            UpdateTotalOrderAmount();
        }

        /// <summary>
        /// Gets or sets the selected stock code for the order.
        /// </summary>
        public string SelectedStockCode
        {
            get => _selectedStockCode;
            set
            {
                _selectedStockCode = value;
                OnPropertyChanged(nameof(SelectedStockCode));
            }
        }

        /// <summary>
        /// Gets or sets the quantity of the order.
        /// </summary>
        public int OrderQuantity
        {
            get => _orderQuantity;
            set
            {
                _orderQuantity = value;
                OnPropertyChanged(nameof(OrderQuantity));
            }
        }

        /// <summary>
        /// Gets or sets the total amount for the order.
        /// </summary>
        public decimal TotalOrderAmount
        {
            get => _totalOrderAmount;
            set
            {
                _totalOrderAmount = value;
                OnPropertyChanged(nameof(TotalOrderAmount));
            }
        }

        /// <summary>
        /// Gets or sets the currently selected order for deletion.
        /// </summary>
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        // Commands
        /// <summary>
        /// Command for adding a new order.
        /// </summary>
        public ICommand AddOrderCommand { get; }

        /// <summary>
        /// Command for deleting an existing order.
        /// </summary>
        public ICommand DeleteOrderCommand { get; }

        /// <summary>
        /// Command for placing orders.
        /// </summary>
        public ICommand PlaceOrderCommand { get; }

        /// <summary>
        /// Adds a new order based on the selected stock code and quantity.
        /// </summary>
        private void AddOrder()
        {
            if (!int.TryParse(OrderQuantity.ToString(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for the order quantity.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!_orderRepo.ValidateStockAvailability(SelectedStockCode, OrderQuantity))
            {
                MessageBox.Show($"Stock limit exceeded");
                return;
            }

            var stock = _stockRepo.GetStockByCode(SelectedStockCode);
            var totalPrice = stock.UnitPrice * OrderQuantity;
            var newOrder = new Order
            {
                StockCode = stock.StockCode,
                StockName = stock.StockName,
                Quantity = OrderQuantity,
                TotalPrice = totalPrice,
                UnitPrice = stock.UnitPrice,
            };
            Orders.Add(newOrder);

            UpdateTotalOrderAmount();
        }

        //private bool CanAddOrder()
        //{
        //    return !string.IsNullOrEmpty(SelectedStockCode) && OrderQuantity > 0;
        //}

        /// <summary>
        /// Deletes the currently selected order.
        /// </summary>
        private void DeleteOrder()
        {
            if (SelectedOrder != null)
            {
                _orderRepo.DeleteOrder(SelectedOrder);
                //Orders.Remove(SelectedOrder);
                UpdateTotalOrderAmount();
            }
            Reset();
        }

        /// <summary>
        /// Determines whether an order can be deleted.
        /// </summary>
        /// <returns>True if an order can be deleted; otherwise, false.</returns>
        private bool CanDeleteOrder()
        {
            return SelectedOrder != null;
        }

        /// <summary>
        /// Resets the selected stock code and order quantity.
        /// </summary>
        public void Reset()
        {
            SelectedStockCode = null;
            OrderQuantity = 0;
        }

        /// <summary>
        /// Places the current orders and clears the order list.
        /// </summary>
        private void PlaceOrder()
        {
            _orderRepo.PlaceOrder(Orders);
            Orders.Clear(); // Clear order list after placing the order

            //StockViewModel.Instance.LoadStocks();

            UpdateTotalOrderAmount();
            Reset();
            MessageBox.Show($"Order placed successfully");
        }

        /// <summary>
        /// Updates the total order amount based on the current orders.
        /// </summary>
        private void UpdateTotalOrderAmount()
        {
            //TotalOrderAmount = Orders.Sum(o => o.TotalPrice);
           
            decimal totalAmount = 0; // Initialize a variable to hold the total amount

            // Iterate through each order in the Orders collection
            foreach (var order in Orders)
            {
                totalAmount += order.TotalPrice; // Add each order's total price to totalAmount
            }

            TotalOrderAmount = totalAmount; // Update the TotalOrderAmount property
        }
    }
}
