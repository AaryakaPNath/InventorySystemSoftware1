/**************************************************************************************************
 * Project Name    : Inventory System Software
 * File Name       : OrderSQLRepo.cs
 * Description     : Manages orders and stock levels by validating stock availability, retrieving all orders, and updating stock quantities after each order.
 * Author          : Aaryaka P Nath
 * Date Created    : [2024-10-27]
 **************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InventorySystemSoftware1.Models;
using InventorySystemSoftware1.Repos;

namespace InventorySystemSoftware1.Memory
{
    /// <summary>
    /// Repository for managing orders in the database, implementing CRUD operations and stock validation.
    /// </summary>
    public class OrderSQLRepo : IOrderRepo
    {
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Inventory_Db;Integrated Security=True;";
        private static OrderSQLRepo _instance;

        /// <summary>
        /// Singleton instance of the OrderSQLRepo.
        /// </summary>
        public static OrderSQLRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderSQLRepo();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Adds a new order to the Orders table.
        /// </summary>
        public void AddOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Orders (StockCode, StockName, Quantity, UnitPrice, TotalPrice) VALUES (@StockCode, @StockName, @Quantity, @UnitPrice, @TotalPrice)", connection);
                    command.Parameters.AddWithValue("@StockCode", order.StockCode);
                    command.Parameters.AddWithValue("@StockName", order.StockName);
                    command.Parameters.AddWithValue("@Quantity", order.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Deletes an order from the Orders table based on stock code, quantity, and total price.
        /// </summary>
        public void DeleteOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Orders WHERE StockCode = @StockCode AND Quantity = @Quantity AND TotalPrice = @TotalPrice", connection);
                command.Parameters.AddWithValue("@StockCode", order.StockCode);
                command.Parameters.AddWithValue("@StockName", order.StockName);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Places an order by validating stock and updating the Orders and Stocks tables.
        /// </summary>
        public void PlaceOrder(ObservableCollection<Order> orders)
        {
            foreach (var order in orders)
            {
                try
                {
                    if (!ValidateStockAvailability(order.StockCode, order.Quantity))
                    {
                        Console.WriteLine($"Not enough stock for {order.StockCode}. Order not placed.");
                        continue;
                    }
                    UpdateStockQuantity(order.StockCode, order.Quantity);
                    AddOrder(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error placing order for {order.StockCode}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Validates if sufficient stock is available for an order.
        /// </summary>
        public bool ValidateStockAvailability(string stockCode, int requiredQuantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT StockCount FROM Stocks WHERE StockCode = @StockCode", connection);
                command.Parameters.AddWithValue("@StockCode", stockCode);
                var stockCount = (int)command.ExecuteScalar();
                return stockCount >= requiredQuantity;
            }
        }

        /// <summary>
        /// Retrieves all orders from the Orders table.
        /// </summary>
        public ObservableCollection<Order> GetAllOrders()
        {
            var orders = new ObservableCollection<Order>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT StockCode, StockName, Quantity, UnitPrice, TotalPrice FROM Orders", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            StockCode = reader["StockCode"].ToString(),
                            StockName = reader["StockName"].ToString(),
                            Quantity = (int)reader["Quantity"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            TotalPrice = (decimal)reader["TotalPrice"]
                        };
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }

        /// <summary>
        /// Updates stock quantity in the Stocks table after an order is placed.
        /// </summary>
        public void UpdateStockQuantity(string stockCode, int quantity)
        {
            string query = "UPDATE Stocks SET StockCount = StockCount - @Quantity WHERE StockCode = @StockCode";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@StockCode", stockCode);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Stock item not found for update.");
                    }
                }
            }
        }
    }
}