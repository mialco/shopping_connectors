using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront
{
	public class Product
	{
		public Product()
		{
			ProductVariants = new List<ProductVariant>();
			ProductStores = new List<ProductStore>();
			ProductCategories = new List<ProductCategory>();
		}
#pragma warning disable CS0169 // The field 'Product._productVariants' is never used
		private IEnumerable<ProductVariant> _productVariants;
#pragma warning restore CS0169 // The field 'Product._productVariants' is never used
		public int ProductID { get; set; }
		//{ get => base.Id; 
		//	set
		//	{
		//		AssignId(ProductID);
		//	}
		//}
		public string Name { get; set; }
		public string Description { get; set; }
		public string SEDescription { get; set; }
		public string SETitle { get; set; }
		public string SEAltText { get; set; }
		public Guid ProductGUID { get; set; }
		public string ProductColor { get; set; }
		public string Summary { get; set; }
		public string SEName { get; set; }
		public byte Published { get; set; }
		public byte ExcludeFromPriceFeeds { get; set; }
		public byte IsFeatured { get; set; }
		public int ShowBuyButton { get; set; }
		public int ProductTypeID { get; set; }
		public byte IsAKit { get; set; }
		public byte TrackInventoryBySizeAndColor { get; set; }
		public byte RequiresTextOption { get; set; }
		public string ManufacturerPartNumber { get; set; }
		public virtual List<ProductVariant> ProductVariants { get; set; }
		public List<ProductStore> ProductStores { get; set; }
		public virtual List<ProductCategory> ProductCategories { get; set;} 
		public ProductType ProductType { get; set; }
	}
}
