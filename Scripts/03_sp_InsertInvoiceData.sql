use MCCatalog

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_InsertInvoiceData
	@TransctionDate nvarchar(300), 
	@Total numeric(6,2),
	@Balance numeric(6,2)
AS
BEGIN
	SET NOCOUNT ON;

	insert into dbo.InvoiceData(TransctionDate,Total, Balance)
	values(@TransctionDate,@Total, @Balance)

	select top(1) InvoiceID from InvoiceData order by InvoiceID desc
END
GO
