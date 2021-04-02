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

		public StoreFrontFullFeed(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration , string appInstanceName)
		{
			this._storeId = storeId;
			_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
			_appSettings = _shoppingConnectorConfiguration.GetApplicationSettings();			
			var hasInstanceSettings = _appSettings.ApplicationInstances.TryGetValue(appInstanceName, out _appInstanceSettings);
			GetFilters();
			var orch = new StoreFrontOrchestratorZero(_storeId,_appSettings,_appInstanceSettings, _identifiersFilters);

			orch.Run();
		}

		public StoreFrontFullFeed(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration, string appInstanceName, string filterJSON)
		{
			this._storeId = storeId;
			_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
			var appSettings = _shoppingConnectorConfiguration.GetApplicationSettings();
			var hasInstanceSettings = appSettings.ApplicationInstances.TryGetValue(appInstanceName, out _appInstanceSettings);
			// ToDo: To be continued
		}


		private class Filters
		{
			public List<int> CategoryIds { get; set; }
		}
		/// <summary>
		/// By Convention, filters are located in the Input folder and has the name <InstanceName>_<StoreId>_Filters.json
		/// </summary>
		private void GetFilters()
		{

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

			var orch = new StoreFrontOrchestratorZero(_storeId, WebStoreDeploymentType.Production);

			orch.Run();

		}

	}
}
