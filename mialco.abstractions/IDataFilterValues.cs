using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	public interface IDataFilterValues <T> : IDataFilter<T>
	{
		IEnumerable<T> Values { get; }
	}
}
