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

namespace biz_connector_cli
{
	/// <summary>
	/// Runs a storefront full end-to-end full feed
	/// </summary>
	public class StoreFrontFullFeed
	{
		private readonly int _storeId;
		private readonly ShoppingConnectorConfiguration _shoppingConnectorConfiguration;

		public StoreFrontFullFeed(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration)
		{
			this._storeId = storeId;
			_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
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
