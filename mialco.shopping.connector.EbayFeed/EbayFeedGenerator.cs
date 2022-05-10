using mialco.shopping.connector.FeedCommon;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.EbayFeed.EbayClasses;
using System;
using System.Collections.Generic;
using mialco.configuration;
using System.Xml.Serialization;
using System.IO;
using mialco.shopping.connector.shared;
using mialco.abstractions;
using mialco.shopping.connector.EbayFeed.Abstraction;

namespace mialco.shopping.connector.EbayFeed
{
	public class EbayFeedGenerator
	{
		//todo: Get this frpm the configuration
		private const int MaxAttributesPerProduct = 30;
		private Dictionary<string, string> _ebayMappedConditions;
		private Dictionary<int, List<string>> _ebayCategoryRequiredAttributes;
		private ApplicationSettings _applicatiopnSettings;
		private ApplicationInstanceSettings _applicationInstanceSettings;
		private IMialcoLogger _logger;
		private IEbayFeedCategoryAttributesRepository _ebayCategoryAttributesRepo;
		private int _productCountOverride = 0;
		public EbayFeedGenerator(ApplicationSettings applicationSettings,ApplicationInstanceSettings applicationInstanceSettings,  IMialcoLogger logger, IEbayFeedCategoryAttributesRepository attributesRepository)
		{
			_logger = logger;
			_applicatiopnSettings = applicationSettings;
			_applicationInstanceSettings = applicationInstanceSettings;
			_ebayCategoryAttributesRepo = attributesRepository;
			InitializeEbayMappedConditions();
			InitializeEbayCategoryRequiredAttributes();
			InitializeProductCountOverride(applicationInstanceSettings);
			
		}

		private void InitializeProductCountOverride(ApplicationInstanceSettings applicationInstanceSettings)
		{
			int.TryParse(applicationInstanceSettings.InventoryOverride, out _productCountOverride);
		}

		private void InitializeEbayCategoryRequiredAttributes()
		{
			_ebayCategoryRequiredAttributes = new Dictionary<int, List<string>>();
		}


