﻿using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	public class SizeOptionsFactory : ProductAttributeAbstractFactory
	{
		public override ProductAttribute GetAttribute()
		{
			return new SizeOption();
		}
	}

}