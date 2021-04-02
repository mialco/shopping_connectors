using System;
using System.Collections.Generic;
using System.Text;
using mialco.abstractions;
using System.Linq;

namespace mialco.shopping.connector.shared
{
	public class IdentifiersFilters : IDataFilterValues<int>
	{
		List<int> _identifiers;
		string _filterName;
		Dictionary<string, IEnumerable<int>> _identifiersFilter;

		public IdentifiersFilters(string filterName)
		{
			_filterName = filterName;
			_identifiers = new List<int>();
			_identifiersFilter = new Dictionary<string, IEnumerable <int>>();
		}

		public IEnumerable<int> Values => _identifiers;

		public string FilterName => _filterName;

		public bool AddFilter(string filterName, IEnumerable<int> filter)
		{
			if (!_identifiersFilter.ContainsKey(filterName))
			{
				_identifiersFilter.Add(filterName, filter);

			}
			else
			{
				_identifiersFilter[filterName] = filter;
			}
			return true;
		}

		public IEnumerable<int> GetFilter(string filterName)
		{
			if (!_identifiersFilter.ContainsKey(filterName))
			{
				return null;
			}
			else
			{
				return _identifiersFilter[filterName];
			}
		}
	}
}
