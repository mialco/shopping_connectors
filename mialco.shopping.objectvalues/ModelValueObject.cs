using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.objectvalues
{
	public abstract class ModelValueObject<T> where T: ModelValueObject<T>
	{
		public override bool Equals(object obj)
		{
			
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
