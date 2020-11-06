using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping.DL
{
	public class StoreCategoryRepository
	{
		string _storeConnectionString;

		public StoreCategoryRepository(string connectionString)
		{
			_storeConnectionString = connectionString;
		}
		

		public IEnumerable<Category> GetStoreCategories()
		{
			using (var ctx = new StoreCategoryDbContext(_storeConnectionString))
			{
				var categories =ctx.Category.Where(c => c.Published == 1 && c.Deleted == 0)
					.OrderBy(x => x.ParentCategoryID).ThenBy(y=>y.CategoryID)
					.ToList();
				return categories;
			}

		}
	}
}
