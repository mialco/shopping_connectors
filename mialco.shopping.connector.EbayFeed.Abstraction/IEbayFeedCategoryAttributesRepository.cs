using System;
using System.Collections.Generic;

namespace mialco.shopping.connector.EbayFeed.Abstraction
{
	public interface IEbayFeedCategoryAttributesRepository
	{
		public IEnumerable<string> GetAttributes(int categoryId);
		public bool AddAttribute(int categoryId, string attribute);
		public bool AddAttributes(int categotyId, IEnumerable<string> attributes);
		public bool HasCategory(int categoryId);
		public IEnumerable<int> ExistingCategories();
		public bool InsertAttributeName(string attributeName );
		public bool InsertAttributeName	(IEnumerable<string> attributeNames);
		public bool InsertChannelName(string channelName);
		public bool InsertChannelName(IEnumerable<string> channelNames);
	}
}
