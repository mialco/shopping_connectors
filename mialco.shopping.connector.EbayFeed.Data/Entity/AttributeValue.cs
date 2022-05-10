using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.EbayFeed.Data.Entity
{
	public class AttributeValue
	{
		int Id { get; set; }
		/// <summary>
		/// Foreign Key in AttributeDefinition
		/// </summary>
		int AttributeId { get; set; }
		string ChannelId { get; set; }

	}
}
