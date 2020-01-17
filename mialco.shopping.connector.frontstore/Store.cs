using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	//Category
	public class Store: Entity
	{
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
		public virtual ICollection<ProductStore> ProductStores { get; set; }
	}

}
