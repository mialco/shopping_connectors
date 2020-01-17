/****** Script for SelectTopNRows command from SSMS  ******/
use irosepetals
SELECT 
--TOP (1000) 
		--[ID]
     ps.storeId, count (ps.[ProductID])
      --,[CreatedOn]
      -- ,[UpdatedOn]
  FROM [irosepetals].[dbo].[ProductStore] ps
      inner join Product p on ps.productID = p.productID
	  where p.Published= 1
        group by [StoreID]


SELECT 
--TOP (1000) 
		--[ID]
     ps.ProductId, count (ps.[StoreID])
      --,[CreatedOn]
      -- ,[UpdatedOn]
	  
  FROM [irosepetals].[dbo].[ProductStore] ps
      inner join Product p on ps.productID = p.productID
	  where p.Published= 1
	    group by ps.[productID]
