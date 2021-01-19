using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mialco.utilities
{
	public static class AppUtilities
	{
		private const string AppDataRoot = "ShoppingConnectorFeed"; //TODO: Read From Config

		public static string GetApplicationDataPath()
		{
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
