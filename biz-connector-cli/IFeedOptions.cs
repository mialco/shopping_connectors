using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz_connector_cli
{
	public interface IFeedOptions
	{
		List<string> AcceptedCommandScopes { get; }
		Dictionary<string,List<string>> AcceptedCommands { get; }
		List<string> InputParametersErrors { get; set; }

		[Value(0, Required = true, HelpText = "Executes Feed Related commands and queries", MetaName = "Command scope", MetaValue = "full-run")]
		string Command { get; set; }

		[Value(1, Required = true, HelpText = "Executes Feed Related commands and queries", MetaName = "Command scope", MetaValue ="feed metavalue"  )]
		string CommandScope { get; set; }


		[Option('i', "instance-name", SetName = "Feed", Required = true, MetaValue = "STRING", HelpText = "Installation instance  for which the feed to be executed")]
		string InstanceName { get; set; }

		[Option('s', "store-id", SetName = "Feed",Required = true, MetaValue = "INTEGER", HelpText = "Store Id for which the feed to be executed")]
		int StoreId { get; set; }

	}

}
