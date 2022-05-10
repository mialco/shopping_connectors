﻿/****** Script for SelectTopNRows command from SSMS  ******/
use irosepetals
DECLARE @StoreId INT 

SET @StoreId = 31

SELECT TOP (10000000) 
pc.CategoryID
,pr.[ProductID]
,pv.variantid
,st.Name as StoreName
,st.ProductionURI
--,pr.[ProductGUID]
      ,pr.[Name]
      ,pr.[SEName]
      ,pr.[Summary]
      ,pr.[Description]
      ,pr.[SEKeywords]
      ,pr.[SEDescription]
      ,pr.[MiscText]
      ,pr.[SwatchImageMap]
      ,pr.[FroogleDescription]
      ,pr.[SETitle]
      ,pr.[SEAltText]
      ,pr.[SizeOptionPrompt]
      ,pr.[ColorOptionPrompt]
      ,pr.[TextOptionPrompt]
      ,pr.[ProductTypeID]
      ,pr.[TaxClassID]
      ,pr.[SKU]
      ,pr.[ManufacturerPartNumber]
      ,pr.[SalesPromptID]
      ,pr.[IsFeatured]
      ,pr.[XmlPackage]
      ,pr.[ColWidth]
      ,pr.[Published]
      ,pr.[Wholesale]
      ,pr.[RequiresRegistration]
      ,pr.[Looks]
      ,pr.[Notes]
      ,pr.[QuantityDiscountID]
      ,pr.[RelatedProducts]
      ,pr.[UpsellProducts]
      ,pr.[UpsellProductDiscountPercentage]
      ,pr.[RelatedDocuments]
      ,pr.[TrackInventoryBySizeAndColor]
      ,pr.[TrackInventoryBySize]
      ,pr.[TrackInventoryByColor]
      ,pr.[IsAKit]
      ,pr.[ShowInProductBrowser]
      ,pr.[ShowBuyButton]
      ,pr.[RequiresProducts]
      ,pr.[HidePriceUntilCart]
      ,pr.[IsCalltoOrder]
      ,pr.[ExcludeFromPriceFeeds]
      ,pr.[RequiresTextOption]
      ,pr.[TextOptionMaxLength]
      ,pr.[ExtensionData]
      ,pr.[ExtensionData2]
      ,pr.[ExtensionData3]
      ,pr.[ExtensionData4]
      ,pr.[ExtensionData5]
      ,pr.[ImageFilenameOverride]
      ,pr.[IsImport]
      ,pr.[IsSystem]
      ,pr.[Deleted]
      ,pr.[CreatedOn]
      ,pr.[WarehouseLocation]
      ,pr.[SkinID]
      ,pr.[TemplateName]
      ,pr.[UpdatedOn]
	  , '******Start PV' as startPv
	  ,pv.VariantID
      ,pv.[VariantGUID] as pv_VariantId
      ,pv.[IsDefault]
      ,pv.[Name]
      ,pv.[Description]
      ,pv.[SEKeywords]
      ,pv.[SEDescription]
      ,pv.[SEAltText]
      ,pv.[Colors]
      ,pv.[ColorSKUModifiers]
      ,pv.[Sizes]
      ,pv.[SizeSKUModifiers]
      ,pv.[FroogleDescription]
      ,pv.[ProductID]
      ,pv.[SKUSuffix]
      ,pv.[ManufacturerPartNumber]
      ,pv.[Price]
      ,pv.[SalePrice]
      ,pv.[Weight]
      ,pv.[MSRP]
      ,pv.[Cost]
      ,pv.[Points]
      ,pv.[Dimensions]
      ,pv.[Inventory]
      ,pv.[DisplayOrder]
      ,pv.[Notes]
      ,pv.[IsTaxable]
      ,pv.[IsShipSeparately]
      ,pv.[IsDownload]
      ,pv.[DownloadLocation]
      ,pv.[DownloadValidDays]
      ,pv.[FreeShipping]
      ,pv.[Published]
      ,pv.[Wholesale]
      ,pv.[IsSecureAttachment]
      ,pv.[IsRecurring]
      ,pv.[RecurringInterval]
      ,pv.[RecurringIntervalType]
      ,pv.[RewardPoints]
      ,pv.[SEName]
      ,pv.[RestrictedQuantities]
      ,pv.[MinimumQuantity]
      ,pv.[ExtensionData]
      ,pv.[ExtensionData2]
      ,pv.[ExtensionData3]
      ,pv.[ExtensionData4]
      ,pv.[ExtensionData5]
      ,pv.[ImageFilenameOverride]
      ,pv.[IsImport]
      ,pv.[Deleted]
      ,pv.[CreatedOn]
      ,pv.[CustomerEntersPrice]
      ,pv.[CustomerEntersPricePrompt]
      ,pv.[Condition]
      ,pv.[GTIN]
      ,pv.[UpdatedOn]

  FROM [irosepetals].[dbo].[Product] pr
  inner join dbo.ProductVariant pv on pr.ProductID=pv.ProductID
  left outer join [irosepetals].[dbo].[ProductStore] ps on pr.ProductID =  ps.ProductID
  inner join [irosepetals].[dbo].[Store] st on ps.StoreID = st.StoreID
  inner join [dbo].[ProductCategory] pc on pr.ProductID = pc.ProductID
  where pr.Published =1
  and st.Published = 1
  and ps.StoreId=@StoreId
  --and pr.ProductID=20803
  and pc.CategoryID in (
  3,
4,
8,
12,
13,
15,
16,
17,
18,
19,
20,
22,
24,
25,
28,
30,
31,
32,
33,
34,
35,
36,
37,
42,
43,
47,
49,
51,
52,
53,
54,
56,
57,
59,
60,
75,
76,
78,
93,
95,
96,
97,
98,
99,
100,
101,
102,
119,
120,
121,
123,
125,
129)
  
  order by pc.CategoryID,  pr.ProductID
  