using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.objectvalues
{
	//todo: Remove this if it has  no reference. 
	//It was created just to be used as a model for other classes
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
