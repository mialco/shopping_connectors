USE [irosepetals]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[spBLL_FeedsExport_Mialco_Amazon_20210412]
		@feedName = N'Amazon-feed',
		@storeId = 33,
		@categories = N'128',
		@ParentCategory = NULL,
		@price = 12,
		@brand = N'amore Tees',
		@manufacturer = N'amore Tees',
		@itemType = N'Generic Item',
		@quantity = 123

SELECT	'Return Value' = @return_value

GO
