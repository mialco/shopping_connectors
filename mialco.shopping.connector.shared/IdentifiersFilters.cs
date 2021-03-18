using System;
using System.Collections.Generic;
using System.Text;
using mialco.abstractions;

namespace mialco.shopping.connector.shared
{
	public class IdentifiersFilters : IDataFilterValues<int>
	{
		List<int> _identifiers;
		string _filterName;

		public IdentifiersFilters(string filterName)
		{
			_filterName = filterName;
			_identifiers = new List<int>();
		}

		public IEnumerable<int> Values => _identifiers;

		public string FilterName => _filterName;
	}
}
