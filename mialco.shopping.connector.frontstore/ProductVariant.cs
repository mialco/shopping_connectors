using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	public class ProductVariant //: Entity
	{
		public ProductVariant()
		{
	
		}
		public int VariantID { get; set; }
		public Guid VariantGUID { get; set; }
		public decimal Price { get; set; }
		public decimal? SalePrice { get; set; }
		public string SkuSuffix { get; set; }
		public string ColorSKUModifiers { get; set; }
		public string SizeSKUModifiers { get; set; }

		public int ProductID { get; set; }
		public virtual Product Product {get; set;}
	}
}
