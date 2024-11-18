CREATE DATABASE Inventory_Db;
USE Inventory_Db;

CREATE TABLE Stocks (
    StockCode NVARCHAR(50) PRIMARY KEY,
    StockName NVARCHAR(100) NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    StockCount INT NOT NULL
);

INSERT INTO Stocks (StockCode, StockName, UnitPrice, StockCount) VALUES
('A001', 'Apple', 1.00, 50),
('B002', 'Banana', 0.50, 100),
('O003', 'Orange', 0.75, 80),
('G004', 'Grapes', 2.00, 40),
('M005', 'Mango', 1.50, 60);

SELECT * FROM Stocks;

DROP TABLE Stocks;

CREATE TABLE Orders (

    StockCode VARCHAR(50) NOT NULL,        
    StockName VARCHAR(100) NOT NULL,    
    Quantity INT NOT NULL,                   
    UnitPrice DECIMAL(10, 2) NOT NULL  
);

INSERT INTO Orders (StockCode, StockName, Quantity, UnitPrice) VALUES
('A001', 'Apple', 10, 1.00),
('B002', 'Banana', 5, 0.50),
('O003', 'Orange', 20, 0.75),
('G004', 'Grapes', 15, 2.00),
('M005', 'Mango', 8, 1.50);

SELECT * FROM Orders;