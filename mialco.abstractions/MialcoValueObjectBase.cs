using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	public abstract class MialcoValueObjectBase
	{
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public abstract override string ToString();

	}
}
