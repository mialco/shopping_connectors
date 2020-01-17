
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mialco.shopping.connector.frontstore.repositories
{
	public class ProductReporsitoryEF : IRepository<Product>
	{
		public IEnumerable<Product> GetAll()
		{
			using (var ctx = new StoreFrontDbContext())
			{

				var stores = ctx.Store.AsEnumerable().ToList();
				var prodstore = ctx.ProductStore
					.Where(ps=>ps.StoreID==1 && ps.Product!= null)
					.Take(1000).ToList();
					var variants = ctx.ProductVariant
					.Where(x=>x.ColorSKUModifiers!= null && x.SizeSKUModifiers != null)
					.Take(20).ToList();

				var prods = ctx.Product .Where(
					x => 
				     x.Published > 0 
					&& x.ProductStores.Any(ps=>ps.StoreID==7)
				    //&&
					//x.ProductStores.Count() > 2 // .Any(ps=>ps.StoreID==3)			
				)
				.Select(y=> new { prdstores = y.ProductStores.Where(x=>x.StoreID==7).FirstOrDefault(), storescount = y.ProductStores.Count(),id = y.ProductID , var = y.ProductVariants })
				//.Count();
				//.Take(500000)
				.ToList();

				return null;// prods;
			}
		}
	}
}
