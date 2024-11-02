CREATE DATABASE CafeShopManagement
GO

USE CafeShopManagement
GO

--Create Table
CREATE TABLE Accounts
(
	username NVARCHAR(100) PRIMARY KEY,	
	password NVARCHAR(1000) NOT NULL DEFAULT 0,
	fullname NVARCHAR(100) NOT NULL,
	phone_number VARCHAR(20) NOT NULL,
	email NVARCHAR(100) NOT NULL,
	role_id INT NOT NULL
)
GO

CREATE TABLE Roles
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100)
)
GO

CREATE TABLE Orders
(
	id INT IDENTITY PRIMARY KEY,
	order_date DATE NOT NULL DEFAULT GETDATE(),
	note NVARCHAR(1000),
	buyer_name NVARCHAR(100),
	address NVARCHAR(300),
	discount FLOAT NOT NULL DEFAULT 0,
	total_cost FLOAT NOT NULL DEFAULT 0,
	money_received FLOAT NOT NULL DEFAULT 0,
	status_id INT NOT NULL DEFAULT 1,
	shipping_method_id INT NOT NULL DEFAULT 1,
	payment_method_id INT NOT NULL DEFAULT 1,
)
GO

CREATE TABLE Status
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE ShippingMethod
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE PaymentMethod
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE Products
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	price FLOAT NOT NULL,
	thumbnail NVARCHAR(1000),
	description NVARCHAR(1000),
	category_id INT NOT NULL,
	on_sale INT NOT NULL DEFAULT 1
)
GO

CREATE TABLE Categories
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE OrderInformations
(
	id INT IDENTITY PRIMARY KEY,
	order_id INT NOT NULL,
	product_id INT NOT NULL,
	count INT NOT NULL
)
GO

CREATE TABLE Receipts
(
	id INT IDENTITY PRIMARY KEY,
	recipitent_name NVARCHAR(100) NOT NULL,
	supplier_name NVARCHAR(100) NOT NULL,
	receipt_date DATE NOT NULL DEFAULT GETDATE(),
	confirm INT NOT NULL DEFAULT 0
)
GO

CREATE TABLE Inventories
(
	id INT IDENTITY PRIMARY KEY,
	writer_name NVARCHAR(100) NOT NULL,
	writing_date DATE NOT NULL DEFAULT GETDATE()
)
GO

CREATE TABLE Goods
(
	id INT IDENTITY PRIMARY KEY,
	good_name NVARCHAR(1000) NOT NULL,
	price FLOAT NOT NULL,
	type INT NOT NULL,
	is_active INT DEFAULT 1
)
GO

CREATE TABLE ReceiptInformations
(
	id INT IDENTITY PRIMARY KEY,
	receipt_id INT NOT NULL,
	good_id INT NOT NULL,
	count FLOAT NOT NULL
)
GO

CREATE TABLE InventoryInformations
(
	id INT IDENTITY PRIMARY KEY,
	inventory_id INT NOT NULL,
	good_id INT NOT NULL,
	count FLOAT NOT NULL
)
GO

-- Create Foreign key
ALTER TABLE Accounts
ADD CONSTRAINT FK_Accounts_Roles FOREIGN KEY (role_id) REFERENCES dbo.Roles(id)
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Status FOREIGN KEY (status_id) REFERENCES dbo.Status(id)
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_ShippingMethod FOREIGN KEY (shipping_method_id) REFERENCES dbo.ShippingMethod(id)
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_PaymentMethod FOREIGN KEY (payment_method_id) REFERENCES dbo.PaymentMethod(id)
GO

ALTER TABLE Products
ADD CONSTRAINT FK_Products_Categories FOREIGN KEY (category_id) REFERENCES dbo.Categories(id)
GO

ALTER TABLE OrderInformations
ADD CONSTRAINT FK_OrderInfo_Orders FOREIGN KEY (order_id) REFERENCES dbo.Orders(id),
	CONSTRAINT FK_OrderInfo_Products FOREIGN KEY (product_id) REFERENCES dbo.Products(id);
