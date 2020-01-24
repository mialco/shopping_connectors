using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore.Abstraction
{
	public interface IProductRepository<T> //where T : Entity
	{
		IEnumerable<T> GetAllInStore(int StoreId);
	}
}
