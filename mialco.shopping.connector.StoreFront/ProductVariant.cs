using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront
{
	public class ProductVariant
	{
		public ProductVariant()
		{

		}
		public int VariantID { get; set; }
		public Guid VariantGUID { get; set; }
		public decimal Price { get; set; }
		public decimal? SalePrice { get; set; }
		public string SkuSuffix { get; set; }
		public string Colors { get; set; }
		public string ColorSKUModifiers { get; set; }
		public string Sizes { get; set; }
		public string SizeSKUModifiers { get; set; }
		public string ManufacturerPartNumber { get; set; }
		public string Dimensions { get; set; }
		public string GTIN { get; set; }
		public decimal? Weight { get; set; }
		public decimal? MSRP { get; set; }
		public decimal? Cost { get; set; }
		public int IsDefault { get; set; }
		public byte IsDownload { get; set; }
		public byte FreeShipping { get; set; }
		public byte Published { get; set; }
		public byte Condition { get; set; }
		public int ProductID { get; set; }
		public int Inventory { get; set; }
		public virtual Product Product { get; set; }
		
	}
}
