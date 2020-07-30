using mialco.shopping.objectvalues;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace GoogleAdFeed
{
	public class StoreFrontGoogleFeedMap : MialcoValueObject, IEnumerable
	{
		OrderedDictionary _propertyMapping;
		
		/// <summary>.HasPrincipalKey(pk => pk.ProductID).
		/// Instantiates the Class
		/// Instantiated the mapping values as a dictionary having the 
		/// keys the feed properties and as values the name of the source table 
		/// properties correpondent to the feed field
		/// </summary>
		public StoreFrontGoogleFeedMap():base()
		{
			_propertyMapping = new OrderedDictionary();
			DictionaryInit();
		}

		private void DictionaryInit()
		{
			_propertyMapping.Add("id", "SKU");
			_propertyMapping.Add("title", "Title");
			_propertyMapping.Add("description", "Description");
			_propertyMapping.Add("link", "Link");
			_propertyMapping.Add("image_link", "ImageLink");
			_propertyMapping.Add("additional_image_link","AdditionalImageLink");
			_propertyMapping.Add("mobile_link", "MobileLink");
			_propertyMapping.Add("availability", "Availability");
			_propertyMapping.Add("availability_date", "AcvailabilityDate");
			_propertyMapping.Add("cost_of_goods_sold", "CostOfGoodsSold");
			_propertyMapping.Add("expiration_date", "ExpirationDate");
			_propertyMapping.Add("price", "Price");
			_propertyMapping.Add("sale_price", "SalePrice");
			_propertyMapping.Add("sale_price_effective_date", "");
			_propertyMapping.Add("unit_pricing_measure", "");
			_propertyMapping.Add("unit_pricing_base_measure", "");
			_propertyMapping.Add("installment", "");
			_propertyMapping.Add("subscription_cost", "");
			_propertyMapping.Add("loyalty_points", "");

			//"Product category", "");
			//"You can use these attributes to organize your advertising campaigns in Google Ads.", "");

			_propertyMapping.Add("google_product_category", "Category");
			_propertyMapping.Add("product_type", "");

			//"Product identifiers", "");

			_propertyMapping.Add("brand", "");
			_propertyMapping.Add("gtin", "");
			_propertyMapping.Add("MPN", "");
			_propertyMapping.Add("identifier_exists", "");

			//"Detailed product description", "");
			_propertyMapping.Add("condition", "");
			_propertyMapping.Add("adult", "");
			_propertyMapping.Add("multipack", "");
			_propertyMapping.Add("is_bundle", "");
			_propertyMapping.Add("energy_efficiency_class", "");
			_propertyMapping.Add("min_energy_efficiency_class", "");
			_propertyMapping.Add("max_energy_efficiency_class", "");
			_propertyMapping.Add("age_group", "");
			_propertyMapping.Add("color", "");
			_propertyMapping.Add("gender", "");
			_propertyMapping.Add("material", "");
			_propertyMapping.Add("pattern", "");
			_propertyMapping.Add("size", "");
			_propertyMapping.Add("size_type", "");
			_propertyMapping.Add("size_system", "");
			_propertyMapping.Add("item_group_id", "");

			//"Shopping campaigns and other configurations", "");
			//"These attributes are used to control how your product data is used when you create advertising campaigns in Google Ads.", "");
			_propertyMapping.Add("ads_redirect", "");
			_propertyMapping.Add("custom_label_0", "");
			_propertyMapping.Add("promotion_id", "");

			//"Destinations", "");
			//"These attributes can be used to control the type of ads your products participate in. For example, you could use this attribute if you want a product to appear in a dynamic remarketing campaign, but not in a Shopping ads campaign.", "");
			_propertyMapping.Add("excluded_destination", "");
			_propertyMapping.Add("included_destination", "");

			//("Shipping", "");
			_propertyMapping.Add("shipping", "");
			_propertyMapping.Add("shipping_label", "");
			_propertyMapping.Add("shipping_weight", "");
			_propertyMapping.Add("shipping_length", "");
			_propertyMapping.Add("shipping_width", "");
			_propertyMapping.Add("transit_time_label", "");
			_propertyMapping.Add("max_handling_time", "");
			_propertyMapping.Add("min_handling_time", "");
			// Tax", "");
			// These attributes can be used together with the account tax settings to help you provide accurate tax costs in your ads. Learn how to set up account tax settings.", "");
			_propertyMapping.Add("tax", "");
			_propertyMapping.Add("tax_category", "");

		}


		public override string ToString()
		{
			return "Store Front to Googr Feed Map";
		}

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)_propertyMapping).GetEnumerator();
		}
	}
}
