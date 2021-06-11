using mialco.shopping.connector.FeedCommon;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.EbayFeed.EbayClasses;
using System;
using System.Collections.Generic;
using mialco.configuration;
using System.Xml.Serialization;
using System.IO;

namespace mialco.shopping.connector.EbayFeed
{
	public class EbayFeedGenerator
	{
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
					productInformation.title = rec.GetFeedValue(EbayFeedElementMapping.Title);
					product.productInformation.subtitle = string.Empty;

					var productInformationdescription = new ProductTypeProductInformationDescription();
					product.productInformation.description = productInformationdescription;
					productInformationdescription.productDescription = rec.GetFeedValue(EbayFeedElementMapping.Description);

					productInformation.localizedFor = locale;


					var description = new ProductTypeProductInformationDescription();
					productInformation.Brand = rec.GetFeedValue(EbayFeedElementMapping.Brand);
					productInformation.UPC = rec.GetFeedValue(EbayFeedElementMapping.UPC);
					productInformation.ISBN = rec.GetFeedValue(EbayFeedElementMapping.ISBN);
					productInformation.EAN = rec.GetFeedValue(EbayFeedElementMapping.EAN);
					productInformation.MPN = rec.GetFeedValue(EbayFeedElementMapping.MPN);
					productInformation.ePID = rec.GetFeedValue(EbayFeedElementMapping.ePID);
					
					// Pictures					
					productInformation.pictureURL = GetPictureUrls(rec);
					//Attributes
					productInformation.attribute = GetProductAttributes(rec);
					//Codition
					var conditionInfo = new ProductTypeProductInformationConditionInfo();
					conditionInfo.condition = rec.GetFeedValue(EbayFeedElementMapping.Condition);
					conditionInfo.conditionDescription = rec.GetFeedValue(EbayFeedElementMapping.ConditionDescription);
					productInformation.conditionInfo = conditionInfo;

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


		/// <summary>
		/// Builds a list of the available attributes,like size, color, metal
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		public AttributeType[] GetProductAttributes(GenericFeedRecord rec)
		{
			//Attributes
			var attributes = new List<AttributeType>();
			var color = rec.GetFeedValue(EbayFeedElementMapping.Color);
			if (!string.IsNullOrEmpty(color)) attributes.Add(new AttributeType { name = "Color", Value = color });

			var size = rec.GetFeedValue(EbayFeedElementMapping.Size);
			if (!string.IsNullOrEmpty(size)) attributes.Add(new AttributeType { name = "Size", Value = color });

			var metal = rec.GetFeedValue(EbayFeedElementMapping.Metal);
			if (!string.IsNullOrEmpty(size)) attributes.Add(new AttributeType { name = "Metal", Value = metal });
			return attributes.ToArray();

		}

		/// <summary>
		/// Returns an array of picture urls
		/// </summary>
		/// <param name="rec"></param>
		/// <returns></returns>
		public string[] GetPictureUrls(GenericFeedRecord rec)
		{
			var pictureUrls = new List<string>();
			var pictureUrl = rec.GetFeedValue(EbayFeedElementMapping.ImageLink);
			if (!string.IsNullOrEmpty(pictureUrl))
			{
				pictureUrls.Add(pictureUrl);
			}
			pictureUrl = rec.GetFeedValue(EbayFeedElementMapping.AdditionalImageLink);
			if (!string.IsNullOrEmpty(pictureUrl))
			{
				pictureUrls.Add(pictureUrl);
			}

			return pictureUrls.ToArray();

		}

	}
}
