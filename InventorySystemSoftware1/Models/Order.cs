/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : Order.cs
 * Description     : Represents an order in the inventory system, encapsulating details such 
 *                   as stock code, stock name, quantity ordered, unit price, and total price.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-25]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemSoftware1.Models
{
    /// <summary>
    /// Represents an order in the inventory system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique code of the stock item.
        /// </summary>
        public string StockCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock item.
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the stock item ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the stock item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the total price for the order (calculated as Quantity * UnitPrice).
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}