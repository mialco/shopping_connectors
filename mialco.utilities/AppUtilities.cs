using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mialco.utilities
{
	public static class AppUtilities
	{
		private const string AppDataRoot = "ShoppingConnectorFeed"; //TODO: Read From Config

		public static IContainer DiContainer {get;set;}
		public static string GetApplicationDataPath()
		{
			//todo: Check if this is used by anything as most likely we are gitting the appliction folders from configurations. Remove it if not in use
			var result = string.Empty;
			try
			{
				var sysAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
				var appDataFolder = Path.Combine(sysAppDataFolder, AppDataRoot);
				if (!Directory.Exists(appDataFolder))
				{
					Directory.CreateDirectory(appDataFolder);
				}
				result = appDataFolder;
				return result;
			}
			catch (Exception )
			{
				throw;
			}
		}
	}
}
