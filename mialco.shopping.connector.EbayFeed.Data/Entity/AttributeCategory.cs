using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.EbayFeed.Data.Entity
{
	/// <summary>
	/// To store the master list of the categories for which we have defined attributes
	/// </summary>
	public 	class AttributeCategory
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }

	}
}
