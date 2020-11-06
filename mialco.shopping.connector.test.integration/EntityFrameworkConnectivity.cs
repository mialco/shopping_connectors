using mialco.shopping.connector.StoreFront.GoogleCategoryMapping.DL;
using System;
using Xunit;

namespace mialco.shopping.connector.test.integration
{
	public class EntityFrameworkConnectivity
	{
		[Fact]
		public void TestCategoriesDbConnectivity()
		{
			//todo: Pass the connection string from the configuration
			string connectionString = @"Server =.\SQLExpress; Database = irosepetals; Trusted_Connection = True;";
			var storeCategoryRepo = new StoreCategoryRepository(connectionString);
			var categories = storeCategoryRepo.GetStoreCategories();
			Assert.NotNull(categories);
		}


	}
}
