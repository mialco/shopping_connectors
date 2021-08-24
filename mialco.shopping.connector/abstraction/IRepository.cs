using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.abstraction
{
	public interface IRepository<T>
	{
		T GetById(long id);
		IEnumerable<T> GettAll();

		T Insert(T item);

		bool Update(T item);

	}
}
