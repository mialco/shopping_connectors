using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	public abstract class ProductAttribute
	{
		public int Priority { get; }
		public string Name { get; set; }
		public decimal AddedPrice { get; set; }
		public string SkuModifier { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var obj1 = obj as ProductAttribute;
			return (obj1.Name == this.Name
				&& obj1.Priority == this.Priority
				&& obj1.SkuModifier == this.SkuModifier
				&& obj1.AddedPrice == this.AddedPrice
				);
		}

		public override int GetHashCode()
		{
			const int p = 29;
			int hash = p;
			hash = hash * p + AddedPrice.GetHashCode();
			hash = hash * p + (Name ?? "").GetHashCode();
			hash = hash * p + (SkuModifier ?? "").GetHashCode();
			hash = hash * p + Priority.GetHashCode();
			return hash;
		}
	}

}
