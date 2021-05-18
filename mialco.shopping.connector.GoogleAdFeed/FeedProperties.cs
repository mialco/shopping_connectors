using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.GoogleAdFeed
{	
	public class FeedProperties
	{
		public FeedProperties(string title, string link, string description, string xmlPrefix, string  xmlNamespace)
		{
			Title = title??"";
			Link = link??"";
			Description = description?? string.Empty;
			XmlFeedPrefix = xmlPrefix ?? string.Empty;
			XmlFeedNameSpace = xmlNamespace ?? string.Empty;
		}

		public string Title { get; }
		public string Link { get; }
		public string Description { get; }
		public string XmlFeedPrefix { get; }
		public string XmlFeedNameSpace { get; }
	}
}
