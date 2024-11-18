/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : Stocks.cs
 * Description     : Represents a stock item in the inventory system, containing properties 
 *                   such as stock code, stock name, unit price, and stock count to manage 
 *                   inventory levels effectively.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-25]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemSoftware1.Models
{
    /// <summary>
    /// Represents a stock item in the inventory system.
    /// </summary>
    public class Stock : INotifyPropertyChanged
    {
        private string _stockCode;
        private string _stockName;
        private decimal _unitPrice;
        private int _stockCount;

        /// <summary>
        /// Gets or sets the unique code of the stock item.
        /// </summary>
        public string StockCode
        {
            get => _stockCode;
            set
            {
                if (_stockCode != value)
                {
                    _stockCode = value;
                    OnPropertyChanged(nameof(StockCode));
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the stock item.
        /// </summary>
        public string StockName
        {
            get => _stockName;
            set
            {
                if (_stockName != value)
                {
                    _stockName = value;
                    OnPropertyChanged(nameof(StockName));
                }
            }
        }

        /// <summary>
        /// Gets or sets the unit price of the stock item.
        /// </summary>
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }
        }

        /// <summary>
        /// Gets or sets the available count of the stock item.
        /// </summary>
        public int StockCount
        {
            get => _stockCount;
            set
            {
                if (_stockCount != value)
                {
                    _stockCount = value;
                    OnPropertyChanged(nameof(StockCount));
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
