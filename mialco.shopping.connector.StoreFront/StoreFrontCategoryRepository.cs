using mialco.shopping.connector.intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mialco.shopping.connector.StoreFront
{
	public class StoreFrontCategoryRepository : IRepository<Category>
	{
		private readonly string _connectionString;

		public StoreFrontCategoryRepository(string connectionString)
		{
			_connectionString = connectionString;
		}
		public IEnumerable<Category> GetAll()
		{
			try
			{
				using (var ctx = new StoreFrontDbContext(_connectionString))
				{
					var categories = ctx.Category.Where(c => c.Deleted == 0 && c.Published == 1).ToList();
					return categories;
				}
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		public Category GetById(int id)
		{
			try
			{
				using (var ctx = new StoreFrontDbContext(_connectionString))
				{
					var category = ctx.Category.Where(c => c.Deleted == 0 && c.Published == 1 && c.CategoryID == id).SingleOrDefault();
					return category;
				}
			}
			catch (Exception)
			{
				//todo: Error Handling
				throw;
			}
		}
	}
}
