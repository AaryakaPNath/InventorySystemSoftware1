/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : StockSQLRepo.cs
 * Description     : Manages stock items by performing CRUD operations, validating stock data, 
 *                   and updating stock levels to reflect changes after orders are placed.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;
using InventorySystemSoftware1.Models;
using InventorySystemSoftware1.Repos;
namespace InventorySystemSoftware1.Memory
{
    public class StockSQLRepo : IStockRepo
    {
        // Singleton instance
        private static StockSQLRepo _instance;

        // Database connection string
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Inventory_Db;Integrated Security=True;";

        // Singleton pattern for the repository
        public static StockSQLRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockSQLRepo();
                }
                return _instance;
            }
        }

        // Private constructor to enforce singleton pattern
        private StockSQLRepo() { }

        /// <summary>
        /// Retrieves all stocks from the database.
        /// </summary>
        /// <returns>ObservableCollection of Stock objects.</returns>
        public ObservableCollection<Stock> GetAllStocks()
        {
            ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Stocks";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Stock stock = new Stock
                        {
                            StockCode = (string)reader["StockCode"],
                            StockName = (string)reader["StockName"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            StockCount = (int)reader["StockCount"]
                        };
                        stocks.Add(stock);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading stocks from the database", ex);
            }
            return stocks;
        }

        /// <summary>
        /// Adds a new stock to the database.
        /// </summary>
        /// <param name="stock">The stock object to add.</param>
        public void AddStock(Stock stock)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Stocks (StockCode, StockName, UnitPrice, StockCount) VALUES (@StockCode, @StockName, @UnitPrice, @StockCount)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    cmd.Parameters.AddWithValue("@StockName", stock.StockName);
                    cmd.Parameters.AddWithValue("@UnitPrice", stock.UnitPrice);
                    cmd.Parameters.AddWithValue("@StockCount", stock.StockCount);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding new stock to the database", ex);
            }
        }

        /// <summary>
        /// Updates an existing stock in the database.
        /// </summary>
        /// <param name="stock">The stock object to update.</param>
        public void UpdateStock(Stock stock)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Stocks SET StockName = @StockName, UnitPrice = @UnitPrice, StockCount = @StockCount WHERE StockCode = @StockCode";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    cmd.Parameters.AddWithValue("@StockName", stock.StockName);
                    cmd.Parameters.AddWithValue("@UnitPrice", stock.UnitPrice);
                    cmd.Parameters.AddWithValue("@StockCount", stock.StockCount);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Stock item not found for update.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating stock in the database", ex);
            }
        }

        /// <summary>
        /// Deletes a stock from the database.
        /// </summary>
        /// <param name="stock">The stock object to delete.</param>
        public void DeleteStock(Stock stock)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Stocks WHERE StockCode = @StockCode";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockCode", stock.StockCode);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Stock item not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting stock from the database", ex);
            }
        }

        /// <summary>
        /// Retrieves a stock by its stock code.
        /// </summary>
        /// <param name="selectedStockCode">The stock code of the stock to retrieve.</param>
        /// <returns>A Stock object if found; otherwise, null.</returns>
        public Stock GetStockByCode(string selectedStockCode)
        {
            Stock stock = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Stocks WHERE StockCode = @StockCode";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockCode", selectedStockCode);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        stock = new Stock
                        {
                            StockCode = (string)reader["StockCode"],
                            StockName = (string)reader["StockName"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            StockCount = (int)reader["StockCount"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading stock from the database", ex);
            }
            return stock;
        }

        /// <summary>
        /// Retrieves all unique stock codes from the database.
        /// </summary>
        /// <returns>ObservableCollection of stock codes as strings.</returns>
        public ObservableCollection<string> GetAllStockCodes()
        {
            ObservableCollection<string> stockCodes = new ObservableCollection<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT DISTINCT StockCode FROM Stocks";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        stockCodes.Add((string)reader["StockCode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading stock codes from the database", ex);
            }
            return stockCodes;
        }
    }
}