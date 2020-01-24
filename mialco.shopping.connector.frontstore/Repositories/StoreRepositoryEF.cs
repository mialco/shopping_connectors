using mialco.shopping.connector.frontstore.Abstraction;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;
using mialco.shopping.connector.frontstore;

namespace mialco.shopping.connector.frontstore.repositories
{
	public class StoreRepositoryEF //: IRepository
	{
		public IEnumerable<Store> GetAll()
		{
			using (var ctx = new StoreFrontDbContext())
			{

				// var stores = ctx.Store.Where(x => 1 == 1).ToList();
				var stores = ctx.Store.ToList();
				return stores as IEnumerable<Store>;
			}
		}

		public mialco.shopping.connector.frontstore.Store GetById(int id)
		{
			using (var ctx = new StoreFrontDbContext())
			{

				var st = ctx.Store.Where(s=>s.StoreID== id).FirstOrDefault();
				return st;
			}
		}

	}
}
