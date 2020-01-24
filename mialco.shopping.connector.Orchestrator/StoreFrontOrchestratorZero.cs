using mialco.abstractions;
using mialco.shopping.connector.StoreFront;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.Orchestrator
{
	public class StoreFrontOrchestratorZero: IRunable
	{
		private IEnumerable<Product> _products;
		private readonly int _storeId;

		public StoreFrontOrchestratorZero(int storeId)
		{
			_storeId = storeId;
		}


		//Extracts data from the storefront database
		private void  ExtractData(int storeId)
		{
			StoreFrontStoreRepositoryEF sfsr = new StoreFrontStoreRepositoryEF();
			var store = sfsr.GetById(storeId);
			var prodrep = new StoreFrontProductRepositoryEF();
			_products = prodrep.GetAll(storeId);
		}

		private void ExportRawData()
		{
		}

		public int Run()
		{
			ExtractData(7);
			ExportRawData();
			return 0;
		}
	}
}
