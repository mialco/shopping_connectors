using mialco.shopping.connector.entities;
using mialco.shopping.connector.entities.abstraction;
using mialco.shopping.connector.intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	//Book
	public  class Product : Entity
	{	
		public int ProductID { get; set; }
		public Guid ProductGUID { get; set; }
		public string ProductColor { get; set; }
		public string Summary { get; set; }
		public byte Published { get; set; }
		public byte ExcludeFromPriceFeeds { get; set; }
		public virtual ICollection<ProductVariant> ProductVariants { get; set; }
		public virtual ICollection<ProductStore> ProductStores { get; set; }

	}
}
