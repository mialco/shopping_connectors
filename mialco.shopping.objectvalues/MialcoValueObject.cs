﻿using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.objectvalues
{
    public abstract class MialcoValueObject
    {
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public  override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public  abstract override string ToString();
	}
}