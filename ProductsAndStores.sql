/****** Script for SelectTopNRows command from SSMS  ******/
use irosepetals

use irosepetals
use irosepetals
SELECT 
--TOP (1000)
p.ProductID,
p.Name,
pc.CategoryID,
cat.Name as CategoryName,
 
		--[ID]
ps.storeId
      --,[CreatedOn]
      -- ,[UpdatedOn]
from Product p 
inner Join [irosepetals].[dbo].[ProductStore] ps on p.productID = ps.productID
inner Join [irosepetals].[dbo].[ProductCategory] pc on p.ProductID = pc.ProductID    
inner Join [irosepetals].[dbo].[Category] cat on pc.CategoryID = cat.CategoryID
where p.Published= 1 and ps.StoreID=33
and cat.Name like '%petal%'
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
