
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using mialco.shopping.connector.frontstore.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace mialco.shopping.connector.frontstore.repositories
{
	public class ProductReporsitoryEF : IProductRepository<Product>
	{
		public IEnumerable<Product> GetAllInStore(int storeId)
		{
			using (var ctx = new StoreFrontDbContext())
			{
				var variants = ctx.ProductVariant.Where(v => v.ProductID == 16).Take(100).ToList();

				var prods = ctx.Product
					.Where(x => x.Published > 0
					  && x.ProductStores.Any(ps => ps.StoreID == storeId 
					  ))
					  .Take(1000)
						//.Include(p => p.ProductVariants)
					  .ToList();
				return prods;
			}
		}
	}
}

