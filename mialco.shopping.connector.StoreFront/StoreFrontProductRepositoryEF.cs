using mialco.shopping.connector.intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;




namespace mialco.shopping.connector.StoreFront
{
	public class StoreFrontProductRepositoryEF: IProductRepository<Product>
	{

		string _connectionString;

		public StoreFrontProductRepositoryEF(string connectionString)
		{
			_connectionString = connectionString;
		}


		public IEnumerable<Product> GetAll(int storeId)
		{
			using (var ctx = new StoreFrontDbContext(_connectionString))
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

		public IEnumerable<Product> GetAllFilteredByCategory(int storeId , IEnumerable<int> categories)
		{
			using (var ctx = new StoreFrontDbContext(_connectionString))
			{
				if (categories != null && categories.Count() > 0)
				{
					var prods = ctx.Product.Where(x => x.Published > 0
					&& x.ProductStores.Any(ps => ps.StoreID == storeId 
					&& x.ProductCategories.Any(pc=>categories.Contains(pc.CategoryID))
					))
		  .Include(p => p.ProductVariants)
		  .Include(p => p.ProductCategories)
		  .Include(p => p.ProductType)

		  .ToList();
					return prods;
				}
				else
				{
					var prods = ctx.Product.Where(x => x.Published > 0
					&& x.ProductStores.Any(ps => ps.StoreID == storeId
					))
		  .Include(p => p.ProductVariants)
		  .Include(p => p.ProductCategories)
		  .Include(p => p.ProductType)

		  .ToList();
					return prods;
				}
			}

		}


		public Product GetById(int productId)
		{
			using (var ctx = new StoreFrontDbContext(_connectionString))
			{
				var product  = ctx.Product.Where(p => p.ProductID == productId).FirstOrDefault();
				return product;
			}

		}


	}
}