		/// <summary>
		/// Loads the mapping between product condition of the raw feed and the ebay required condition name
		/// </summary>
		private void InitializeEbayMappedConditions()
		{
			//todo: Retrieve the map from the database
			_ebayMappedConditions = new Dictionary<string, string>();
			_ebayMappedConditions.Add(EbayConditionsMappedKeys.New, EbayProductConditions.NEW);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="FileName">The full path of the file name where the XML Feed will be written</param>
		/// <param name="feedRecords">A list of records extracted from a source (a shopping card application for example). 
		/// The records in this list are in a genercic format where the fields are stored in a dictionary   </param>
		/// <param name="feedProerties"></param>
		public void GenerateXmlFeed(string fileName, List<GenericFeedRecord> feedRecords, ApplicationInstanceSettings applicationInstanceSettings)
		{

			// We are building the "productRequest" object from the EbayClasses, which are created from the 
			//Schema provided by ebay, then we serialize the object to the output file
			// We eventually may need to 
			//using (GoogleXmlFileWriter xmlw = new GoogleXmlFileWriter(fileName))
			//Create an instance of the productRequest
			var locale = applicationInstanceSettings.EbayLocale;
			var productRequest = new productRequest();
			var items = new List<object>();
			var productItems  =new List<CombinedProductType>();
			{
				var counter = 0;
				foreach (var rec in feedRecords)
				{

					//	//TODO: REMOVE AFTER TESTING
					counter++;
					//if (counter > 500) break;
					var value = string.Empty;
					//create an instance of the product
					var product = new CombinedProductType();
					var sku = new SKUType { Value = rec.ProductId };
					product.SKU = sku;
					var productInformation = new ProductTypeProductInformation();
					product.productInformation = productInformation;
					productInformation.title = GetTitle(rec);
					product.productInformation.subtitle = string.Empty;

					var productInformationdescription = new ProductTypeProductInformationDescription();
					product.productInformation.description = productInformationdescription;
					productInformationdescription.productDescription = GetProductDescription( rec);

					productInformation.localizedFor = locale;


					var description = new ProductTypeProductInformationDescription();
					productInformation.Brand = string.Empty;// rec.GetFeedValue(EbayFeedElementMapping.Brand);
					productInformation.UPC = rec.GetFeedValue(EbayFeedElementMapping.UPC);
					productInformation.ISBN = rec.GetFeedValue(EbayFeedElementMapping.ISBN);
					productInformation.EAN = rec.GetFeedValue(EbayFeedElementMapping.EAN);
					productInformation.MPN = rec.GetFeedValue(EbayFeedElementMapping.MPN);
					productInformation.ePID = rec.GetFeedValue(EbayFeedElementMapping.ePID);
					
					// Pictures					
					productInformation.	pictureURL = GetPictureUrls(rec);
					//Attributes
					productInformation.attribute = GetProductAttributes(rec);
					//Codition
					var conditionInfo = new ProductTypeProductInformationConditionInfo();
					conditionInfo.condition =  GetEbayProductCondition(rec);
					conditionInfo.conditionDescription = rec.GetFeedValue(EbayFeedElementMapping.ConditionDescription);
					productInformation.conditionInfo = conditionInfo;

					//Distribution
					product.distribution = GetProductDistribution(rec, applicationInstanceSettings);

					product.inventory = GetProductInventory(rec);
					

					//ProductVariationGroup
					


					items.Add(product);
					productItems.Add(product);


				}
				//productRequest.productVariationGroup = new ProductVariationGroupType[] { };
				//productRequest.Items = items.ToArray();
				productRequest.product = productItems.ToArray();
				//Writing te xml
				XmlSerializer xmlsrl = new XmlSerializer(productRequest.GetType());
				TextWriter writer = new StreamWriter(fileName);
				xmlsrl.Serialize(writer, productRequest);
				//xmlw.Serialize(

			}
		}

		private string GetEbayProductCondition(GenericFeedRecord rec)
		{
			var result = string.Empty;
			var condition = rec.GetFeedValue(EbayFeedElementMapping.Condition);
			if (string.IsNullOrEmpty(condition))
			{
				//todo: Logging
				Console.WriteLine($"There is no condition associated with the product {rec.ProductId} in the raw record");
			}
			else
			{
				if (!_ebayMappedConditions.ContainsKey(condition))
				{
					Console.WriteLine($"There is no condition mapped for Ebay with the raw condition {condition}  for  the product {rec.ProductId} in the raw record");
				}
				else
				{
					result = _ebayMappedConditions[condition];
				}
			}
			return result;
		}




		/// <summary>
		/// Builds a list of the available attributes,like size, color, metal
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private AttributeType[] GetProductAttributes(GenericFeedRecord rec)
		{

			//Attributes
			var attributes = new List<AttributeType>();
			var categoryId = 0;
			AttributeType attributeType;
			var required = false;
			foreach (var attributeName in EbayRequiredAttributeNames.AttributeList)
			{
				switch (attributeName)
				{
					case EbayRequiredAttributeNames.Color :
					//attributeType = 	GetColorAttribute(rec, categoryId, out required);
					//	attributes.Add(attributeType);
						break;
					case EbayRequiredAttributeNames.Brand:
						required = true;
						//attributeType = Ge...(rec, categoryId, out required);
						///attributes.Add(attributeType);
						break;

					default:
						break;

				}
			
			}
			
			//var color = (rec.GetFeedValue(EbayFeedElementMapping.Color)?? string.Empty).Trim();
			//if (!string.IsNullOrEmpty(color)) attributes.Add(new AttributeType { name = "Color", Value = color });

			attributes.Add(new AttributeType { name = "Brand", Value = "Amore Rose Petals - New York" });
			attributes.Add(new AttributeType { name = "Product", Value = "Topiary" });
			//attributes.Add(new AttributeType { name = "Type", Value = "Table Skirt" });
			attributes.Add(new AttributeType { name = "Type", Value = "Petals" });
			attributes.Add(new AttributeType { name = "Occasion", Value = "Wedding" });
			//attributes.Add(new AttributeType { name = "Occasion", Value = "Party" });
			//attributes.Add(new AttributeType { name = "Occasion", Value = "All Occasions" });
			//attributes.Add(new AttributeType { name = "Occasion", Value = "Anniversary" });
			//attributes.Add(new AttributeType { name = "Occasion", Value = "Bachelorette Party" });
			//attributes.Add(new AttributeType { name = "Occasion", Value = "Baby Shower" });


			//var size = rec.GetFeedValue(EbayFeedElementMapping.Size);
			//if (!string.IsNullOrEmpty(size)) attributes.Add(new AttributeType { name = "Size", Value = color });

			//var metal = rec.GetFeedValue(EbayFeedElementMapping.Metal);
			//if (!string.IsNullOrEmpty(size)) attributes.Add(new AttributeType { name = "Metal", Value = metal });
			return attributes.ToArray();

		}

		/// <summary>
		/// It retrieves the color attribute of the product.
		/// The category id is used to determine if this attribute is required for the respective category
		/// </summary>
		/// <param name="rec"></param>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		private AttributeType GetColorAttribute(GenericFeedRecord rec, int categoryId, out bool required)
		{
			//todo: This method has to be redone considering all the conditions related to the category 
		 var color =	rec.GetFeedValue(EbayFeedElementMapping.Color);
			var attributeType = new AttributeType {name = EbayRequiredAttributeNames.Color, Value=color };
			required = false;
			return attributeType;
		}

		/// <summary>
		/// Returns the title trimmed and truncated at 80 characters long
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private string GetTitle(GenericFeedRecord rec)
		{
			//todo: get MaxLength from config
			const int MaxLenght = 80;
			var title= (rec.GetFeedValue(EbayFeedElementMapping.Title) ?? string.Empty).Trim() ;
			if (title.Length == 0) 
			{
				Console.WriteLine($"WARNING : EbayFeedgenerator::GetTitle() - The Title field is Zero Length for {rec.ProductId}");
			}
			title = $"1000 Wedding {title}, Petals & Garlands, Rose Petals"; 
			if (title.Length > MaxLenght)
			{
				Console.WriteLine($"WARNING : EbayFeedgenerator::GetTitle() - The Title field is longer than {MaxLenght} and will be truncated. RecordId: {rec.ProductId} Title: {title}");
				title = title.Substring(0, MaxLenght);
			}
			return title;
		}

		private string GetProductDescription(GenericFeedRecord rec)
		{

			//todo: DescriptionMaxLength in the app configuration
			const int DescriptionMaxLength = 800;

			var result = string.Empty;

			var variationSEDescription = rec.GetFeedValue(RawFeedFieldNames.VariantSEDescription);
			if (!string.IsNullOrEmpty(variationSEDescription))
			{
				result = variationSEDescription;
			}
			else {
				var seDescription = rec.GetFeedValue(RawFeedFieldNames.SEDescription);
				if (!string.IsNullOrEmpty(seDescription))
				{
					result = seDescription;
				}
				else
				{
					var variationDescription = rec.GetFeedValue(RawFeedFieldNames.VariantDescription);
					variationDescription = RemoveHtmlTags(variationDescription);
					if (!string.IsNullOrEmpty(variationDescription))
					{
						result = variationDescription;
					}
					else
					{
						var description = rec.GetFeedValue(RawFeedFieldNames.Description);
						description = RemoveHtmlTags(description);
						result = description;
					}
				}
			}
			if (string.IsNullOrEmpty(result))
			{ 
			//todo: Log Error 
			
			}
			return $"1000 {result} \n ******* All our products are shipped from our warehouse in Cicero, New York *******";		
		}

		private string RemoveHtmlTags(string variationDescription)
		{
			//todo: implementation
			return variationDescription;
		}

		/// <summary>
		/// Returns an array of picture urls
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		private string[] GetPictureUrls(GenericFeedRecord rec)
		{
			var pictureUrls = new List<string>();
			var pictureUrl = rec.GetFeedValue(EbayFeedElementMapping.ImageLink);
			if (!string.IsNullOrEmpty(pictureUrl))
			{
				pictureUrls.Add(pictureUrl);
			}
			//ToDo: Additional Picture is allowed only for advanced Ebay subscrptions
			var isAdvancedAccount = false;
			if (isAdvancedAccount)
			{
				pictureUrl = rec.GetFeedValue(EbayFeedElementMapping.AdditionalImageLink);
				if (!string.IsNullOrEmpty(pictureUrl))
				{
					pictureUrls.Add(pictureUrl);
				}
			}
			return pictureUrls.ToArray();

		}

		private DistributionType GetProductDistribution(GenericFeedRecord rec, ApplicationInstanceSettings applicationInstanceSettings)
		{
			var distribution = new DistributionType();
			var channelDetails = new DistributionTypeChannelDetails();
			distribution.channelDetails = channelDetails;
			var shippingCostOverrides = new DistributionTypeChannelDetailsShippingCostOverrides();
			channelDetails.shippingCostOverrides = shippingCostOverrides; //Laurentiu:
			channelDetails.channelID = applicationInstanceSettings.EbayChannel;
			
			var categoryId = 0;

			var locale = applicationInstanceSettings.EbayLocale;
			distribution.localizedFor = locale;

			rec.GetFeedValue(EbayFeedElementMapping.CategotyId,out categoryId);
			channelDetails.categorySpecified = true;
			channelDetails.category = categoryId; // TODO: This is a mandatory field. Needs Validation! 
			
			//channelDetails.secondaryCategorySpecified = true;
			//channelDetails.secondaryCategory = categoryId; // TODO: Implementation for the second category


			channelDetails.lotSize = 0;
			channelDetails.shippingPolicyName = applicationInstanceSettings.EbayShippingPolicy;
			//channelDetails.maxQuantityPerBuyer = ""; TODO: Either from the store database 
			//That is in the Product variant table "RestrictedQuantities and MinimumQuantity

			//Payment policy
			channelDetails.paymentPolicyName = applicationInstanceSettings.EbayPaymentPolicy;

			//Pricing
			channelDetails.pricingDetails = GetPricingDetails(rec);
			//Return Policy
			channelDetails.returnPolicyName = applicationInstanceSettings.EbayReturnPolicy;

			distribution.channelDetails.customFields = GetCustomFIelds(rec);
			distribution.channelDetails.hideBuyerDetails = GetHideBuyersDetailSetting(rec);
			distribution.channelDetails.hideBuyerDetailsSpecified = false;
			return distribution;
		}

		private DistributionTypeChannelDetailsPricingDetails GetPricingDetails(GenericFeedRecord rec)
		{
			var priceDetails = new DistributionTypeChannelDetailsPricingDetails();
			var price = 0m;
			rec.GetFeedValue(EbayFeedElementMapping.Price, out price);
			priceDetails.listPrice = price;
			priceDetails.listPriceSpecified = true;
			var salesPrice = 0m;
			if (rec.GetFeedValue(EbayFeedElementMapping.SalePrice, out salesPrice) && salesPrice> 0 && salesPrice < price)
			{
				//todo - we need more logic to establish the sales price based on date for example
				priceDetails.strikeThroughPriceSpecified = false; ///!!!!!! hardcoded not to put sales price
				priceDetails.strikeThroughPrice = salesPrice;
			}
			//TODO : Review minimumAdvertisedPrice implementation. This may be something imposed by a producer or distrbutor to the retailer 
			// To keep the price above a certain minimum. It may not apply to our store
			/*
				DISTRIBUTION.createChannelDetails(channelID).ApplyTax
				The sales tax that applied if sales tax is set up in your account preferences.To override the sales tax settings for the product, include a boolean value of false.
				boolean
				Optional
			*/
			//TemplateName: ToDo: IF we implementd product level enroichment

			return priceDetails;
		}

		private DistributionTypeChannelDetailsCustomField[] GetCustomFIelds(GenericFeedRecord rec) 
		{
			//TODO: Implement CustomFields from ebay product enrichment
			var customFields = new List<DistributionTypeChannelDetailsCustomField>();
			var customField = new DistributionTypeChannelDetailsCustomField();

			return customFields.ToArray();
		}

		private bool GetHideBuyersDetailSetting(GenericFeedRecord rec)
		{
			var result = false;
			//todo: This is optional field The default is false
			// When we will implement ebay product enhancemet,we shoudl be able to add logic to this field

			return result;
		}

		
		private InventoryType GetProductInventory(GenericFeedRecord rec)
		{
			var inventory = new InventoryType();
			var inventoryCount = 0;
			// TODO: This is not the correct field . We may need to extract from the database the inventory field
			if (rec.GetFeedValue(EbayFeedElementMapping.Inventory, out inventoryCount) && inventoryCount>0)
			{
				inventory.totalShipToHomeQuantity = inventoryCount;
				inventory.totalShipToHomeQuantitySpecified = true;
				if (_productCountOverride> 0 )
					inventory.totalShipToHomeQuantity = _productCountOverride;
			}
			return inventory;
		}

	}
}
