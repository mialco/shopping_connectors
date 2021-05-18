using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	public class ColorOptionsFactory : ProductAttributeAbstractFactory
	{
		public override ProductAttribute GetAttribute()
		{
			return new ColorOption();
		}
	}
}
