/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) c0.[CategoryID]
      --,[CategoryGUID]
	  ,c0.ParentCategoryID as parentid0
	  ,c1.ParentCategoryID as parentid1
	  ,c2.ParentCategoryID as parentid2
	  ,c3.ParentCategoryID as parentid3
	  ,c3.Name as parentCategory3
	  ,c2.Name as parentCategory2
      ,c1.Name as parentCategory1
	  ,c0.[Name]
      ,c0.[Summary]
      ,c0.[Description]
      ,c0.[SEKeywords]
      ,c0.[SEDescription]
      ,c0.[DisplayPrefix]
      ,c0.[SETitle]
      ,c0.[SEAltText]
      ,c0.[ParentCategoryID]
      ,c0.[ColWidth]
      ,c0.[SortByLooks]
      ,c0.[DisplayOrder]
      ,c0.[RelatedDocuments]
      ,c0.[XmlPackage]
      ,c0.[Published]
      ,c0.[Wholesale]
      ,c0.[AllowSectionFiltering]
      ,c0.[AllowManufacturerFiltering]
      ,c0.[AllowProductTypeFiltering]
      ,c0.[QuantityDiscountID]
      ,c0.[ShowInProductBrowser]
      ,c0.[SEName]
      ,c0.[ExtensionData]
      ,c0.[ImageFilenameOverride]
      ,c0.[IsImport]
      ,c0.[Deleted]
      --,c0.[CreatedOn]
      ,c0.[PageSize]
      ,c0.[TaxClassID]
      ,c0.[SkinID]
      ,c0.[TemplateName]
      ,c0.[UpdatedOn]
  FROM [irosepetals].[dbo].[Category] c0
  left join dbo.Category c1 on c0.ParentCategoryID = c1.CategoryID
  left join dbo.Category c2 on c1.ParentCategoryID = c2.CategoryID
  left join dbo.Category c3 on c2.ParentCategoryID = c3.CategoryID

 --  order by c3.name , c2.Name,c1.Name 
 order by c0.ParentCategoryID,  c1.ParentCategoryID, c2.ParentCategoryID, c3.ParentCategoryID
