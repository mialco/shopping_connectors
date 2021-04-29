using biz_connector_cli;
using System;
using System.IO;

namespace testconsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var p = Path.Combine("MyName", ".txt");
			var files = Directory.GetFiles("./");
			foreach (var file in files)
			{

				Console.WriteLine(file);
			}

			var applicationSettings = mialco.configuration.ShoppingConnectorConfiguration.GetConfiguration();
			var runFullFeed = new StoreFrontFullFeed(30, applicationSettings, "StoreFront-Dev");
			runFullFeed.Run();

			Console.WriteLine("Press any key to finish");
			Console.ReadLine();
		}
	}
}
