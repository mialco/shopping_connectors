using mialco.shopping.connector.frontstore;
using mialco.shopping.connector.intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.repositories
{
	class StoreFrontRepository : IRepository<ProductStoreFrontEntity>
	{
		public ProductStoreFrontEntity Get(long id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ProductStoreFrontEntity> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
