using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.EbayFeed.Data.Entity
{
	/// <summary>
	/// The Ebay Attributes are specified here in "Category Metadata Feed Definitions":
	/// https://developer.ebay.com/devzone/merchant-products/mipng/user-guide-en/default.html#definitions-category-metadata-feed.html?TocPath=Inventory%2520management%257CFeed%2520definitions%257C_____9
	/// This defines what an attribute contains and what are the allowed values for specific attributes
	/// The attributes are specific for each category.  So the attribute definition key will be the categoryID
	/// 
	/// </summary>
	public class AttributeDefinition
	{
		public int AttributeId { get; set; }
		public int CategoryId { get; set; }
		public string ChannelId { get; set; }
		/// <summary>
		/// The name of the attribute
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Represents how the attribute is used in data processing 
		/// The enumeration value returned in this field will indicate if the corresponding aspect 
		/// is required, preferred (recommended, especially for optimizing your listing showing up in search results), 
		/// or optional.
		/// The recommended values are REQUIRED PREFERRED OPTIONAL 
		/// and these values will be listed in the table [Usage]
		/// </summary>
		public string Usage { get; set; }
		/// <summary>
		/// The expected date after which the aspect will be required.
		/// Note: The value returned in this field specifies only an approximate date, 
		/// which may not reflect the actual date after which the aspect is required.
		/// </summary>
		public string ExpectedRequiredByDate { get; set; }

		public IEnumerable<AttributeValue> AttributeValues { get; set; }

		public string MessageType { get; set; }
		public string MessageId { get; set; }
		public string Message { get; set; }

	}
}
