using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront
{
	public class ProductCategory
	{
		public int ProductID { get; set; }
		public int CategoryID { get; set; }
		public virtual Product Product { get; set; }
	}
}
