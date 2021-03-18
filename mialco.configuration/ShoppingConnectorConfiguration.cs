using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace mialco.configuration
{
	public class ShoppingConnectorConfiguration
	{
		//Reference: https://makolyte.com/csharp-how-to-read-custom-configuration-from-appsettings-json/
		private static readonly string ConfigurationFile = "ShoppingConnectorConfig.json";
		private static ShoppingConnectorConfiguration _config;
		private static object _lock;
		IConfigurationRoot _configRoot;
		private ApplicationSettings _applicationSettings;

		static ShoppingConnectorConfiguration()
		{
			_config = new ShoppingConnectorConfiguration();
			_lock = new object();
		}

		public static ShoppingConnectorConfiguration GetConfiguration()
		{
			lock (_lock)
			{
				return _config;
			}
		}

		public string GetValue(string key)
		{
			var result = string.Empty;

			_configRoot = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
				AddJsonFile("ShoppingConnectorConfig.json").Build();


			var section = _configRoot.GetSection(nameof(RawFeedBuilderSettings));
			var rawFeedBuilder = section.Get<RawFeedBuilderSettings>();
			result = rawFeedBuilder.DefaultCurrency;
			return result;
		}

		public string GetValue(Sections section, SubSections subSection, ApplicationSettingKeys key)
		{
			var result = string.Empty;

			_configRoot = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
				AddJsonFile("ShoppingConnectorConfig.json").Build();

			switch (section)
			{
				case Sections.ApplicationSettings:

					var appSection = _configRoot.GetSection(nameof(ApplicationSettings));
					var appSetting = appSection.Get<ApplicationSettings>();

					break;
				default:
					break;
			}


			return result;
		}

		public ApplicationSettings GetApplicationSettings()
		{
			//TODO: Test Method for loading application settings
			if (_applicationSettings != null)
				return _applicationSettings;

			_configRoot = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
				AddJsonFile(ConfigurationFile).Build();

			try
			{
				var appSection = _configRoot.GetSection(nameof(ApplicationSettings));
				_applicationSettings = appSection.Get<ApplicationSettings>();
			}
			catch (Exception)
			{
				//todo: Logging
				Console.WriteLine($"Cannot Read [ApplicationSettings] from the {ConfigurationFile} file. Please review the content of the configuration file");
				_applicationSettings = new ApplicationSettings();
			}

			try
			{
				// Getting The Application Instances Settings 
				var instancesSection = _configRoot.GetSection("ApplicationSettings:ApplicationInstances");
				var instancesValues = instancesSection.Get<ApplicationInstanceSettings[]>();
				if (instancesValues == null) throw new Exception($"[ApplicationInstances] section from[ApplicationSettings] of the { ConfigurationFile } file is EMPTY. Please review the content of the configuration file");
				var instancesSettings = new Dictionary<string, ApplicationInstanceSettings>();
				foreach (var item in instancesValues)
				{
					if (!instancesSettings.ContainsKey(item.Name))
					{
						instancesSettings.Add(item.Name, item);
					}
					else
					{
						instancesSettings[item.Name] = item;
					}
				}
				_applicationSettings.ApplicationInstances = new ApplicationSettingsInstances(instancesSettings);
			}
			catch (Exception)
			{
				//todo: Logging
				Console.WriteLine($"Cannot Read [ApplicationInstances] section  from  [ApplicationSettings] from the {ConfigurationFile} file. Please review the content of the configuration file");
			}

			return _applicationSettings;
		}
	}
}
