/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : IOrderRepo.cs
 * Description     : Interface for managing order operations in the inventory system.
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

namespace InventorySystemSoftware1.Repos
{
    /// <summary>
    /// Interface for managing orders in the inventory system.
    /// Provides methods to add, delete, and validate orders and to check stock availability.
    /// </summary>
    public interface IOrderRepo
    {
        /// <summary>
        /// Adds a new order to the database.
        /// </summary>
        /// <param name="order">The order to be added.</param>
        void AddOrder(Order order);

        /// <summary>
        /// Deletes an existing order from the database.
        /// </summary>
        /// <param name="order">The order to be deleted.</param>
        void DeleteOrder(Order order);

        /// <summary>
        /// Places multiple orders.
        /// </summary>
        /// <param name="orders">The collection of orders to be placed.</param>
        void PlaceOrder(ObservableCollection<Order> orders); // Change this to accept a List<Order>

        /// <summary>
        /// Validates if there is enough stock available for the specified stock code.
        /// </summary>
        /// <param name="stockCode">The stock code to validate.</param>
        /// <param name="requiredQuantity">The quantity required.</param>
        /// <returns>True if sufficient stock is available; otherwise, false.</returns>
        bool ValidateStockAvailability(string stockCode, int requiredQuantity);

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>An ObservableCollection of all orders.</returns>
        ObservableCollection<Order> GetAllOrders();

        /// <summary>
        /// Updates the stock quantity for a specific stock item.
        /// </summary>
        /// <param name="stockCode">The stock code of the item.</param>
        /// <param name="quantity">The quantity to update.</param>
        void UpdateStockQuantity(string stockCode, int quantity);
    }
}