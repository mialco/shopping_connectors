using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.intefaces
{
	public interface IProductRepository<T>
	{
		IEnumerable<T> GetAll(int storeId);
		IEnumerable<T> GetAllFilteredByCategory(int storeId, IEnumerable<int> categories);
		T GetById(int productId);
	}
}
