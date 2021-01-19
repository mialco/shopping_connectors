using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront
{
	public class ProductType
	{
		public int ProductTypeID { get; set; }
		public string Name { get; set; }
		public  Product Product { get; set; }
	}
}
