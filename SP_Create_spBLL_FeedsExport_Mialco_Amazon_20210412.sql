USE [irosepetals]
GO

/****** Object:  StoredProcedure [dbo].[spBLL_FeedsExport_Mialco_Amazon_20210412]    Script Date: 4/16/2021 1:02:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Mike Onciulescu>
-- Create date: 04/12/2021
-- Description:	Exports for amazon Feed. Based on Existing sp : spBLL_FeedsExport
--exec [spBLL_FeedsExport] 'Amazon-Tricouri',1,'451'
-- =============================================
CREATE PROCEDURE [dbo].[spBLL_FeedsExport_Mialco_Amazon_20210412]
	@feedName AS NVARCHAR(50),
	@storeId AS INT =NULL ,
	@categories as nvarchar(max)  =null,
	@ParentCategory as nvarchar(100) = null,
	@price as float = null,
	@brand nvarchar(50) = 'Amore Tees',
	@manufacturer nvarchar(100) = 'Amore Tees',
	@itemType varchar(50) = 'Generic Prroduct',
	@quantity decimal 
AS
BEGIN

DECLARE @Prefix as NVARCHAR(50)

SET @Prefix ='SYR'
--SET @brand ='BestSyracuse'
--IF(@StoreID =8)
--BEGIN
-- SET @Prefix='SYR'
-- SET @brand = 'BestSyracuse'
--END
--IF(@StoreID =1)
--BEGIN
-- SET @Prefix='SYR'
-- SET @brand = 'BestSyracuse'
--END
--IF(@StoreID =16)
--BEGIN
-- SET @Prefix='SYR'
-- SET @brand = 'BestSyracuse'
--END

--IF(@StoreID =14) 
--BEGIN
--  SET @Prefix='WIT'
--  SET @brand = 'WickedTees'
--END




if(@feedName='GetModes')
begin
	select '1' as Id, 'Amazon-Tricouri' as Mode
	union all
	select '2' as Id, 'Amazon-Costume' as Mode
	union all 
	select '3' as Id, 'Amazon-Chiloti' as Mode
	union all 
	select '4' as Id, 'Amazon-Hoodies' as Mode
	union all 
	select '5' as Id, 'Amazon-Aprons' as Mode
	union all 
	select '6' as Id, 'Amazon-long sleeve'  Mode
	union all
	select '7' as Id, 'Amazon-crewneck sweatshirts'  Mode
	union all
	select '8' as Id, 'Amazon-rhinestones'  Mode
	union all
	select '10' as Id,  'Amazon-Tricouri rhinestones' as Mode
	--union all
	--select '11' as Id, 'Amazon-long sleeve rhinestones' as Mode
	union all
	select '9' as Id, 'ExportAllProducts' as Mode
	union all
	select '11' as Id, 'T-shirt titles' as Mode

end



if(@categories like '%149%') set @ParentCategory='Costume'
if(@categories not like '%149%') set @ParentCategory = 'Tricouri'


IF(@feedName ='T-shirt titles')
begin
	select distinct replace(replace(replace(Name,'T-shirts',''),'-t-shirt',''),'t-shirt','') as Name 
	from Product
	where Name like '%t-shirt%' and name not like '%long sleeve%' and name not like '%wedding%' and name not like '%funny%'
	and name not like '%college%' and name not like '%novelty%' and name not like '%logo%' and name not like '%tee%'
	and name not like '%rude%' and name not like '%scary%' and name not like '%airplane%' 

end
		
		
if(@feedName ='Amazon-Feed')
begin

if(@price is null)
begin
set @price = 14.95
end


SELECT  distinct
	   	[item-type] as item_type,

        sku as item_sku,
		brand as brand_name,
		'' as part_number,
		@manufacturer as manufacturer,
		[product-id] as external_product_id,
		[product-id-type] as external_product_id_type,
		@itemType as item_type,
		[item-price] as standard_price,
		@quantity as quantity,
		[main-image-url] as main_image_url, 
		[other-image-url1] as other_image_url1 ,
		[other-image-url2] as other_image_url2,
		[other-image-url3] as other_image_url3,
		[other-image-url4] as other_image_url4,
		[other-image-url5] as other_image_url5,
		[other-image-url6] as other_image_url6,
		[other-image-url7] as other_image_url7,
		[other-image-url8] as other_image_url8 ,
		[swatch-image-url] as swatch_image_url,
		[parent-child] as parent_child,
		[parent-sku] as parent_sku,
		[relationship-type] as relationship_type,
		[variation-theme] as variation_theme,
		[update-delete] as update_delete,
		dbo.RemoveSpecialChars(dbo.fn_empty_blank(dbo.[StripHtml]((replace(replace(replace(cast([product-description] as nvarchar(max)),'ÿ',' '),'<strong>',' '),'</strong>',' '))))) as product_description,
		sku as model,
	    [apparel-closure-type] as  closure_type,		
		[bullet-point1] as bullet_point1,
	    [bullet-point2] as bullet_point2 ,
	    [bullet-point3] as bullet_point3,
	    [bullet-point4] as bullet_point4 ,
	    [bullet-point5] as bullet_point5,
		'' as target_audience,
		'' as catalog_number,
		'' as specific_uses_keywords1,
		'' as specific_uses_keywords2,
		'' as specific_uses_keywords3,
		'' as specific_uses_keywords4,
		'' as specific_uses_keywords5,
		'' as target_audience_keywords1,
		'' as target_audience_keywords2,
		'' as target_audience_keywords3,
		'' as thesaurus_attribute_keywords1,
		'' as thesaurus_attribute_keywords2,
		'' as thesaurus_attribute_keywords3,
		'' as thesaurus_attribute_keywords4,
		'' as thesaurus_subject_keywords1,
		'' as thesaurus_subject_keywords2,
		'' as thesaurus_subject_keywords3,
		[search-terms1] as generic_keywords,
		[platinum-keywords1] as platinum_keywords1 ,
		[platinum-keywords2] as platinum_keywords2 ,
		[platinum-keywords3] as platinum_keywords3,
		[platinum-keywords4] as platinum_keywords4,
		[platinum-keywords5] as platinum_keywords5,
	    [country-as-labeled] as country_as_labeled,
	    [fur-description] as fur_description,
		'' as occasion,
	    [number-of-pieces] as number_of_pieces,
		'' as scent_name,
		'' as light_source_type,
		color as color_name,
		

		--
		department as department_name,
		size as size_name,
		--[item-price] as standard_price, 
		'' as merchant_shipping_group_name,


	


		--[search-terms1] as generic_keywords1,
		--[search-terms2] as generic_keywords2,
		--[search-terms3] as generic_keywords3,
		--[search-terms4] as generic_keywords4,
		--[search-terms5] as generic_keywords5,

	


		
		[color-map] as color_map ,	
		 [fit-type] as fit_type,
		[neck-size] as neck_size,
	    [neck-size-unit-of-measure] as neck_size_unit_of_measure,
	    [neck-style] as neck_style,
		

		[size-map] as size_map,
	   [size-modifier] as special_size_type,
	   [sleeve-length] as sleeve_length,
	   [sleeve-length-unit-of-measure] as sleeve_length_unit_of_measure,
	   [sleeve-type] as sleeve_type,
	   [style-name] as style_name,

	    [shipping-weight] as website_shipping_weight,
		 [shipping-weight-unit-measure] as website_shipping_weight_unit_of_measure,
		 [item-weight-unit-of-measure] as item_weight_unit_of_measure,
		 [item-weight] as  item_weight,
		 [item-length-unit-of-measure] as item_length_unit_of_measure,
		[item-length] as item_length,
		[item-width] as item_width,
		[item-height]as item_height ,
		[cpsia-warning1] as cpsia_cautionary_statement,
		--[cpsia-warning1] as cpsia_cautionary_statement1,
		--[cpsia-warning2] as cpsia_cautionary_statement2, 
		--[cpsia-warning3] as cpsia_cautionary_statement3,
		--[cpsia-warning4] as cpsia_cautionary_statement4,
		[cpsia-warning-description] as cpsia_cautionary_description,

		 --[material-fabric1] as fabric_type1,
	  -- [material-fabric2] as fabric_type2,
	  -- [material-fabric3] as fabric_type3,
	    [material-fabric1] as fabric_type,
		 [import-designation] as import_designation,

		 '' as prop_65,


		[fulfillment-center-id] as fulfillment_center_id,
	    [package-height] as package_height ,
		[package-width] as package_width,
		[package-length] as package_length,
		[package-length-unit-of-measure] as package_length_unit_of_measure,
		[package-weight] as package_weight,
		[package-weight-unit-of-measure] as package_weight_unit_of_measure,
		


		[sale-price] as list_price,
		'New' as condition_type,
		'' as condition_note,
		[product-tax-code] as product_tax_code,
		'' as fulfillment_latency,
		[launch-date] as product_site_launch_date,
		[release-date] as merchant_release_date,
		[restock-date] as restock_date,
	    [sale-price] as sale_price,
		[sale-from-date] as sale_from_date, 
		[sale-through-date] as sale_end_date,
		'' as max_aggregate_ship_quantity,
		[item-package-quantity]  as item_package_quantity,
		[number-of-items] as number_of_items,
		[is-gift-message-available] as offering_can_be_gift_messaged,
		[is-gift-wrap-available] as offering_can_be_giftwrapped,
		[is-discontinued-by-manufacturer] as is_discontinued_by_manufacturer,
		'' as missing_keyset_reason, 
		''as max_order_quantity

		--currency,
	
		
		
		--'' as delivery_schedule_group_id,
		
		
		
		
	 
	   
		

		--[belt-style] as belt_style ,
		--[bottom-style] as bottom_style, 
		--[button-quantity] as button_quantity ,
		--[character] as subject_character ,
		--[chest-size] as chest_size,
		--[chest-size-unit-of-measure] as chest_size_unit_of_measure,
		--[band-size-num] as band_size_num ,
		--[band-size-num-unit-of-measure] as band_size_num_unit_of_measure,
		--[collar-type]as collar_style,
		
	    		    
	 --   [control-type] as control_type,
	 --   [cuff-type] as cuff_type,
	 --   [cup-size] as cup_size,
	    
	 --   [fabric-wash] as fabric_wash,
	   
	 --   [front-pleat-type] as front_style,
		--[inseam-length] as  inseam_length,
	 --   [inseam-length-unit-of-measure] as inseam_length_unit_of_measure,
	 --   [is-stain-resistant] as is_stain_resistant,
	 --   [item-rise] as rise_height, 
	 --   [item-rise-unit-of-measure] as rise_height_unit_of_measure,
	 --   --[laptop-capacity],
	 --  [leg-diameter] as leg_diameter,
	 --  [leg-diameter-unit-of-measure] as leg_diameter_unit_of_measure,
	 --  [leg-style] as leg_style,
	  
	
	 --  [material-opacity] as opacity,
	  
	 --  [pattern-style] as pattern_type ,
	 --  [pocket-description] as pocket_description,
	 --  [rise-style] as rise_style,
	 --  [shoe-width] as shoe_width,
	
	

	 --  [special-feature1] as special_features,
	 --  [strap-type] as strap_type,
	   
	 --  [theme] as theme,
	 --  [toe-style] as toe_style,
	 --  [top-style] as top_style ,
	 --  [underwire-type] as  underwire_type,
	 --  [waist-size] as waist_size,
	 --  [waist-size-unit-of-measure] as waist_size_unit_of_measure,
	 --  [water-resistance-level] as water_resistance_level,
	 --  '' as capacity_name,
	 --  [wheel-type] as wheel_type,
	 --  '' as sport_type

FROM(
SELECT 
	   p.productId,
       @Prefix+p.SKU+ '-F' + cm.Items+sm.Items as sku,
	   p.name + '-'+  'F' + cm.Items+'-'+sm.Items  AS [product-name], 
	   '' as [product-id],
	   '' as [product-id-type],
	   @brand AS brand,
	  p.Description AS [product-description],
	  -- 'Brand New' as [bullet-point1],
	  -- '100% Cotton Tee' as [bullet-point2],
	  --'Available in various sizes  from Small to 5XL' as [bullet-point3],
	  --'Easy Care, Machine Wash' as [bullet-point4],
	  --'Graphic Logo Tee' as [bullet-point5],
	  '100% cotton Ring-spun fabric — softer and more durable for better washing and wearing.  
*Ash: 99% cotton/1% viscose. Heather Grey: 90% cotton/10% viscose.Double-needle-cover seamed neck — keeps the collar from waving' as [bullet-point1],
	   'The Artwork is printed  with the latest printing technology  Direct to Garment' as 	[bullet-point2],
	   'We use only ink Oeko-Tex® Standard 100, Class 1 certified and are safe for use on adult, infant and youth apparel.' as [bullet-point3],
	   'Printed with pride in New York, USA' as [bullet-point4],
	   'This is not an unauthorized replica/counterfeit item. This is an original inspired design and does not infringe on any rights holders rights. Words used in the title/search terms are not intended to imply they are licensed by any rights holders' as [bullet-point5],
	  isnull(cast(@price+
	    replace(replace(replace((select top 1  items from Split(pv.Colors,',') where Items like '%'+cm.Items +'%'),cm.Items,''),'[',''),']','') 
		+
		case  when sm.items like '%x%' then 4
		else 0
		end
		as nvarchar(10)),@price)
		
		AS [item-price],
		
	  '' as msrp,
	  'USD' as currency,
	  '' as [product-tax-code],
	  '' as [shipping-weight],
	  '' as [shipping-weight-unit-measure],
	  '' as [leadtime-to-ship],
	  '' as [launch-date],
	  '' as [release-date],
	  '' as [restock-date],
	  '1000' as quantity,
	  '' as [sale-price],
	  '' as [sale-from-date],
	  '' as [sale-through-date],
	  --'graphic tee tshirt' as [search-terms1],
	  --'t-shirt novelty print funny' as [search-terms2],
	  --'T-Shirt Amore Funny t-shirtsFunny shirts' as [search-terms3],
	  --'Crazy t-shirts Cool t-shirts' as [search-terms4],
	  --'Cool shirts Crazy shirts' as [search-terms5],
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),1)
	   else 'graphic tee tshirt'
	   end )as [search-terms1],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),2)
	   else  't-shirt novelty print funny'
	   end )as [search-terms2],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),3)
	   else  'T-Shirt Amore Funny t-shirtsFunny shirts'
	   end )as [search-terms3],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),4)
	   else   'Crazy t-shirts Cool t-shirts'
	   end )as [search-terms4],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),5)
	   else   'Cool shirts Crazy shirts'
	   end )as [search-terms5],
	  'novelty-t-shirts' as [item-type],
	  'http://' + Store.ProductionURI + '/images/Product/medium/' + CAST(p.productId AS NVARCHAR(10)) +'_1_'+cm.Items+ '.jpg' AS [main-image-url], 
	  '' as [other-image-url1],
	  '' as [other-image-url2],
	  '' as [other-image-url3],
	  '' as [other-image-url4],
	  '' as [other-image-url5],
	  '' as [other-image-url6],
	  '' as [other-image-url7],
	  '' as [other-image-url8],
	  'http://' + Store.ProductionURI + '/images/Product/medium/' + CAST(p.productId AS NVARCHAR(10)) +'_1_'+cm.Items+ '.jpg' AS [swatch-image-url], 
	  '' as [fulfillment-center-id],
	  '' as [package-height],
	  '' as [package-width],
	  '' as [package-length],
	  '' as [package-length-unit-of-measure],
	  '' as [package-weight],
	  '' as [package-weight-unit-of-measure],
	  '' as [max-aggregate-ship-quantity],
	   'child' as [parent-child],
	    @Prefix+p.SKU +'-F' as [parent-sku],
		'Variation' as [relationship-type],
		'SizeColor' as [variation-theme],
		'' as [apparel-closure-type],
		'' as [belt-style],
		'' as [bottom-style],
		'' as [button-quantity],
		'' as [character],
		'' as [chest-size],
		'' as [chest-size-unit-of-measure],
		'' as [band-size-num],
		'' as [band-size-num-unit-of-measure],
		'' as [collar-type],
		 cm.Items as color,
		 case cm.Items when 'natural' then 'beige'
					   when 'lime' then 'green'
					   when 'ash' then 'grey'
		     else cm.Items
	     end as [color-map],			    
		
		 '' as [control-type],
		 '' as [cpsia-warning1],
		 '' as [cpsia-warning2],
		 '' as [cpsia-warning3],
		 '' as [cpsia-warning4],
		 '' as [cpsia-warning-description],
		 '' as [cuff-type],
		 '' as [cup-size],
		 'womens' as [department],
		 '' as [fabric-wash],
		 '' as [fit-type],
		 '' as [front-pleat-type],
		 '' as [inseam-length],
		 '' as [inseam-length-unit-of-measure],
		 '' as [is-stain-resistant],
		 '' as [item-package-quantity],
		 '' as [item-rise],
		 '' as [item-rise-unit-of-measure],
		 '' as [laptop-capacity],
		 '' as [leg-diameter],
		 '' as [leg-diameter-unit-of-measure],
		 '' as [leg-style],
		 '' as [material-fabric1],
		 '' as [material-fabric2],
		 '' as [material-fabric3],
		 '' as [import-designation],
		 '' as [country-as-labeled],
		 '' as [fur-description],
		 '' as [material-opacity],
		 '' as [neck-size],
		 '' as [neck-size-unit-of-measure],
		 '' as [neck-style],
		 '' as [number-of-items],
		 '' as [number-of-pieces],
		 '' as [pattern-style],
		 '' as [pocket-description],
		 '' as [rise-style],
		 '' as [shoe-width],
		 sm.Items as size,
		  --case sm.Items when 'S' then 'Small'
				--	   when 'M' then 'Medium'
				--	   when 'L' then 'Large'
				--	   when 'XL' then 'X-Large'
				--	   when '2XL' then 'XX-Large'
				--	   when '3XL' then 'XXX-Large'
				--	   when '4XL' then 'XXXX-Large'
				--	   when '5XL' then 'XXXXX-Large'
		  --else ''

	     --end
		 sm.Items as [size-map],
		 '' as [size-modifier],
		 '' as [sleeve-length],
		 '' as [sleeve-length-unit-of-measure],
		 '' as [sleeve-type],
		 '' as [special-feature1],
		 '' as [strap-type],
		 '' as [style-name],
		 '' as [theme],
		 '' as [toe-style],
		 '' as [top-style],
		 '' as [underwire-type],
		 '' as [waist-size],
		 '' as [waist-size-unit-of-measure],
		 '' as [water-resistance-level],
		 '' as [wheel-type],
		  p.SKU+cm.Items+sm.Items as model,
		  'LB' as [item-weight-unit-of-measure],
		  '1' as [item-weight],
		  '' as [item-length-unit-of-measure],
		  '' as [item-length],
		  '' as [item-width],
		  '' as [item-height],
		  '' as [is-gift-message-available],
		  '' as [is-gift-wrap-available],
		  '' as [is-discontinued-by-manufacturer],
		  '' as [registered-parameter],
		  '' as [platinum-keywords1],
		  '' as [platinum-keywords2],
		  '' as [platinum-keywords3],
		  '' as [platinum-keywords4],
		  '' as [platinum-keywords5],
		  'update' as [update-delete]


	  
	  
FROM
	 dbo.product p
	 left JOIN dbo.productvariant pv ON p.productid = pv.productid
	 INNER JOIN ProductStore ON ProductStore.ProductID = p.ProductID
	 INNER JOIN Store ON Store.StoreID = ProductStore.StoreId
	 INNER JOIN productCategory ON productCategory.ProductId = p.productid
	 INNER JOIN category ON productCategory.CategoryId = category.categoryID
	 cross apply dbo.Split(cast(pv.ColorSKUModifiers as nvarchar(1000)),',')  as cm
	 cross apply dbo.Split(cast(pv.SizeSKUModifiers as nvarchar(1000)),',') as sm
	 LEFT JOIN(SELECT variantid, 
					  SUM(quan)inventory
			   FROM dbo.inventory
			   GROUP BY variantid)i ON pv.variantid = i.variantid
WHERE
    p.IsSystem = 0
  AND p.deleted = 0
  AND p.published = 1
  AND p.ExcludeFromPriceFeeds = 0
  AND pv.isdefault = 1
  AND
   CASE p.TrackInventoryBySizeAndColor
	  WHEN 1 THEN ISNULL(i.inventory, 0)
		  ELSE pv.inventory
	  END >= 0
  AND store.storeId = @StoreID
 and category.categoryId in (select Items from dbo.split(@categories,','))

union all 

SELECT 
	   p.productId,
        @Prefix+p.SKU+ '-B' + cm.Items+sm.Items as sku,
	   p.name + '-'+  'B' + cm.Items+'-'+sm.Items  AS [product-name], 
	   '' as [product-id],
	   '' as [product-id-type],
	   @brand AS brand,
	  p.Description AS [product-description],
	     '100% cotton Ring-spun fabric — softer and more durable for better washing and wearing.  
*Ash: 99% cotton/1% viscose. Heather Grey: 90% cotton/10% viscose.Double-needle-cover seamed neck — keeps the collar from waving' as [bullet-point1],
	   'The Artwork is printed  with the latest printing technology  Direct to Garment' as 	[bullet-point2],
	   'We use only ink Oeko-Tex® Standard 100, Class 1 certified and are safe for use on adult, infant and youth apparel.' as [bullet-point3],
	   'Printed with pride in New York, USA' as [bullet-point4],
	   'This is not an unauthorized replica/counterfeit item. This is an original inspired design and does not infringe on any rights holders rights. Words used in the title/search terms are not intended to imply they are licensed by any rights holders' as [bullet-point5],
	  cast(@price+
	    replace(replace(replace((select top 1  items from Split(pv.Colors,',') where Items like '%'+cm.Items +'%'),cm.Items,''),'[',''),']','') 
		+
		case  when sm.items like '%x%' then 4
		else 0
		end
		as nvarchar(10))
		
		AS [item-price],
		
	  '' as msrp,
	  'USD' as currency,
	  '' as [product-tax-code],
	  '' as [shipping-weight],
	  '' as [shipping-weight-unit-measure],
	  '' as [leadtime-to-ship],
	  '' as [launch-date],
	  '' as [release-date],
	  '' as [restock-date],
	  '1000' as quantity,
	  '' as [sale-price],
	  '' as [sale-from-date],
	  '' as [sale-through-date],
	  --'graphic tee tshirt' as [search-terms1],
	  --'t-shirt novelty print funny' as [search-terms2],
	  --'T-Shirt Amore Funny t-shirtsFunny shirts' as [search-terms3],
	  --'Crazy t-shirts Cool t-shirts' as [search-terms4],
	  --'Cool shirts Crazy shirts' as [search-terms5],
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),1)
	   else 'graphic tee tshirt'
	   end )as [search-terms1],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),2)
	   else  't-shirt novelty print funny'
	   end )as [search-terms2],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),3)
	   else  'T-Shirt Amore Funny t-shirtsFunny shirts'
	   end )as [search-terms3],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),4)
	   else   'Crazy t-shirts Cool t-shirts'
	   end )as [search-terms4],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),5)
	   else   'Cool shirts Crazy shirts'
	   end )as [search-terms5],
	  'novelty-t-shirts' as [item-type],
	  'http://' + Store.ProductionURI + '/images/Product/large/' + CAST(p.productId AS NVARCHAR(10)) +'_1_'+cm.Items+ '.jpg' AS [main-image-url], 
	  '' as [other-image-url1],
	  '' as [other-image-url2],
	  '' as [other-image-url3],
	  '' as [other-image-url4],
	  '' as [other-image-url5],
	  '' as [other-image-url6],
	  '' as [other-image-url7],
	  '' as [other-image-url8],
	  '' as [swatch-image-url],
	  '' as [fulfillment-center-id],
	  '' as [package-height],
	  '' as [package-width],
	  '' as [package-length],
	  '' as [package-length-unit-of-measure],
	  '' as [package-weight],
	  '' as [package-weight-unit-of-measure],
	  '' as [max-aggregate-ship-quantity],
	   'child' as [parent-child],
	     @Prefix+p.SKU +'-B' as [parent-sku],
		'Variation' as [relationship-type],
		'SizeColor' as [variation-theme],
		'' as [apparel-closure-type],
		'' as [belt-style],
		'' as [bottom-style],
		'' as [button-quantity],
		'' as [character],
		'' as [chest-size],
		'' as [chest-size-unit-of-measure],
		'' as [band-size-num],
		'' as [band-size-num-unit-of-measure],
		'' as [collar-type],
		 cm.Items as color,
		 case cm.Items when 'natural' then 'beige'
					   when 'lime' then 'green'
					   when 'ash' then 'grey'
		     else cm.Items
	     end as [color-map],			    
		
		 '' as [control-type],
		 '' as [cpsia-warning1],
		 '' as [cpsia-warning2],
		 '' as [cpsia-warning3],
		 '' as [cpsia-warning4],
		 '' as [cpsia-warning-description],
		 '' as [cuff-type],
		 '' as [cup-size],
		 'mens' as [department],
		 '' as [fabric-wash],
		 '' as [fit-type],
		 '' as [front-pleat-type],
		 '' as [inseam-length],
		 '' as [inseam-length-unit-of-measure],
		 '' as [is-stain-resistant],
		 '' as [item-package-quantity],
		 '' as [item-rise],
		 '' as [item-rise-unit-of-measure],
		 '' as [laptop-capacity],
		 '' as [leg-diameter],
		 '' as [leg-diameter-unit-of-measure],
		 '' as [leg-style],
		 '' as [material-fabric1],
		 '' as [material-fabric2],
		 '' as [material-fabric3],
		 '' as [import-designation],
		 '' as [country-as-labeled],
		 '' as [fur-description],
		 '' as [material-opacity],
		 '' as [neck-size],
		 '' as [neck-size-unit-of-measure],
		 '' as [neck-style],
		 '' as [number-of-items],
		 '' as [number-of-pieces],
		 '' as [pattern-style],
		 '' as [pocket-description],
		 '' as [rise-style],
		 '' as [shoe-width],
		 sm.Items as size,
		  --case sm.Items when 'S' then 'Small'
				--	   when 'M' then 'Medium'
				--	   when 'L' then 'Large'
				--	   when 'XL' then 'X-Large'
				--	   when '2XL' then 'XX-Large'
				--	   when '3XL' then 'XXX-Large'
				--	   when '4XL' then 'XXXX-Large'
				--	   when '5XL' then 'XXXXX-Large'
		  --else ''

	     --end
		 sm.Items as [size-map],
		 '' as [size-modifier],
		 '' as [sleeve-length],
		 '' as [sleeve-length-unit-of-measure],
		 '' as [sleeve-type],
		 '' as [special-feature1],
		 '' as [strap-type],
		 '' as [style-name],
		 '' as [theme],
		 '' as [toe-style],
		 '' as [top-style],
		 '' as [underwire-type],
		 '' as [waist-size],
		 '' as [waist-size-unit-of-measure],
		 '' as [water-resistance-level],
		 '' as [wheel-type],
		  p.SKU+cm.Items+sm.Items as model,
		  'LB' as [item-weight-unit-of-measure],
		  '1' as [item-weight],
		  '' as [item-length-unit-of-measure],
		  '' as [item-length],
		  '' as [item-width],
		  '' as [item-height],
		  '' as [is-gift-message-available],
		  '' as [is-gift-wrap-available],
		  '' as [is-discontinued-by-manufacturer],
		  '' as [registered-parameter],
		  '' as [platinum-keywords1],
		  '' as [platinum-keywords2],
		  '' as [platinum-keywords3],
		  '' as [platinum-keywords4],
		  '' as [platinum-keywords5],
		  'update' as [update-delete]


	  
	  
FROM
	 dbo.product p
	 left JOIN dbo.productvariant pv ON p.productid = pv.productid
	 INNER JOIN ProductStore ON ProductStore.ProductID = p.ProductID
	 INNER JOIN Store ON Store.StoreID = ProductStore.StoreId
	 INNER JOIN productCategory ON productCategory.ProductId = p.productid
	 INNER JOIN category ON productCategory.CategoryId = category.categoryID
	 cross apply dbo.Split(cast(pv.ColorSKUModifiers as nvarchar(1000)),',')  as cm
	 cross apply dbo.Split(cast(pv.SizeSKUModifiers as nvarchar(1000)),',') as sm
	 LEFT JOIN(SELECT variantid, 
					  SUM(quan)inventory
			   FROM dbo.inventory
			   GROUP BY variantid)i ON pv.variantid = i.variantid
WHERE
    p.IsSystem = 0
  AND p.deleted = 0
  AND p.published = 1
  AND p.ExcludeFromPriceFeeds = 0
  AND pv.isdefault = 1
  AND
   CASE p.TrackInventoryBySizeAndColor
	  WHEN 1 THEN ISNULL(i.inventory, 0)
		  ELSE pv.inventory
	  END >= 0
  AND store.storeId = @StoreID
 and category.categoryId in (select Items from dbo.split(@categories,','))

union all
SELECT 
       p.productId,
       @Prefix+p.SKU +'-F' as sku,
	   p.name  AS [product-name], 
	  '' as [product-id],
	   '' as [product-id-type],
	   @brand AS brand,
	  p.Description AS [product-description],
	   '100% cotton Ring-spun fabric — softer and more durable for better washing and wearing.  
*Ash: 99% cotton/1% viscose. Heather Grey: 90% cotton/10% viscose.Double-needle-cover seamed neck — keeps the collar from waving' as [bullet-point1],
	   'The Artwork is printed  with the latest printing technology  Direct to Garment' as 	[bullet-point2],
	   'We use only ink Oeko-Tex® Standard 100, Class 1 certified and are safe for use on adult, infant and youth apparel.' as [bullet-point3],
	   'Printed with pride in New York, USA' as [bullet-point4],
	   'This is not an unauthorized replica/counterfeit item. This is an original inspired design and does not infringe on any rights holders rights. Words used in the title/search terms are not intended to imply they are licensed by any rights holders' as [bullet-point5],
	  ''  AS [item-price],
      '' as msrp,
	  '' as currency,
	  '' as [product-tax-code],
	  '' as [shipping-weight],
	  '' as [shipping-weight-unit-measure],
	  '' as [leadtime-to-ship],
	  '' as [launch-date],
	  '' as [release-date],
	  '' as [restock-date],
	  '' as quantity,
	  '' as [sale-price],
	  '' as [sale-from-date],
	  '' as [sale-through-date],
	  --'graphic tee tshirt' as [search-terms1],
	  --'t-shirt novelty print funny' as [search-terms2],
	  --'T-Shirt Amore Funny t-shirtsFunny shirts' as [search-terms3],
	  --'Crazy t-shirts Cool t-shirts' as [search-terms4],
	  --'Cool shirts Crazy shirts' as [search-terms5],
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),1)
	   else 'graphic tee tshirt'
	   end )as [search-terms1],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),2)
	   else  't-shirt novelty print funny'
	   end )as [search-terms2],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),3)
	   else  'T-Shirt Amore Funny t-shirtsFunny shirts'
	   end )as [search-terms3],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),4)
	   else   'Crazy t-shirts Cool t-shirts'
	   end )as [search-terms4],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),5)
	   else   'Cool shirts Crazy shirts'
	   end )as [search-terms5],
	  'novelty-t-shirts' as [item-type],
	  'http://' + Store.ProductionURI + '/images/Product/large/' + CAST(p.productId AS NVARCHAR(10)) +'.jpg' AS [main-image-url], 
	  '' as [other-image-url1],
	  '' as [other-image-url2],
	  '' as [other-image-url3],
	  '' as [other-image-url4],
	  '' as [other-image-url5],
	  '' as [other-image-url6],
	  '' as [other-image-url7],
	  '' as [other-image-url8],
	  '' as [swatch-image-url],
	  '' as [fulfillment-center-id],
	  '' as [package-height],
	  '' as [package-width],
	  '' as [package-length],
	  '' as [package-length-unit-of-measure],
	  '' as [package-weight],
	  '' as [package-weight-unit-of-measure],
	  '' as [max-aggregate-ship-quantity],
	   'parent' as [parent-child],
	    ''  as [parent-sku],
		'' as [relationship-type],
		'SizeColor' as [variation-theme],
		'' as [apparel-closure-type],
		'' as [belt-style],
		'' as [bottom-style],
		'' as [button-quantity],
		'' as [character],
		'' as [chest-size],
		'' as [chest-size-unit-of-measure],
		'' as [band-size-num],
		'' as [band-size-num-unit-of-measure],
		'' as [collar-type],
		 '' as color,
		 '' as [color-map],
		 '' as [control-type],
		 '' as [cpsia-warning1],
		 '' as [cpsia-warning2],
		 '' as [cpsia-warning3],
		 '' as [cpsia-warning4],
		 '' as [cpsia-warning-description],
		 '' as [cuff-type],
		 '' as [cup-size],
		 'womens' as [department],
		 '' as [fabric-wash],
		 '' as [fit-type],
		 '' as [front-pleat-type],
		 '' as [inseam-length],
		 '' as [inseam-length-unit-of-measure],
		 '' as [is-stain-resistant],
		 '' as [item-package-quantity],
		 '' as [item-rise],
		 '' as [item-rise-unit-of-measure],
		 '' as [laptop-capacity],
		 '' as [leg-diameter],
		 '' as [leg-diameter-unit-of-measure],
		 '' as [leg-style],
		 '' as [material-fabric1],
		 '' as [material-fabric2],
		 '' as [material-fabric3],
		 '' as [import-designation],
		 '' as [country-as-labeled],
		 '' as [fur-description],
		 '' as [material-opacity],
		 '' as [neck-size],
		 '' as [neck-size-unit-of-measure],
		 '' as [neck-style],
		 '' as [number-of-items],
		 '' as [number-of-pieces],
		 '' as [pattern-style],
		 '' as [pocket-description],
		 '' as [rise-style],
		 '' as [shoe-width],
		 '' as size,
		 '' as [size-map],
		 '' as [size-modifier],
		 '' as [sleeve-length],
		 '' as [sleeve-length-unit-of-measure],
		 '' as [sleeve-type],
		 '' as [special-feature1],
		 '' as [strap-type],
		 '' as [style-name],
		 '' as [theme],
		 '' as [toe-style],
		 '' as [top-style],
		 '' as [underwire-type],
		 '' as [waist-size],
		 '' as [waist-size-unit-of-measure],
		 '' as [water-resistance-level],
		 '' as [wheel-type],
		  p.SKU as model,
		  '' as [item-weight-unit-of-measure],
		  '' as [item-weight],
		  '' as [item-length-unit-of-measure],
		  '' as [item-length],
		  '' as [item-width],
		  '' as [item-height],
		  '' as [is-gift-message-available],
		  '' as [is-gift-wrap-available],
		  '' as [is-discontinued-by-manufacturer],
		  '' as [registered-parameter],
		  '' as [platinum-keywords1],
		  '' as [platinum-keywords2],
		  '' as [platinum-keywords3],
		  '' as [platinum-keywords4],
		  '' as [platinum-keywords5],
		  'update' as [update-delete]


	  
	  
FROM
	 dbo.product p
	 JOIN dbo.productvariant pv ON p.productid = pv.productid
	 INNER JOIN ProductStore ON ProductStore.ProductID = p.ProductID
	 INNER JOIN Store ON Store.StoreID = ProductStore.StoreId
	 INNER JOIN productCategory ON productCategory.ProductId = p.productid
	 INNER JOIN category ON productCategory.CategoryId = category.categoryID
	 LEFT JOIN(SELECT variantid, 
					  SUM(quan)inventory
			   FROM dbo.inventory
			   GROUP BY variantid)i ON pv.variantid = i.variantid
WHERE p.IsSystem = 0
  AND p.deleted = 0
  AND p.published = 1
  AND p.ExcludeFromPriceFeeds = 0
  AND pv.isdefault = 1
  AND CASE p.TrackInventoryBySizeAndColor
	  WHEN 1 THEN ISNULL(i.inventory, 0)
		  ELSE pv.inventory
	  END >= 0
 AND store.storeId = @StoreID
 and category.categoryId in (select Items from dbo.split(@categories,','))



union all
SELECT 
       p.productId,
       @Prefix+p.SKU +'-B' as sku,
	   p.name  AS [product-name], 
	  '' as [product-id],
	   '' as [product-id-type],
	   @brand AS brand,
	  p.Description AS [product-description],
	   '100% cotton Ring-spun fabric — softer and more durable for better washing and wearing.  
*Ash: 99% cotton/1% viscose. Heather Grey: 90% cotton/10% viscose.Double-needle-cover seamed neck — keeps the collar from waving' as [bullet-point1],
	   'The Artwork is printed  with the latest printing technology  Direct to Garment' as 	[bullet-point2],
	   'We use only ink Oeko-Tex® Standard 100, Class 1 certified and are safe for use on adult, infant and youth apparel.' as [bullet-point3],
	   'Printed with pride in New York, USA' as [bullet-point4],
	   'This is not an unauthorized replica/counterfeit item. This is an original inspired design and does not infringe on any rights holders rights. Words used in the title/search terms are not intended to imply they are licensed by any rights holders' as [bullet-point5],
	  ''  AS [item-price],
      '' as msrp,
	  '' as currency,
	  '' as [product-tax-code],
	  '' as [shipping-weight],
	  '' as [shipping-weight-unit-measure],
	  '' as [leadtime-to-ship],
	  '' as [launch-date],
	  '' as [release-date],
	  '' as [restock-date],
	  '' as quantity,
	  '' as [sale-price],
	  '' as [sale-from-date],
	  '' as [sale-through-date],
	  --'graphic tee tshirt' as [search-terms1],
	  --'t-shirt novelty print funny' as [search-terms2],
	  --'T-Shirt Amore Funny t-shirtsFunny shirts' as [search-terms3],
	  --'Crazy t-shirts Cool t-shirts' as [search-terms4],
	  --'Cool shirts Crazy shirts' as [search-terms5],
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),1)
	   else 'graphic tee tshirt'
	   end )as [search-terms1],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),2)
	   else  't-shirt novelty print funny'
	   end )as [search-terms2],
	 
	  (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),3)
	   else  'T-Shirt Amore Funny t-shirtsFunny shirts'
	   end )as [search-terms3],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),4)
	   else   'Crazy t-shirts Cool t-shirts'
	   end )as [search-terms4],

	 (case when   cast(p.SEKeywords as nvarchar(max))  <>''
	   then  dbo.fnGetSEOSearch ( cast(p.SEKeywords as nvarchar(max)),5)
	   else   'Cool shirts Crazy shirts'
	   end )as [search-terms5],
	  'novelty-t-shirts' as [item-type],
	  'http://' + Store.ProductionURI + '/images/Product/large/' + CAST(p.productId AS NVARCHAR(10)) +'.jpg' AS [main-image-url], 
	  '' as [other-image-url1],
	  '' as [other-image-url2],
	  '' as [other-image-url3],
	  '' as [other-image-url4],
	  '' as [other-image-url5],
	  '' as [other-image-url6],
	  '' as [other-image-url7],
	  '' as [other-image-url8],
	  '' as [swatch-image-url],
	  '' as [fulfillment-center-id],
	  '' as [package-height],
	  '' as [package-width],
	  '' as [package-length],
	  '' as [package-length-unit-of-measure],
	  '' as [package-weight],
	  '' as [package-weight-unit-of-measure],
	  '' as [max-aggregate-ship-quantity],
	   'parent' as [parent-child],
	    ''  as [parent-sku],
		'' as [relationship-type],
		'SizeColor' as [variation-theme],
		'' as [apparel-closure-type],
		'' as [belt-style],
		'' as [bottom-style],
		'' as [button-quantity],
		'' as [character],
		'' as [chest-size],
		'' as [chest-size-unit-of-measure],
		'' as [band-size-num],
		'' as [band-size-num-unit-of-measure],
		'' as [collar-type],
		 '' as color,
		 '' as [color-map],
		 '' as [control-type],
		 '' as [cpsia-warning1],
		 '' as [cpsia-warning2],
		 '' as [cpsia-warning3],
		 '' as [cpsia-warning4],
		 '' as [cpsia-warning-description],
		 '' as [cuff-type],
		 '' as [cup-size],
		 'mens' as [department],
		 '' as [fabric-wash],
		 '' as [fit-type],
		 '' as [front-pleat-type],
		 '' as [inseam-length],
		 '' as [inseam-length-unit-of-measure],
		 '' as [is-stain-resistant],
		 '' as [item-package-quantity],
		 '' as [item-rise],
		 '' as [item-rise-unit-of-measure],
		 '' as [laptop-capacity],
		 '' as [leg-diameter],
		 '' as [leg-diameter-unit-of-measure],
		 '' as [leg-style],
		 '' as [material-fabric1],
		 '' as [material-fabric2],
		 '' as [material-fabric3],
		 '' as [import-designation],
		 '' as [country-as-labeled],
		 '' as [fur-description],
		 '' as [material-opacity],
		 '' as [neck-size],
		 '' as [neck-size-unit-of-measure],
		 '' as [neck-style],
		 '' as [number-of-items],
		 '' as [number-of-pieces],
		 '' as [pattern-style],
		 '' as [pocket-description],
		 '' as [rise-style],
		 '' as [shoe-width],
		 '' as size,
		 '' as [size-map],
		 '' as [size-modifier],
		 '' as [sleeve-length],
		 '' as [sleeve-length-unit-of-measure],
		 '' as [sleeve-type],
		 '' as [special-feature1],
		 '' as [strap-type],
		 '' as [style-name],
		 '' as [theme],
		 '' as [toe-style],
		 '' as [top-style],
		 '' as [underwire-type],
		 '' as [waist-size],
		 '' as [waist-size-unit-of-measure],
		 '' as [water-resistance-level],
		 '' as [wheel-type],
		  p.SKU as model,
		  '' as [item-weight-unit-of-measure],
		  '' as [item-weight],
		  '' as [item-length-unit-of-measure],
		  '' as [item-length],
		  '' as [item-width],
		  '' as [item-height],
		  '' as [is-gift-message-available],
		  '' as [is-gift-wrap-available],
		  '' as [is-discontinued-by-manufacturer],
		  '' as [registered-parameter],
		  '' as [platinum-keywords1],
		  '' as [platinum-keywords2],
		  '' as [platinum-keywords3],
		  '' as [platinum-keywords4],
		  '' as [platinum-keywords5],
		  'update' as [update-delete]


	  
	  
FROM
	 dbo.product p
	 JOIN dbo.productvariant pv ON p.productid = pv.productid
	 INNER JOIN ProductStore ON ProductStore.ProductID = p.ProductID
	 INNER JOIN Store ON Store.StoreID = ProductStore.StoreId
	 INNER JOIN productCategory ON productCategory.ProductId = p.productid
	 INNER JOIN category ON productCategory.CategoryId = category.categoryID
	 LEFT JOIN(SELECT variantid, 
					  SUM(quan)inventory
			   FROM dbo.inventory
			   GROUP BY variantid)i ON pv.variantid = i.variantid
WHERE p.IsSystem = 0
  AND p.deleted = 0
  AND p.published = 1
  AND p.ExcludeFromPriceFeeds = 0
  AND pv.isdefault = 1
  AND CASE p.TrackInventoryBySizeAndColor
	  WHEN 1 THEN ISNULL(i.inventory, 0)
		  ELSE pv.inventory
	  END >= 0
 AND store.storeId = @StoreID
 and category.categoryId in (select Items from dbo.split(@categories,','))


) as temp
order by  [product-id] , [parent-child] desc	
end






END
GO


