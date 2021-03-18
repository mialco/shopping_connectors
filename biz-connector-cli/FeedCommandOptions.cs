using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz_connector_cli
{

	[Verb("feed",HelpText="Scope feed")]
	public class FeedCommandOptions : IFeedOptions
	{
		public List<string> AcceptedCommandScopes => new List<string> { "full-feed" };
		public Dictionary<string, List<string>> AcceptedCommands => new Dictionary<string, List<string>>
		{
			{ "feed", new List<string> {"full-run"} },
			{ "store", new List<string>{ "list"} }
		};



		public int? StoreId { get ; set; }
		public string FeedScope { get ; set ; }
		public List<string> InputParametersErrors { get; set; }

		public FeedCommandOptions()
		{

		}
	[CommandLine.Text.Usage(ApplicationAlias = "biz-connector-cli.exe")]
		public static IEnumerable<CommandLine.Text.Example> Examples
		{
			get
			{
				yield return new Example("Usage scenario", new FeedCommandOptions { FeedScope = "feed", Command="full-run", StoreId = 33 });
				//yield return new Example("List Stores", new StoreCommandOptions { FeedScope = "store", Command = "list" });
			}
		}

		public string CommandScope { get ; set ; }
		public string Command { get ; set ; }
	}

	[Verb("store", HelpText ="Store HElp TExt" )]
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
