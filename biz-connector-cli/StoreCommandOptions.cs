using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace biz_connector_cli
{



	[Verb("store", HelpText = "Store Help TExt")]
	public class StoreCommandOptions : IStoreOptions
	{
		public List<string> AcceptedCommandScopes => new List<string> { "feed", "store" };
		public Dictionary<string, List<string>> AcceptedCommands => new Dictionary<string, List<string>>
		{
			{ "feed", new List<string> {"full-run"} },
			{ "store", new List<string>{ "list"} }
		};



		public int? StoreId { get; set; }
		public string FeedScope { get; set; }
		public List<string> InputParametersErrors { get; set; }

		public StoreCommandOptions()
		{

		}


		public string CommandScope { get; set; }
		//public string Command { get; set; }
	}


}
