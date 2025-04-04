USE [MCCatalog]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertInvoiceProduct]    Script Date: 1/8/2025 1:58:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertInvoiceProduct]
	@InvoiceID int, 
	@Product varchar(max),
	@Quantity int,
	@UnitPrice numeric(6,2),
	@Discount numeric(6,2)
AS
BEGIN
	SET NOCOUNT ON;

	insert into dbo.InvoiceProductData(ProductID,Quantity,UnitPrice,Discount,InvoiceID)
	values((select ProductID from Product where [Name]=@Product),@Quantity,@UnitPrice,@Discount,@InvoiceID)
END
