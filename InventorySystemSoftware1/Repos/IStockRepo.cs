/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : IStockRepo.cs
 * Description     : Interface for managing stock operations in the inventory system.
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
    /// Interface for managing stock in the inventory system.
    /// Provides methods to retrieve, add, update, and delete stock items.
    /// </summary>
    public interface IStockRepo
    {
        /// <summary>
        /// Retrieves all stocks from the database.
        /// </summary>
        /// <returns>An ObservableCollection of all stocks.</returns>
        ObservableCollection<Stock> GetAllStocks();

        /// <summary>
        /// Adds a new stock item to the database.
        /// </summary>
        /// <param name="stock">The stock item to be added.</param>
        void AddStock(Stock stock);

        /// <summary>
        /// Updates an existing stock item in the database.
        /// </summary>
        /// <param name="stock">The stock item to be updated.</param>
        void UpdateStock(Stock stock);

        /// <summary>
        /// Deletes a stock item from the database.
        /// </summary>
        /// <param name="stock">The stock item to be deleted.</param>
        void DeleteStock(Stock stock);

        /// <summary>
        /// Retrieves a stock item by its stock code.
        /// </summary>
        /// <param name="SelectedStockCode">The stock code of the item to retrieve.</param>
        /// <returns>The stock item matching the specified stock code, or null if not found.</returns>
        Stock GetStockByCode(string SelectedStockCode);

        /// <summary>
        /// Retrieves all unique stock codes from the database.
        /// </summary>
        /// <returns>An ObservableCollection of all stock codes.</returns>
        ObservableCollection<string> GetAllStockCodes();
    }
}