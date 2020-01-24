﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;



namespace mialco.shopping.connector.StoreFront
{
	public class StoreFrontStoreRepositoryEF
	{
		public IEnumerable<Store1> GetAll()
		{
			using (var ctx = new StoreFrontDbContext())
			{

				// var stores = ctx.Store.Where(x => 1 == 1).ToList();
				var stores = ctx.Store;
				return stores.ToList(); ;
			}

		}

		public Store1 GetById(int storeId)
		{
			using (var ctx = new StoreFrontDbContext())
			{

				// var stores = ctx.Store.Where(x => 1 == 1).ToList();
				var store = ctx.Store.Where(s=>s.StoreID==storeId).FirstOrDefault();
				return store;
			}

		}


	}
}