GO

ALTER TABLE ReceiptInformations
ADD CONSTRAINT FK_ReceiptInfo_Receipts FOREIGN KEY (receipt_id) REFERENCES dbo.Receipts(id),
	CONSTRAINT FK_ReceiptInfo_Goods FOREIGN KEY (good_id) REFERENCES dbo.Goods(id);
GO

ALTER TABLE InventoryInformations
ADD CONSTRAINT FK_InventoryInfo_Inventories FOREIGN KEY (inventory_id) REFERENCES dbo.Inventories(id),
	CONSTRAINT FK_InventoryInfo_Goods FOREIGN KEY (good_id) REFERENCES dbo.Goods(id);
GO

-- Insert Table
INSERT INTO dbo.Roles (name)
VALUES
(N'Quản trị viên'),
(N'Nhân viên bán hàng'),
(N'Nhân viên quản lý kho')
GO


INSERT INTO dbo.Accounts (username, password, fullname, phone_number, email, role_id)
VALUES
(N'user123', '12345', N'John', '0912345678' , N'stu715105190@hnue.edu.vn', 1),
(N'user122', '12345', N'Mary', '0912345678' ,N'stu715105190@hnue.edu.vn', 2)
GO

INSERT INTO dbo.Status(name)
VALUES
(N'Đang gọi đồ'),
(N'Đang chờ'),
(N'Đang làm'),
(N'Đã hoàn thành'),
(N'Đã lấy đồ')
GO

INSERT INTO dbo.ShippingMethod(name)
VALUES
(N'Dùng ở quán'),
(N'Mang đi'),
(N'Giao đến')
GO

INSERT INTO dbo.PaymentMethod(name)
VALUES
(N'Tiền mặt'),
(N'Thẻ tín dụng'),
(N'Chuyển khoản')
GO

INSERT INTO dbo.Categories(name)
VALUES
(N'Cafe'),
(N'Trà'),
(N'Sinh tố'),
(N'Nước ép'),
(N'Đồ ăn')
GO

INSERT INTO dbo.Products(name, price, thumbnail, description, category_id)
VALUES
(N'Cafe đen đá', 35000, NULL, N'Siêu đắng và lạnh' , 1),
(N'Cafe đen nóng', 35000, NULL, N'Siêu đắng và nóng' , 1),
(N'Sinh tố cam', 25000, NULL, N'Chua và ngọt' , 3)
GO

INSERT INTO dbo.Goods(good_name, price, type)
VALUES
(N'Cốc nhụa', 500, 1),
(N'Ống hút', 100, 1),
(N'Hạt cafe', 20000, 0)
GO

-- Create Procedure
CREATE PROCEDURE USP_GetAccountByUsername
@username NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.Accounts WHERE username = @username
END
GO

-- DROP PROCEDURE USP_Login
CREATE PROCEDURE USP_Login
@username NVARCHAR(100),
@password NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.Accounts WHERE username = @username AND LOWER(CONVERT(VARCHAR(32), HashBytes('MD5', CONVERT(varchar, password)), 2)) = @password
END
GO

CREATE PROCEDURE USP_InsertOrderInfo
@idOrder INT, @idProduct INT, @count INT
AS
BEGIN
	DECLARE @idOrderInfo INT = 0
	DECLARE @productCount INT = 1

	SELECT @idOrderInfo = id, @productCount = count
	FROM dbo.OrderInformations
	WHERE order_id = @idOrder AND product_id = @idProduct

	IF(@idOrderInfo > 0)
	BEGIN
		DECLARE @newCount INT = @productCount + @count
		UPDATE dbo.OrderInformations SET count = @newCount WHERE order_id = @idOrder AND product_id = @idProduct
	END
	ELSE
	BEGIN
		INSERT INTO dbo.OrderInformations(order_id, product_id, count) VALUES (@idOrder, @idProduct, @count)
	END
