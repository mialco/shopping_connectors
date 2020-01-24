using mialco.shopping.connector.entities;
using mialco.shopping.entities.abstraction;
using mialco.shopping.connector.intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	//Book
	public  class Product //: Entity
	{
		public Product()
		{
			ProductVariants =  new List<ProductVariant>();
			ProductStores = new List<ProductStore>();
		}
		private IEnumerable<ProductVariant> _productVariants;
		public int ProductID { get; set; }
		//{ get => base.Id; 
		//	set
		//	{
		//		AssignId(ProductID);
		//	}
		//}
		public Guid ProductGUID { get; set; }
		public string ProductColor { get; set; }
		public string Summary { get; set; }
		public byte Published { get; set; }
		public byte ExcludeFromPriceFeeds { get; set; }
		public virtual List<ProductVariant> ProductVariants { get; set; } 
		public List<ProductStore> ProductStores { get; set; }

	}
}
