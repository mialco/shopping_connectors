using mialco.shopping.connector.EbayFeed.Abstraction;
using System;
using System.Collections.Generic;
using LiteDB;
using mialco.shopping.connector.EbayFeed.Data.Entity;
using System.Linq;

namespace mialco.shopping.connector.EbayFeed.Data
{

	public class EbayFeedCategoryAttributesRepositoryDbLite : IEbayFeedCategoryAttributesRepository
	{
		private string _connectionString ;
		private const string AttributeNameCollection = "AttributeNameCollection";
		private const string ChannelCollection = "ChannelCollection";
		public EbayFeedCategoryAttributesRepositoryDbLite(string connectionString)
		{
			_connectionString = connectionString;
		}
		public bool AddAttribute(int categoryId, string attribute)
		{
			throw new NotImplementedException();
		}

		public bool InsertAttributeName(string attributeName)
		{
			var result = false;
			if (string.IsNullOrEmpty(attributeName)) return result;
			//Establishing the connection to the database
			using (var db = new LiteDatabase(_connectionString)) 
			{
				var collection = db.GetCollection<AttributeName>(AttributeNameCollection);
				var records = collection.Find(c => c.Name == attributeName);
				if (records.Count() == 0)
				{
					collection.Insert(new AttributeName { Name = attributeName});
					result = true;
					db.Commit();
				}
			}
			return result;
		}

		public bool InsertAttributeName(IEnumerable<string> attributeNames)
		{
			var result = false;
			if (attributeNames == null || attributeNames.Count() == 0) return result;
			foreach (var attributeName in attributeNames) InsertAttributeName(attributeName);
			return result;
		}


		public bool AddAttributes(int categotyId, IEnumerable<string> attributes)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<int> ExistingCategories()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetAttributes(int categoryId)
		{
			throw new NotImplementedException();
		}

		public bool HasCategory(int categoryId)
		{
			throw new NotImplementedException();
		}

		public bool InsertChannelName(string channelName)
		{
			var result = false;
			if (string.IsNullOrEmpty(channelName)) return result;
			//Establishing the connection to the database
			using (var db = new LiteDatabase(_connectionString))
			{
				var collection = db.GetCollection<EbayChannel>(ChannelCollection);
				var records = collection.Find(c => c.ChannelId == channelName);
				if (records.Count() == 0)
				{
					collection.Insert(new EbayChannel { ChannelId = channelName });
					result = true;
				}
				db.Commit();
			}
			return result;
		}

		public bool InsertChannelName(IEnumerable<string> channelNames)
		{
			var result = false;
			if (channelNames == null || channelNames.Count() == 0) return result;
			foreach (var channelName in channelNames) InsertChannelName(channelName);
			return result;

		}
	}
}
