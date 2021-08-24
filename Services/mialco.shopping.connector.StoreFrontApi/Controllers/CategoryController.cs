using mialco.utilities;
using mialco.shopping.connector.intefaces;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFrontApi.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mialco.shopping.objectvalues;

namespace mialco.shopping.connector.StoreFrontApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class CategoryController : Controller
	{
		IRepository<Category> _categoryRepo;
		public CategoryController(IRepository<Category> repo)
		{
			_categoryRepo = repo;
		}
		[HttpGet]
		public JsonResult List()
		{
			//TODO: Use Cache for store categories 
			var storeCategories = LoadStoreCategoriesFromDb();
			var categories = storeCategories.Select(c => new CategoryDto { CategoryId = c.Key, Name = c.Value.CategoryPath });			
			return new JsonResult(categories);
		}



		/// <summary>
		/// Loads store categories from DB
		/// </summary>
		private Dictionary<int,CategoryItemPath> LoadStoreCategoriesFromDb()
		{
			
			var categories = _categoryRepo.GetAll().OrderBy(x => x.ParentCategoryID);
			var storeCategories = new Dictionary<int, CategoryItemPath>();
			if (categories.Count() == 0)
			{
				Console.WriteLine("Store category returned an emty list form the database");
				return storeCategories;
			}

			// Load the categories in a tree
			//var rootNode = new GenericStoreCategory(0, "Root Category", 0);
			//Create a root node for the tree
			var firstCategory = categories.First();
			var treeData = new GenericStoreCategory(firstCategory.CategoryID, firstCategory.Name, firstCategory.ParentCategoryID);

			var rootNode = new GenericTree<GenericStoreCategory>(treeData);
			storeCategories.Add(firstCategory.CategoryID, new CategoryItemPath { CategoryId = firstCategory.CategoryID, CategoryPath = firstCategory.Name });
			// The categories returned from the store front database reflect one segment
			// Of the full path of the category, with a reference to the parent category
			// In order to get the full path of a category, we order the categories by the parent category id 
			//(assuming that the parent category id is always smaller that the children categories)
			// Then we load them into a tree, which will build the full path for each category
			// Then we add the category to the store category dictionary, 
			// using the path obtained from the tree
			foreach (var categ in categories.Skip(1))
			{
				var data = new GenericStoreCategory(categ.CategoryID, categ.Name, categ.ParentCategoryID);
				var node = new GenericTree<GenericStoreCategory>(data);
				var path = rootNode.AddWithPath(rootNode, node, 0);
				var catPath = new CategoryItemPath();
				storeCategories.Add(categ.CategoryID, new CategoryItemPath { CategoryId = categ.CategoryID, CategoryPath = path });
			}
			return storeCategories;
		}

	}
}
