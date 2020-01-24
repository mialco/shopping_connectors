using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	
	public class Store //: Entity
	{
		public Store()
		{
			ProductStores = new List<ProductStore>();
		}
		public int StoreID { get; set; }
		public string ProductionURI { get; set; }
		public string StagingURI { get; set; }
		public string DevelopmentURI { get; set; }
		public byte Published { get; set; }
		public string ProductionDirectoryPath { get; set; }
		public string StagingDirectoryPath { get; set; }
		public string DevelopmentDirectoryPath { get; set; }
		public string ProductionPort { get; set; }
		public string StagingPort { get; set; }
		public string DevelopmentPort { get; set; }
		public List <ProductStore> ProductStores { get; set; }
	}

}
