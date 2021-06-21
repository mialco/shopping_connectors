using mialco.configuration;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace biz_connector_cli
{
	class Program
	{
		static void Main(string[] args)
		{
			var errors = new List<Error> { };
			var settings  = ShoppingConnectorConfiguration.GetConfiguration().GetApplicationSettings();
			var logfile = Path.Combine(settings.Folders.LogFolder, settings.Files.LogFile);
			//Configure the logger
			var logger = mialco.utilities.MialcoLogger.GetLogger();
			logger.configure(logfile);
			logger.LogInfo("Shopping connector cli started", "0");
			logger.LogError("Testing logger error", "1");
			logger.LogException(new Exception("Custom exception testing the logging"), "Message Excetion", "-1");
			logger.LogException(new Exception("Custom exception 1 testing the logging"));
			logger.LogWarning("Logging a warning to test the log module", "1");

			Func<IFeedOptions, string> feed = fopts =>
			{

				var fresult = string.Empty;

				if (!fopts.AcceptedCommandScopes.Contains(fopts.CommandScope))
				{
					fresult = "Command scope not found";
					if (fopts.InputParametersErrors == null) fopts.InputParametersErrors = new List<string>();
					var commandsInScopeString = string.Join(",", fopts.AcceptedCommandScopes.ToArray());
					fopts.InputParametersErrors.Add($"***  Command scope received {fopts.CommandScope} is not one of the accepted commands\n\tAccepted commands are: {commandsInScopeString}");
					return fresult;
				}
				//if (fopts.StoreId.HasValue)
				//{
					var applicationSettings = mialco.configuration.ShoppingConnectorConfiguration.GetConfiguration();
					var runFullFeed = new StoreFrontFullFeed(fopts.StoreId, applicationSettings, fopts.InstanceName , logger);
					runFullFeed.Run();

				//}
				//else
				//{
				//	Console.WriteLine("StoreId is mising");
				//	fresult = "StoreId is missing";
				//}



				return fresult;
			};

			Func<IStoreOptions, string> storecmd = sopts =>
			{
				var fresult = string.Empty;
				if (!sopts.AcceptedCommandScopes.Contains(sopts.CommandScope))
				{
					fresult = "Command scope not found";
					if (sopts.InputParametersErrors == null) sopts.InputParametersErrors = new List<string>();
					var commandsInScopeString = string.Join(",", sopts.AcceptedCommandScopes.ToArray());
					sopts.InputParametersErrors.Add($"***  Command scope received {sopts.CommandScope} is not one of the accepted commands\n\tAccepted commands are: {commandsInScopeString}");
					return fresult;
				}
				return fresult;
			};

			// Load Application options 


			var argsErrors = false;
			//Use the Command Line Parser to read the input parametes
			//var result1 = Parser.Default.ParseArguments<FeedCommandOptions>(args);
			//var result2 = Parser.Default.ParseArguments<StoreCommandOptions>(args);
			//var result = Parser.Default.ParseArguments<FeedCommandOptions, StoreCommandOptions>(args);
			var result = Parser.Default.ParseArguments<FeedCommandOptions>(args);
			//result.MapResult((FeedCommandOptions cmdopts) => feed(cmdopts),
			//(StoreCommandOptions sopts) => storecmd(sopts),
			//	_ => MakeError());
			result.MapResult((FeedCommandOptions cmdopts) => feed(cmdopts),
				_ => MakeError());
			//result.WithNotParsed<object>((errs) => CustomErrors(errs, out argsErrors));

			result.WithParsed<FeedCommandOptions>(cmdoption => ParametersErrors(cmdoption.InputParametersErrors, out argsErrors));

			if (argsErrors)
			{
				var newArgs = new List<string> { "", "", "", "" };
				//result = Parser.Default.ParseArguments<FeedCommandOptions,StoreCommandOptions>(newArgs);
				result.MapResult((FeedCommandOptions cmdopts) => feed(cmdopts),
					_ => MakeError());
				//result.MapResult((StoreCommandOptions sopts) => store(sopts), _=>MakeError());
			}

			Console.Write("Press any Key to continue ...");
			Console.Read();
			//return texts.Equals(MakeError()) ? 1 : 0;
		}

		public static void CustomErrors(IEnumerable<Error> errors, out bool argsErrors)
		{
			if (errors == null)
			{
				argsErrors = false;
				return;
			}
			argsErrors = true;
			foreach (var error in errors)
			{
				Console.WriteLine(error.ToString());
			}
		}

		public static void ParametersErrors(IEnumerable<string> errors, out bool argsErrors)
		{

			if (errors == null)
			{
				argsErrors = false; return;
			}
			argsErrors = true;
			foreach (var error in errors)
			{
				Console.WriteLine(error);
			}
		}

		public static string MakeError() { return "Error"; }
	}
}
