using GoogleAdFeed;
using mialco.shopping.connector.RawFeed;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.GoogleAdFeed
{
	/// <summary>
	/// GIven a raw Feed shoudl create file with the proper format for google ad 
	/// </summary>
	public class FeedGenerator
	{

		public void GenerateXmlFeed(string FileName, List<GenericFeedRecord> feedRecords, FeedProperties feedProerties)
		{
			const string GooglePrefix = "g";
			const string GoogleNamepace = @"http://base.google.com/ns/1.0";
			using (GoogleXmlFileWriter xmlw = new GoogleXmlFileWriter(@"c:\data\temp.xml"))
			{
				xmlw.OpenFeed(GooglePrefix,GoogleNamepace);
				xmlw.StartItem();
				xmlw.WriteItemElement("id", "306-202",GooglePrefix,GoogleNamepace);
				xmlw.WriteItemElement("title", "New Bride and Groom Wedding Baseball Caps-Black", GooglePrefix, GoogleNamepace);
				xmlw.CloseFeed();
			}
		}




	}
}
