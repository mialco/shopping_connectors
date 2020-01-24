/****** Script for SelectTopNRows command from SSMS  ******/
use irosepetals

use irosepetals
SELECT 
--TOP (1000)
p.ProductID, 
		--[ID]
     ps.storeId
      --,[CreatedOn]
      -- ,[UpdatedOn]
  from Product p 
 inner Join [irosepetals].[dbo].[ProductStore] ps on p.productID = ps.productID
    where p.Published= 1 and ps.StoreID=7
order by p.ProductID


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