END
GO

CREATE PROCEDURE USP_ReduceOrderInfo
@idOrder INT, @idProduct INT
AS
BEGIN
	DECLARE @productCount INT = 1

	SELECT @productCount = count
	FROM dbo.OrderInformations
	WHERE order_id = @idOrder AND product_id = @idProduct

	IF(@productCount > 1)
	BEGIN
		DECLARE @newCount INT = @productCount - 1
		UPDATE dbo.OrderInformations SET count = @newCount WHERE order_id = @idOrder AND product_id = @idProduct
	END
		ELSE
	BEGIN
		DELETE FROM dbo.OrderInformations WHERE order_id = @idOrder AND product_id = @idProduct
	END
END
GO

CREATE PROCEDURE USP_InsertReceiptInfo
@idReceipt INT, @idGood INT, @count FLOAT
AS
BEGIN
	DECLARE @idReceiptInfo INT = 0
	DECLARE @goodCount FLOAT = 1

	SELECT @idReceiptInfo = id, @goodCount = count
	FROM dbo.ReceiptInformations
	WHERE receipt_id = @idReceipt AND good_id = @idGood

	IF(@idReceiptInfo > 0)
	BEGIN
		DECLARE @newCount FLOAT = @goodCount + @count
		UPDATE dbo.ReceiptInformations SET count = @newCount WHERE receipt_id = @idReceipt AND good_id = @idGood
	END
	ELSE
	BEGIN
		INSERT INTO dbo.ReceiptInformations(receipt_id, good_id, count) VALUES (@idReceipt, @idGood, @count)
	END
END
GO

CREATE PROCEDURE USP_ReduceReceiptInfo
@idReceipt INT, @idGood INT
AS
BEGIN
	DECLARE @productCount FLOAT = 1

	SELECT @productCount = count
	FROM dbo.ReceiptInformations
	WHERE receipt_id = @idReceipt AND good_id = @idGood

	IF(@productCount > 1)
	BEGIN
		DECLARE @newCount FLOAT = @productCount - 1
		UPDATE dbo.ReceiptInformations SET count = @newCount WHERE receipt_id = @idReceipt AND good_id = @idGood
	END
		ELSE
	BEGIN
		DELETE FROM dbo.ReceiptInformations WHERE receipt_id = @idReceipt AND good_id = @idGood
	END
END
GO

CREATE PROCEDURE USP_InsertInventoryInfo
@idInventory INT, @idGood INT, @count FLOAT
AS
BEGIN
	DECLARE @idInventoryInfo INT = 0
	DECLARE @goodCount FLOAT = 1

	SELECT @idInventoryInfo = id, @goodCount = count
	FROM dbo.InventoryInformations
	WHERE inventory_id = @idInventory AND good_id = @idGood

	IF(@idInventoryInfo > 0)
	BEGIN
		DECLARE @newCount FLOAT = @goodCount + @count
		UPDATE dbo.InventoryInformations SET count = @newCount WHERE inventory_id = @idInventory AND good_id = @idGood
	END
	ELSE
	BEGIN
		INSERT INTO dbo.InventoryInformations(inventory_id, good_id, count) VALUES (@idInventory, @idGood, @count)
	END
END
GO

CREATE PROCEDURE USP_ReduceInventoryInfo
@idInventory INT, @idGood INT
AS
BEGIN
	DECLARE @productCount FLOAT = 1

	SELECT @productCount = count
	FROM dbo.InventoryInformations
	WHERE inventory_id = @idInventory AND good_id = @idGood

	IF(@productCount > 1)
	BEGIN
		DECLARE @newCount FLOAT = @productCount - 1
		UPDATE dbo.InventoryInformations SET count = @newCount WHERE inventory_id = @idInventory AND good_id = @idGood
	END
		ELSE
	BEGIN
		DELETE FROM dbo.InventoryInformations WHERE inventory_id = @idInventory AND good_id = @idGood
	END
END
GO