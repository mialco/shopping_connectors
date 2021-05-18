using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	public abstract class ProductAttributeAbstractFactory
	{
		abstract public ProductAttribute GetAttribute();
	}

}
