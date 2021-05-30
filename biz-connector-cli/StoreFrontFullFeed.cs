using System.Text.Json.Serialization;
using System.Text.Json;
using mialco.configuration;
using mialco.shopping.connector.GoogleAdFeed;
using mialco.shopping.connector.Orchestrator;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.shared;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;
using mialco.workflow.initiator;
using mialco.workflow.manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;

namespace biz_connector_cli
{
	/// <summary>
	/// Runs a storefront full end-to-end full feed
	/// </summary>
	public class StoreFrontFullFeed
	{
		private readonly int _storeId;
		private readonly ShoppingConnectorConfiguration _shoppingConnectorConfiguration;
		ApplicationInstanceSettings _appInstanceSettings;
		ApplicationSettings _appSettings;
		IdentifiersFilters _identifiersFilters;
		WebStoreDeploymentType _deploymentType;
		private bool _isConfigValid = true;
		private StringBuilder _runtimeMessage = new StringBuilder();

		
		

		public StoreFrontFullFeed(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration , string appInstanceName)
		{
			string thisMethod = "StoreFrontFullFeed.Constructor";
			this._storeId = storeId;
			_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
			_appSettings = _shoppingConnectorConfiguration.GetApplicationSettings();						
			LoadInstanceSettings(appInstanceName);
			if (!_isConfigValid) return;
			GetFilters();
			var orch = new StoreFrontOrchestratorZero(_storeId,_appSettings,_appInstanceSettings, _identifiersFilters);

			//Early validation Validation of the configurations, to make sure that they are loaded
			var appSettingsLoaded = _appSettings != null;
			var appInstanceSettingsLoaded = _appInstanceSettings != null;
			Console.WriteLine($"[{thisMethod}] appSetings Loaded: {appSettingsLoaded}  ");
			Console.WriteLine($"[{thisMethod}] app Instance Seting sLoaded: {appInstanceSettingsLoaded}  ");

			if (appSettingsLoaded)
			{
				Console.WriteLine($"[{thisMethod}] InputFolder: {_appSettings.Folders.InputFolder}  ");
				Console.WriteLine($"[{thisMethod}] OutputFolder: {_appSettings.Folders.OutputFolder}  ");
			}

			if (appInstanceSettingsLoaded)
			{
				Console.WriteLine($"[{thisMethod}] ConnectionString: {_appInstanceSettings.ConnecttionString ?? "NULL" }  ");
				Console.WriteLine($"[{thisMethod}] OutputFolder: {_appInstanceSettings.DeploymentType?? "NULL"}  ");
			}

			//orch.Run();
		}

		//public StoreFrontFullFeed(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration, string appInstanceName, string filterJSON)
		//{
		//	this._storeId = storeId;
		//	_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
		//	var appSettings = _shoppingConnectorConfiguration.GetApplicationSettings();
		//	var hasInstanceSettings = appSettings.ApplicationInstances.TryGetValue(appInstanceName, out _appInstanceSettings);
		//	// ToDo: To be continued
		//}




		private class Filters
		{
			public List<int> CategoryIds { get; set; }
		}


		private void LoadInstanceSettings(string appInstanceName)
		{
			var hasInstanceSettings = _appSettings.ApplicationInstances.TryGetValue(appInstanceName, out _appInstanceSettings);
			if (!hasInstanceSettings)
			{
				_runtimeMessage.Append($"The configuration file does not contain an instance setting matching the input parameter [{appInstanceName}");
				_isConfigValid = false;
			}
		}
		
		
		/// <summary>
		/// By Convention, filters are located in the Input folder and has the name <InstanceName>_<StoreId>_Filters.json
		/// </summary>
		private void GetFilters()
		{

			if (_appInstanceSettings == null) return;
			var filterFileName = $"{_appInstanceSettings.Name.ToLower()}_{_storeId.ToString()}_Filters.json";
			var filterFullPath = Path.Combine(_appSettings.Folders.InputFolder, filterFileName);
			if (File.Exists(filterFullPath))
			{
				var filtersString = File.ReadAllText(filterFullPath);
				_identifiersFilters = new IdentifiersFilters(filterFileName);
				var filters = JsonSerializer.Deserialize<Filters>(filtersString);
				if (filters.CategoryIds != null && filters.CategoryIds.Count > 0)
				{
					_identifiersFilters.AddFilter("Categories", filters.CategoryIds);
				}

			}
		}

		public void Run()
		{

			//var storeId = 33; //wickedTees
			//storeId = 1; //irosepetals

			if (_isConfigValid)
			{
				var orch = new StoreFrontOrchestratorZero(_storeId, _appSettings, _appInstanceSettings, _identifiersFilters);

				orch.RunAllActionsInOneBigLoop(_storeId, _appSettings, _appInstanceSettings, _identifiersFilters);
			}
			//todo: Write to log console
			if (_runtimeMessage.Length>0) Console.WriteLine(_runtimeMessage.ToString());

		}

	}
}
