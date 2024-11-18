/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : StockViewModel.cs
 * Description     : ViewModel for managing stock-related data and interactions in the inventory system.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystemSoftware1.Models;
using InventorySystemSoftware1.Repos;
using System.Windows.Input;
using System.Windows;
using InventorySystemSoftware1.Memory;

namespace InventorySystemSoftware1.ViewModels
{
    /// <summary>
    /// ViewModel for managing stock items in the inventory system.
    /// </summary>
    public class StockViewModel : ViewModelBase
    {
        private IStockRepo _repo = StockSQLRepo.Instance;

        // Properties
        private Stock _selectedStock;
        private bool isCodeTextBoxEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether a stock item is selected.
        /// </summary>
        public bool IsCodeTextBoxEnabled
        {
            get { return isCodeTextBoxEnabled; }
            set
            {
                isCodeTextBoxEnabled = value;
                OnPropertyChanged(nameof(IsCodeTextBoxEnabled));
            }
        }

        /// <summary>
        /// The currently selected stock item for editing or deletion.
        /// </summary>
        public Stock SelectedStock
        {
            get => _selectedStock;
            set
            {
                if (_selectedStock != value)
                {
                    _selectedStock = value;
                    IsCodeTextBoxEnabled = false;
                    OnPropertyChanged(nameof(SelectedStock));

                    // Reset editing state when selection changes
                    //if (IsEditingEnabled)
                    //{
                    //    IsEditingEnabled = false;
                    //}
                }
            }
        }

        /// <summary>
        /// Collection of all stock items.
        /// </summary>
        private ObservableCollection<Stock> stocks;
        public ObservableCollection<Stock> Stocks
        {
            get
            {
                return stocks;
            }
            set
            {
                stocks = value;
                OnPropertyChanged(nameof (Stocks));
            }
        }

      
        private Stock _newStock;

        /// <summary>
        /// Represents a new stock item being added or edited.
        /// </summary>
        public Stock NewStock
        {
            get => _newStock;
            set
            {
                _newStock = value;
                OnPropertyChanged(nameof(NewStock));
            }
        }

        private bool _isEditingEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether the editing mode is enabled.
        /// </summary>
        public bool IsEditingEnabled
        {
            get => _isEditingEnabled;
            set
            {
                _isEditingEnabled = value;
                OnPropertyChanged(nameof(IsEditingEnabled));
            }
        }

        // Commands
        public ICommand AddStockCommand { get; }
        public ICommand SaveStockCommand { get; }
        public ICommand UpdateStockCommand { get; }
        public ICommand DeleteStockCommand { get; }

        private static StockViewModel _instance;

        /// <summary>
        /// Singleton pattern for the StockViewModel instance.
        /// </summary>
        public static StockViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockViewModel();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockViewModel"/> class.
        /// </summary>
        private StockViewModel()
        {
            Stocks = _repo.GetAllStocks();
            AddStockCommand = new RelayCommand(PrepareNewStock);
            SaveStockCommand = new RelayCommand(SaveNewStock);
            UpdateStockCommand = new RelayCommand(EditSelectedStock);
            DeleteStockCommand = new RelayCommand(DeleteStock);
            IsCodeTextBoxEnabled = true;
        }

        // Methods

        /// <summary>
        /// Prepares a new stock item for entry.
        /// </summary>
        private void PrepareNewStock()
        {
            // Prepare a new stock item
            NewStock = new Stock();
            IsEditingEnabled = true; // Show the input area for adding a new stock
        }

        /// <summary>
        /// Loads all stocks into the Stocks collection.
        /// </summary>
        //public void LoadStocks()
        //{
        //    var stocks = _repo.GetAllStocks();
            //Stocks.Clear();
            //foreach (var stock in stocks)
            //{
            //    Stocks.Add(stock);
            //}
        //}

        /// <summary>
        /// Saves the new stock item or updates an existing one.
        /// </summary>
        private void SaveNewStock()
        {
            // Call ValidateStock on Save button click to check for errors
            var validationErrorMessage = ValidateNewStock();

            if (!string.IsNullOrEmpty(validationErrorMessage))
            {
                // If there are validation errors, show the error message
                MessageBox.Show(validationErrorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Exit the method to prevent saving
            }

            if (SelectedStock == null) // Adding new stock
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(NewStock.StockCode) || string.IsNullOrWhiteSpace(NewStock.StockName))
                    {
                        MessageBox.Show("StockCode and StockName cannot be null or empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    _repo.AddStock(NewStock);
                    //Stocks.Add(NewStock);
                    _repo.GetAllStocks();
                    var orderViewModel = OrderViewModel.Instance;
                    orderViewModel.LoadStockCodes();
                    MessageBox.Show("Stock added successfully.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding stock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else // Updating existing stock
            {
                try
                {
                    //SelectedStock.StockCode = NewStock.StockCode;
                    //SelectedStock.StockName = NewStock.StockName;
                    //SelectedStock.UnitPrice = NewStock.UnitPrice;
                    //SelectedStock.StockCount = NewStock.StockCount;

                    // Ensure the update method has the correct parameters
                    _repo.UpdateStock(NewStock);
                    MessageBox.Show("Stock updated successfully.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating stock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            IsEditingEnabled = false; // Hide the input area after saving
            SelectedStock = null; // Clear selection to disable edit mode
        }

        /// <summary>
        /// Loads the selected stock item for editing.
        /// </summary>
        private void EditSelectedStock()
        {
            if (SelectedStock != null)
            {
                NewStock = new Stock
                {
                    StockCode = SelectedStock.StockCode,
                    StockName = SelectedStock.StockName,
                    UnitPrice = SelectedStock.UnitPrice,
                    StockCount = SelectedStock.StockCount
                };
                IsEditingEnabled = true; // Show the input area for editing
            }
            else
            {
                MessageBox.Show("Please select a stock item to edit.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Deletes the selected stock item.
        /// </summary>
        private void DeleteStock()
        {
            if (SelectedStock == null)
            {
                MessageBox.Show("Please select a stock item to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {SelectedStock.StockName}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                _repo.DeleteStock(SelectedStock);
                Stocks = _repo.GetAllStocks();
                var orderViewModel = OrderViewModel.Instance;
                orderViewModel.LoadStockCodes();

                SelectedStock = null; // Clear selection
                MessageBox.Show("Stock deleted successfully.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validates the new stock item.
        /// </summary>
        /// <returns>A validation error message if validation fails; otherwise, an empty string.</returns>
        private string ValidateNewStock()
        {
            // Initialize an empty error message
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(NewStock?.StockCode))
            {
                errorMessage += "Stock Code cannot be empty.\n";
            }

            if (string.IsNullOrWhiteSpace(NewStock?.StockName))
            {
                errorMessage += "Stock Name cannot be empty.\n";
            }

            if (!decimal.TryParse(NewStock?.UnitPrice.ToString(), out decimal unitPrice) || unitPrice <= 0)
            {
                errorMessage += "Unit Price must be a positive number.\n";
            }

            if (!int.TryParse(NewStock?.StockCount.ToString(), out int stockCount) || stockCount <= 0)
            {
                errorMessage += "Stock Count must be a positive number.\n";
            }

            return errorMessage; // Return the error message
        }
    }
}