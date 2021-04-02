using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace biz_connector_cli
{
	public interface IStoreOptions
	{
			List<string> AcceptedCommandScopes { get; }
			Dictionary<string, List<string>> AcceptedCommands { get; }
			List<string> InputParametersErrors { get; set; }

			//[Option(Required = false, SetName ="StoreId", HelpText = "Executes Feed Related commands and queries", MetaValue = "list")]
			string CommandScope { get; set; }

			//[Option('s', "store-id", SetName = "StoreId", Required = false, MetaValue = "INTEGER", HelpText = "Store Id for which the feed to be executed")]
			int? StoreId { get; set; }

	}
}
