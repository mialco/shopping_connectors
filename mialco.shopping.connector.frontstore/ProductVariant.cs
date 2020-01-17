using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	public class ProductVariant : Entity
	{
		public int VariantID { get; set; }
		public int ProductID { get; set; }
		public decimal Price { get; set; }
		public decimal? SalePrice { get; set; }
		public string SkuSuffix { get; set; }
		public string ColorSKUModifiers { get; set; }
		public string SizeSKUModifiers { get; set; }
		public virtual Product Product {get; set;}
	}
}
