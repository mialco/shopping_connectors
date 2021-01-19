using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;



namespace mialco.shopping.connector.StoreFront
{
	public class StoreFrontProductRepositoryEF
	{
		public IEnumerable<Product> GetAll(int storeId)
		{
			using (var ctx = new StoreFrontDbContext())
			{
				var prods = ctx.Product.Where(x => x.Published > 0
				&& x.ProductStores.Any(ps => ps.StoreID == storeId
				))
	  .Include(p => p.ProductVariants)
	  .Include(p=>p.ProductCategories)
	  .Include(p=>p.ProductType)
	  
	  .ToList();
				return prods;

			}

		}

		public Product GetById(int productId)
		{
			using (var ctx = new StoreFrontDbContext())
			{
				var product  = ctx.Product.Where(p => p.ProductID == productId).FirstOrDefault();
				return product;
			}

		}


	}
}
