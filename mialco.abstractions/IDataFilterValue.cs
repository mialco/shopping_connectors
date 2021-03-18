using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	public interface IDataFilterValue <T>: IDataFilter<T>
	{
		T FilterValue { get; }
	}
}
