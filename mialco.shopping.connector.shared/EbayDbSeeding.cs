using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.shared
{
	/// <summary>
	/// This class is to be used when application starts to ensure that the Ebay Database exists and it is seeded
	/// </summary>
	public class EbayDbSeeding
	{
		private readonly string _connectionString;

		public EbayDbSeeding(string connectionString)
		{
			this._connectionString = connectionString;
		}

		public void AtributeNamesSeeding()
		{
			var repo = new EbayFeed.Data.EbayFeedCategoryAttributesRepositoryDbLite(_connectionString);
			repo.InsertAttributeName("Brand");
			repo.InsertAttributeName(new List<string> {"Brand",
"Model",
"Power Source",
"Type",
"Color",
"Power",
"Energy Star",
"Manufacturer Warranty",
"MPN",
"Bundle Description",
"Custom Bundle",
"Voltage Country/Region of Manufacture",
"Material",
"Manufacturer Color",
"California Prop 65 Warning",
"EC Range"
 });
		
		}

		public void EbayChannelSeeding() 
		{
			var repo = new EbayFeed.Data.EbayFeedCategoryAttributesRepositoryDbLite(_connectionString);
			repo.InsertChannelName(new List<string> {


			"EBAY_AU",
"EBAY_AT",
"EBAY_BE_FR",
"EBAY_BE_NL",
"EBAY_CA",
"EBAY_CH",
"EBAY_DE",
"EBAY_ES",
"EBAY_FR",
"EBAY_HK",
"EBAY_IN",
"EBAY_IT",
"EBAY_MY",
"EBAY_NL",
"EBAY_PH",
"EBAY_PL",
"EBAY_RU",
"EBAY_UK",
"EBAY_US",
"EBAY_Motors" });

		
		}
	}
}
