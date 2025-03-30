IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MCCatalog')
BEGIN
	CREATE DATABASE [MCCatalog];
END
GO
    USE [MCCatalog];
GO

IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
	CREATE TABLE [Product]
	(
	   [ProductID] INT NOT NULL IDENTITY(1,1)
	  ,[Name] NVARCHAR(300) NOT NULL
	  ,[UnitPrice] NUMERIC(6,2) NOT NULL      
	  ,CONSTRAINT [PK_MCCatalog_Product] PRIMARY KEY 
	  (
		[ProductID] ASC
	  )
	);

	CREATE NONCLUSTERED INDEX IX_Product_ProductID   
    ON [Product] (ProductID);   
END

IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='InvoiceData' and xtype='U')
BEGIN
	CREATE TABLE [InvoiceData]
	(
	   [InvoiceID] INT NOT NULL IDENTITY(1,1)
	  ,[TransctionDate] NVARCHAR(300) NOT NULL
	  ,[Total] NUMERIC(6,2) NOT NULL
	  ,[Balance] NUMERIC(6,2) NOT NULL      
	  ,CONSTRAINT [PK_MCCatalog_InvoiceData] PRIMARY KEY 
	  (
		[InvoiceID] ASC
	  )
	);

	CREATE NONCLUSTERED INDEX IX_InvoiceData_InvoiceID   
    ON [InvoiceData] (InvoiceID);   
END

IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='InvoiceProductData' and xtype='U')
BEGIN
	CREATE TABLE [InvoiceProductData]
	(
	   [InvoiceProductID] INT NOT NULL IDENTITY(1,1)
	  ,[ProductID] INT NOT NULL
	  ,[Quantity] INT NOT NULL
	  ,[UnitPrice] NUMERIC(6,2) NOT NULL
	  ,[Discount] NUMERIC(6,2) NOT NULL
	  ,[InvoiceID] INT NOT NULL
	  ,CONSTRAINT [PK_MCCatalog_InvoiceProductData] PRIMARY KEY 
	  (
		[InvoiceProductID] ASC
	  )
	  ,CONSTRAINT [FK_MCCatalog_InvoiceProductData_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product]([ProductID])
	   ON UPDATE CASCADE ON DELETE CASCADE
	  ,CONSTRAINT [FK_MCCatalog_InvoiceProductData_InvoiceID] FOREIGN KEY ([InvoiceID]) REFERENCES [InvoiceData]([InvoiceID])
	   ON UPDATE CASCADE ON DELETE CASCADE
	);

	CREATE NONCLUSTERED INDEX IX_InvoiceProductData_InvoiceProductID   
    ON [InvoiceProductData] (InvoiceProductID);   
END