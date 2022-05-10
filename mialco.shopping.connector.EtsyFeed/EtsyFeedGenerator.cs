using mialco.abstractions;
using mialco.configuration;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace mialco.shopping.connector.EtsyFeed
{
	public class EtsyFeedGenerator
	{

		//private Dictionary<string, string> _ebayMappedConditions;
		//private Dictionary<int, List<string>> _ebayCategoryRequiredAttributes;
		private ApplicationSettings _applicatiopnSettings;
		private ApplicationInstanceSettings _applicationInstanceSettings;
		private IMialcoLogger _logger;
		private int _productCountOverride;

		//private IEbayFeedCategoryAttributesRepository _ebayCategoryAttributesRepo;
		//private int _productCountOverride = 0;
		public EtsyFeedGenerator(ApplicationSettings applicationSettings, ApplicationInstanceSettings applicationInstanceSettings, IMialcoLogger logger)
		{
			_logger = logger;
			_applicatiopnSettings = applicationSettings;
			_applicationInstanceSettings = applicationInstanceSettings;
			int.TryParse(applicationInstanceSettings.InventoryOverride, out _productCountOverride);
		}


		public void GenerateFeed(string fileName, List<GenericFeedRecord> feedRecords, ApplicationInstanceSettings applicationInstanceSettings)
		{
			var etsyListItems = new List<EtsyListingItem>();
			foreach (var rec in feedRecords)
			{
				var listItem = new EtsyListingItem();
				listItem.quantity = GetItemInventory(rec);
				listItem.title = GetTitle(rec);
				listItem.description = rec.GetFeedValue(RawFeedFieldNames.Description);
				listItem.price = GetPrice(rec);
				listItem.who_made = GetWhoMade(rec);
				listItem.when_made = GetWhenMade(rec);
				listItem.taxonomy_id = GetTaxonomyId(rec, applicationInstanceSettings);
				listItem.shipping_profile_id = GetShippingProfileId(rec, applicationInstanceSettings);
				listItem.materials = GetProductMaterials(rec,applicationInstanceSettings);
				listItem.shop_section_id = GetProductShopSection(rec, applicationInstanceSettings);
				listItem.processing_min = GetProcessingMin(rec, applicationInstanceSettings);
				listItem.tags = GetProductTags();
				listItem.styles = GetProductStyles();
				listItem.item_weight = GetProductWeight(rec);
				listItem.item_length = GetProductLength(rec);
				listItem.item_width = GetProductWidth(rec);
				listItem.item_height = GetProductHeight(rec);
				listItem.item_weight_unit = GetProductWeighUnit(rec, applicationInstanceSettings);
				listItem.item_dimensions_unit = GetProductDimensionsUnit(rec, applicationInstanceSettings);
				listItem.is_personalizable = GetIsPersonalizable(rec, applicationInstanceSettings);
				listItem.personalization_is_required = GetPersonalizationIsRequired(rec, applicationInstanceSettings);
				listItem.personalization_char_count_max = GetPersonalizationCharCountMax(rec, applicationInstanceSettings);
				listItem.personalization_instructions = GetProductPersonalizationInstructions(rec, applicationInstanceSettings);
				listItem.production_partner_ids = GetProductionPartnerIds(rec, applicationInstanceSettings);
				listItem.image_ids = GetProductImagesIds(rec, applicationInstanceSettings);
				listItem.is_supply = GetProductIsSupply(rec, applicationInstanceSettings);
				listItem.is_customizable = GetProductIsCustomizable(rec, applicationInstanceSettings);
				listItem.is_taxable = GetProductIsTaxable(rec, applicationInstanceSettings);
				etsyListItems.Add(listItem);
			}
			var writer = new StreamWriter(fileName);
			var jso = new JsonSerializerOptions();
			var json= JsonSerializer.Serialize(etsyListItems);
			writer.Write(json);
			writer.Close();
		}


		/// <summary>
		/// boolean
		/// When true, applicable shop tax rates apply to this listing at checkout.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private bool GetProductIsTaxable(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: Implement GetProductIsTaxable()
			return true;
		}

		/// <summary>
		/// 
		/// boolean
		/// When true, a buyer may contact the seller for a customized order.The default value is true when a shop accepts custom orders.Does not apply to shops that do not accept custom orders.
		///// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private bool GetProductIsCustomizable(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: Implement GetProductIsCustomizable()
			return false;
		}

		/// <summary>
		/// 
		/// boolean
		/// When true, tags the listing as a supply product, else indicates that it's a finished product. Helps buyers locate the listing under the Supplies heading. Requires 'who_made' and 'when_made'.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private bool GetProductIsSupply(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: implementation GetProductIsSupply
			return false;
		}

		private List<int> GetProductImagesIds(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo : Implement GetProductImagesIds()
			return new List<int>();
		}

		private List<int> GetProductionPartnerIds(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			return null;
		}

		private string GetProductPersonalizationInstructions(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			return null;
		}

		private int GetPersonalizationCharCountMax(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: GetPersonalizationCharCountMax implementation
			return 0;
		}

		private bool? GetPersonalizationIsRequired(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: implementaion of (GetPersonalizationIsRequired)
			return null;
		}

		private bool? GetIsPersonalizable(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: implementation GetIsPersonalizable()
			return null;
		}

		private string GetProductDimensionsUnit(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo : implementation GetProductDimensionsUnit
			return null;
		}

		private string GetProductWeighUnit(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: implementation GerProductWightUnit
			return null;
		}

		/// <summary>
		/// 
		///		number<float>[0..1.79769313486e+308] Nullable
		///The numeric length of the product measured in units set in 'item_dimensions_unit'. Default value is null. If set, the value must be greater than 0.
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private float? GetProductLength(GenericFeedRecord rec)
		{
			//todo: GetProductLength
			return null;
		}

		/// <summary>
		/// 
		///		number<float>[0..1.79769313486e+308] Nullable
		///The numeric width of the product measured in units set in 'item_dimensions_unit'. Default value is null. If set, the value must be greater than 0.
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private float? GetProductWidth(GenericFeedRecord rec)
		{
			//todo: GetProductWidth
			return null;
		}

		/// <summary>
		/// 
		///		number<float>[0..1.79769313486e+308] Nullable
		///The numeric height of the product measured in units set in 'item_dimensions_unit'. Default value is null. If set, the value must be greater than 0.
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private float? GetProductHeight(GenericFeedRecord rec)
		{
			//todo: GetProductHeight
			return null;
		}



		/// <summary>
		/// 
		///		number<float>[0..1.79769313486e+308] Nullable
		///The numeric weight of the product measured in units set in 'item_weight_unit'. Default value is null. If set, the value must be greater than 0.
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private float? GetProductWeight(GenericFeedRecord rec)
		{
			var result = new float?();
			var weight = new float();
			if (rec.GetFeedValue(RawFeedFieldNames.Weight, out weight))
				result = weight;
			return result;
		}

		/// <summary>
		/// Array of strings Nullable
		///		An array of style strings for this listing, each of which is free-form text string such as "Formal", or "Steampunk". A Listing may have up to two styles.Valid style strings contain only letters, numbers, and whitespace characters. (regex: /[^\p{ L}\p{Nd
		///	}\p{Zs
		///}]/ u) Default value is null.
		/// </summary>
		/// <returns></returns>
		private List<string> GetProductStyles()
		{
			return null;
		}

		/// <summary>
		///		Array of strings Nullable
		///A list of tag strings for the listing.
		///Valid tag strings contain only letters, numbers, 
		///whitespace characters, -, ', ™, ©, and ®. 
		///(regex: /[^\p{L}\p{Nd}\p{Zs}-'™©®]/u) 
		///Default value is null.
		/// </summary>
		/// <returns></returns>
		private List<string> GetProductTags()
		{
			return null;
		}

		/// <summary>
		///	integer Nullable
		/// The maximum number of days required to process this listing.Default value is null.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private int? GetProcessingMax(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: Implement GeProcessingMax
			return null;
		}

		/// <summary>
		///		integer Nullable
		///		The minimum number of days required to process this listing.Default value is null.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private int? GetProcessingMin(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: Implement GeProcessingMin
			return null;
		}

		/// <summary>
		/// integer >= 1 Nullable
		/// The numeric ID of the shop section for this listing.Default value is null.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private int? GetProductShopSection(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			//todo: Implementation of ProductShopSection
			return null;
		}

		/// <summary>
		///		Array of strings Nullable
		///		A list of material strings for materials used in the product.Valid materials strings contain only letters, numbers, and whitespace characters. (regex: /[^\p{ L}\p{Nd
		///		}\p{Zs
		///		}]/ u) Default value is null.
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private List<string> GetProductMaterials(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			var result = new List<string>();

			return result;
		}



		/// <summary>
		/// Shipping Profile id must be > 1. 
		/// It is the numeric shipping id associated with the product
		/// Currently in shopping connector is associated with a category 
		/// but for future use we will need to store this with the product
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private int GetShippingProfileId(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			var result = 0;
			result = applicationInstanceSettings.EtsyShippingProfileId;
			return result;
		}

		/// <summary>
		/// Return etsy taxonomy id assigned to the product.
		/// Current version will get it from application instance settings
		/// The taxonomy it is provided by Etsy API and the implementation will need to add
		/// a way to extract the taxonomy for each productio or product category from API:
		/// https://developers.etsy.com/documentation/reference/#operation/getSellerTaxonomyNodes
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="applicationInstanceSettings"></param>
		/// <returns></returns>
		private int GetTaxonomyId(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)

		{
			var result = 0;
			result = applicationInstanceSettings.EtsyDefaultCategoryId;
			return result;
		}

		/// <summary>
		/// Enum: "made_to_order" "2020_2021" "2010_2019" "2002_2009" 
		/// "before_2002" "2000_2001" "1990s" "1980s" "1970s" "1960s" "1950s" 
		/// "1940s" "1930s" "1920s" "1910s" "1900s" "1800s" "1700s" "before_1700"
		/// An enumerated string for the era in which the maker made the product in this listing.
		/// Helps buyers locate the listing under the Vintage heading.
		/// Requires 'is_supply' and 'who_made'.
		/// Using a reference to the shared class mialco.shared EtsyWhenMade 
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private string GetWhenMade(GenericFeedRecord rec)
		{
			//todo : This is to be passed as a parameter 
			//from the instance settings or from the product directly
			return shopping.connector.shared.EtsyWhenMade.MadeToOrder;
		
		}

		/// <summary>
		/// Enum: "i_did", "someone_else", "collective"
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private string GetWhoMade(GenericFeedRecord rec)
		{
			//Hard-Coded for now
			//TODO: Add to the instance configuration or item configuration and default
			//DOTO: Add to the software debt
			return "i_did";
		}

		private float GetPrice(GenericFeedRecord rec)
		{
			float price = 0;
			rec.GetFeedValue(RawFeedFieldNames.Price, out price);
			return price;
		}

		private int GetItemInventory(GenericFeedRecord rec)
		{
			if (rec.GetFeedValue(RawFeedFieldNames.Inventory, out int inventoryCount) && inventoryCount > 0)
			{
				if (_productCountOverride > 0)
				inventoryCount = _productCountOverride;
			}
			return inventoryCount;
		}

		private string GetTitle(GenericFeedRecord rec)
		{
			var title = string.Empty;
			title = rec.GetFeedValue(RawFeedFieldNames.Title);
			//TODO : Validation
			//The listing's title string. Valid title strings contain only letters, numbers, punctuation marks, mathematical symbols, whitespace characters, ™, ©, and ®. (regex: /[^\p{L}\p{Nd}\p{P}\p{Sm}\p{Zs}™©®]/u) You can only use the %, :, & and + characters once each.
			return title;
		}

	}
}
