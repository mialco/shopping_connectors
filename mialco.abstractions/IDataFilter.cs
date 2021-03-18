using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	public interface IDataFilter<T>
	{
		string FilterName { get; }
	}
}
