using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.GoogleAdFeed
{	
	public class FeedProperties
	{
		public FeedProperties(string title, string link, string description)
		{
			Title = title??"";
			Link = link??"";
			Description = description??"";
		}

		public string Title { get; }
		public string Link { get; }
		public string Description { get; }
	}
}
