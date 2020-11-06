using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping
{
	public class GoogleCategoryMapping
	{
		private const string ConnectionString = @"Server =.\SQLExpress; Database = irosepetals; Trusted_Connection = True;";

		CategoryItemPath _defaultGoogleCategory;
		Dictionary<int, CategoryItemPath> _googleCategories;
		Dictionary<int, CategoryItemPath> _storeCategories;
		Dictionary<int, GoogleCategoryMapping> _categoryMapping;

		/// <summary>
		/// Loads store categories from DB
		/// </summary>
		public void  LoadStoreCategoriesFromDb()
		{
			var repo = new DL.StoreCategoryRepository(ConnectionString);

			var categories = repo.GetStoreCategories();

		}

		
	}
}
