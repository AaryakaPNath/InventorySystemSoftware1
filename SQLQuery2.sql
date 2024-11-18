CREATE TABLE Orders (
    StockCode NVARCHAR(50) NOT NULL,
    StockName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(10, 2) NOT NULL,
    TotalPrice AS (Quantity * UnitPrice),
    FOREIGN KEY (StockCode) REFERENCES Stocks(StockCode)
);
INSERT INTO Orders (StockCode, StockName, Quantity, UnitPrice) VALUES
('A001', 'Apple', 10, 1.00),
('B002', 'Banana', 5, 0.50),
('O003', 'Orange', 8, 0.75),
('G004', 'Grapes', 3, 2.00),
('M005', 'Mango', 4, 1.50);

SELECT * FROM Orders;

DROP TABLE Orders;