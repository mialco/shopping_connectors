using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed
{
	public class GenericFeedRecord
	{
		public GenericFeedRecord()
		{
			FeedRecord = new Dictionary<string, string>();
		}
		public int Id { get; set; }
		public string ProductId { get; set; }
		public Dictionary<string, string> FeedRecord { get;set;}


		public string GetFeedValue( string key)
		{
			var result = string.Empty;

			if (string.IsNullOrEmpty(key)) return string.Empty;
			try
			{
				if (!FeedRecord.TryGetValue(key, out result))
				{
					result = string.Empty;
					//todo: Logging of the error
				}
				result ??= string.Empty;
			}
			catch (Exception)
			{

				//todo: add logging
			}
			return result;
		}

	}



	public interface IRawFeedRepository
	{
		IEnumerable<GenericFeedRecord> GetAll();
		void Save(GenericFeedRecord record);
		void SaveAll(IEnumerable<GenericFeedRecord> records);
	}

	public class RawFeedRepository : IRawFeedRepository
	{
		public IEnumerable<GenericFeedRecord> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Save(GenericFeedRecord record)
		{
			throw new NotImplementedException();
		}

		public void SaveAll(IEnumerable<GenericFeedRecord> records)
		{
			throw new NotImplementedException();
		}
	}

}
