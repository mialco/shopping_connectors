using mialco.shopping.objectvalues;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAdFeed
{
	public class StoreFrontGoogleFeedMap : MialcoValueObject
	{
		Dictionary<string, string> _propertyMapping;
		
		/// <summary>.HasPrincipalKey(pk => pk.ProductID).
		/// Instantiates the Class
		/// Instantiated the mapping values as a dictionary having the 
		/// keys the feed properties and as values the name of the source table 
		/// properties correpondent to the feed field
		/// </summary>
		public StoreFrontGoogleFeedMap():base()
		{
			_propertyMapping = new Dictionary<string, string>();
			DictionaryInit();
		}

		private void DictionaryInit()
		{
			_propertyMapping.Add("id", "SKU");
			_propertyMapping.Add("description", "");
			_propertyMapping.Add("link", "");
			_propertyMapping.Add("image_link", "");
			_propertyMapping.Add("additional_image_link", "");
			_propertyMapping.Add("mobile_link", "");
			_propertyMapping.Add("availability", "");
			_propertyMapping.Add("availability_date", "");
			_propertyMapping.Add("cost_of_goods_sold", "");
			_propertyMapping.Add("expiration_​​date", "");
			_propertyMapping.Add("price", "Price");
			_propertyMapping.Add("sale_​​price", "SalePrice");
			_propertyMapping.Add("sale_​​price_​​effective_​​date", "");
			_propertyMapping.Add("unit_​​pricing_​​measure", "");
			_propertyMapping.Add("unit_​​pricing_​​base_​​measure", "");
			_propertyMapping.Add("installment", "");
			_propertyMapping.Add("subscription_​​cost", "");
			_propertyMapping.Add("loyalty_​​points", "");

			//"Product category", "");
			//"You can use these attributes to organize your advertising campaigns in Google Ads.", "");

			_propertyMapping.Add("google_​​product_​​category", "");
			_propertyMapping.Add("product_type", "");

			//"Product identifiers", "");

			_propertyMapping.Add("brand", "");
			_propertyMapping.Add("gtin", "");
			_propertyMapping.Add("MPN", "");
			_propertyMapping.Add("identifier_​​exists", "");

			//"Detailed product description", "");
			_propertyMapping.Add("condition", "");
			_propertyMapping.Add("adult", "");
			_propertyMapping.Add("multipack", "");
			_propertyMapping.Add("is_​​bundle", "");
			_propertyMapping.Add("energy_​​efficiency_​​class", "");
			_propertyMapping.Add("min_energy_​​efficiency_​​class", "");
			_propertyMapping.Add("max_energy_​​efficiency_​​class", "");
			_propertyMapping.Add("age_​​group", "");
			_propertyMapping.Add("color", "");
			_propertyMapping.Add("gender", "");
			_propertyMapping.Add("material", "");
			_propertyMapping.Add("pattern", "");
			_propertyMapping.Add("size", "");
			_propertyMapping.Add("size_​​type", "");
			_propertyMapping.Add("size_​​system", "");
			_propertyMapping.Add("item_​​group_​​id", "");

			//"Shopping campaigns and other configurations", "");
			//"These attributes are used to control how your product data is used when you create advertising campaigns in Google Ads.", "");
			_propertyMapping.Add("ads_​​redirect", "");
			_propertyMapping.Add("custom_​​label_​​0", "");
			_propertyMapping.Add("promotion_​​id", "");

			//"Destinations", "");
			//"These attributes can be used to control the type of ads your products participate in. For example, you could use this attribute if you want a product to appear in a dynamic remarketing campaign, but not in a Shopping ads campaign.", "");
			_propertyMapping.Add("excluded_​​destination", "");
			_propertyMapping.Add("included_​​destination", "");

			//("Shipping", "");
			_propertyMapping.Add("shipping", "");
			_propertyMapping.Add("shipping_​​label", "");
			_propertyMapping.Add("shipping_​​weight", "");
			_propertyMapping.Add("shipping_​​length", "");
			_propertyMapping.Add("shipping_​​width", "");
			_propertyMapping.Add("shipping_​​height", "");
			_propertyMapping.Add("transit_time_label", "");
			_propertyMapping.Add("max_handling_time", "");
			_propertyMapping.Add("min_handling_time", "");
			// Tax", "");
			// These attributes can be used together with the account tax settings to help you provide accurate tax costs in your ads. Learn how to set up account tax settings.", "");
			_propertyMapping.Add("tax", "");
			_propertyMapping.Add("tax_category", "");

		}

		public string GetSourceProperty(string key)
		{
			if (string.IsNullOrEmpty(key)) return null;
			if (_propertyMapping.ContainsKey(key))
				return _propertyMapping[key];
			else
				return null;
		}

		public override string ToString()
		{
			return "Store Front to Googr Feed Map";
		}
	}
}
