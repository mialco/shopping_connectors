using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront
{
	public class ProductStore
	{
		public int ProductID { get; set; }
		public Product Product { get; set; }
		public int StoreID { get; set; }
		public Store1 Store { get; set; }
	}
}
