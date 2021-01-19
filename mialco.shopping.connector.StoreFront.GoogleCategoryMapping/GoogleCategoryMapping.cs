using mialco.shopping.objectvalues;
using mialco.utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping
{
	/// <summary>
	/// This class handles all the activity related to mapping categories from the storefront to google categories
	/// These are: 
	/// Loading store front categories from the storefront database
	/// Loading categories from teh google download
	/// Loading the storefront-Map from different type of sources like Flat File, Web Interface, JSON file 
	/// Loadin categories
	/// </summary>
	public class GoogleCategoryMapping
	{
		private const string ConnectionString = @"Server =.\SQLExpress; Database = irosepetals; Trusted_Connection = True;"; //TODO: ReadFromConfig
		private const string AppDataRoot = "ShoppingConnectorFeed"; //TODO: Read From Config
		private const string GoogleCategoriesFileName = "taxonomy-with-ids.en-US.txt"; //todo: get from the configuration
		private const string StoreFrontGoogleCategoryMappingFileName = "StoreFront-Google-Category-mapping.csv"; //todo: get from the configuration
		private const string DefaultGoogleCategory = "Arts & Entertainment > Party & Celebration > Gift Giving > Fresh Cut Flowers";  //TODO: Replace with value from the configuration
		private bool _isDataLoaded;
		private string _googleCategoriesFileName;
		private string _storeFrontGoogleCategoryMappingFileName;
		private CategoryItemPath _defaultGoogleCategory;
		private Dictionary<int, CategoryItemPath> _googleCategories;
		private Dictionary<int, CategoryItemPath> _storeCategories;
		private Dictionary<int, CategoryMappingItem> _categoryMapping;

		

		/// <summary>
		/// This method will "warm" up the data for fur future operations.
		/// That is, it will load all the Google categories, the store categories
		/// the mapping storefront-google category 
		/// </summary>
		/// <returns></returns>
		public bool Initialize()
		{
			var result = false;
			try
			{
				


				if (_isDataLoaded) return true;
				_defaultGoogleCategory = new CategoryItemPath { CategoryId = 2899, CategoryPath = DefaultGoogleCategory };
				result = InitializeAndValidateResourceExistence();
				if (!result)
				{
					Console.WriteLine("Could not initialize and validate the resources ofr Google Category Classes object ");
					return result;
				}
				LoadStoreCategoriesFromDb();
				LoadGoogleCategoriesToInternalData(_googleCategoriesFileName);
				LoadFrontStoreToGoogleMaping(_storeFrontGoogleCategoryMappingFileName);
				_isDataLoaded = true;
				result = true;
				return result;
			}
			catch (Exception e)
			{
				//todo: Handle the exception
				Console.WriteLine(e.ToString());
				//throw;
			}
			return result;
		}

		/// <summary>
		/// Verifies if the files used by this  class are available 
		/// </summary>
		/// <returns></returns>
		private bool InitializeAndValidateResourceExistence()
		{
			var result = false;
			try
			{
				var appDataFolder = AppUtilities.GetApplicationDataPath();
				if (!Directory.Exists(appDataFolder))
				{
					Directory.CreateDirectory(appDataFolder);
					// Clearly the data does not exist if the folder did not exist 
					Console.WriteLine($"Applicaton data folder [{appDataFolder}] did not exists and it was created. " +
						$"Please place your data files in this folder and re-run the application" +
						$"The following Files are expected : \n" +
						$" -  {GoogleCategoriesFileName}\n" +
						$"");
				}
				_googleCategoriesFileName = Path.Combine(appDataFolder, GoogleCategoriesFileName);
				_storeFrontGoogleCategoryMappingFileName = Path.Combine(appDataFolder, StoreFrontGoogleCategoryMappingFileName);
				result = true;
				if (!File.Exists(_googleCategoriesFileName))
				{
					result = false;
					Console.WriteLine($"Cannot find {_googleCategoriesFileName}" );
				}
				if (!File.Exists(_googleCategoriesFileName))
				{
					result = false;
					Console.WriteLine($"Cannot find {_storeFrontGoogleCategoryMappingFileName}");
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.ToString());
				//todo: Handle exception
			}

			return result;
		}

		public string DefaultCategory { get => _defaultGoogleCategory == null ? string.Empty : _defaultGoogleCategory.CategoryPath; }
		public string GetGooleCategory(int storeCategory)
		{
			var result = string.Empty;
			if (!_isDataLoaded)
			{
				Console.WriteLine("Category mapping data was not loaded");
				return result;
			}
			if (!_categoryMapping.ContainsKey(storeCategory))
			{
				return _defaultGoogleCategory.CategoryPath;
			}
			var googleCategoryId = _categoryMapping[storeCategory].GoogleCategoryId;
			result = _googleCategories[googleCategoryId].CategoryPath;
			return result;
		}

		/// <summary>
		/// Loads store categories from DB
		/// </summary>
		public void LoadStoreCategoriesFromDb()
		{
			var repo = new DL.StoreCategoryRepository(ConnectionString);

			var categories = repo.GetStoreCategories().OrderBy(x=>x.ParentCategoryID);

			if (categories.Count() == 0)
			{
				Console.WriteLine("Store category returned an emty list form the database");
				return;
			}

			// Load the categories in a tree
			//var rootNode = new GenericStoreCategory(0, "Root Category", 0);
			//Create a root node for the tree
			var firstCategory = categories.First();
			var treeData = new GenericStoreCategory(firstCategory.CategoryID, firstCategory.Name, firstCategory.ParentCategoryID);

			var rootNode = new GenericTree<GenericStoreCategory>(treeData);
			_storeCategories = new Dictionary<int, CategoryItemPath>();
			_storeCategories.Add(firstCategory.CategoryID, new CategoryItemPath { CategoryId = firstCategory.CategoryID, CategoryPath = firstCategory.Name });
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
				var path = rootNode.AddWithPath( rootNode,node, 0);
				var catPath = new CategoryItemPath();
				_storeCategories.Add(categ.CategoryID, new CategoryItemPath {CategoryId=categ.CategoryID, CategoryPath=path });
			}
		}

		public void LoadGoogleCategoriesToInternalData(string fileName)
		{
			try
			{
				_googleCategories = new Dictionary<int, CategoryItemPath>();
				using (var file = new StreamReader(fileName))
				{
					while (!file.EndOfStream)
					{
						var line = file.ReadLine().Trim();
						if (line.StartsWith("#")) continue;
						var parts = line.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
						if (parts.Length < 2)
						{
							// Error reading record 
							//TODO : Report error
							continue;
						}
						int id = 0;
						if (!int.TryParse(parts[0].Trim(), out id))
						{
							// Error reading record 
							//TODO : Report error
							continue;
						}
						var categoryItemPath = new CategoryItemPath { CategoryId = id, CategoryPath = parts[1].Trim() };
						if (_googleCategories.ContainsKey(id))
						{
							_googleCategories[id] = categoryItemPath;
						}
						else
						{
							_googleCategories.Add(id, categoryItemPath);
						}
					}

				}

			}
			catch (Exception)
			{

				throw;
			}

		}


		/// <summary>
		/// The category mappingfile is in the csv format with 2 integer fields
		/// First Field is the store front category Id and the second one is Google CategoryId
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadFrontStoreToGoogleMaping(string fileName)
		{
			_categoryMapping = new Dictionary<int, CategoryMappingItem>();
			
			try
			{
				using (var file = new StreamReader(fileName))
				{
					while (!file.EndOfStream)
					{
						var line = file.ReadLine().Trim();
						var parts = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
						if (parts.Length < 2)
						{
							// Error reading record 
							//TODO : Report error
							continue;
						}
						int storeCategoryId = 0;
						if (!int.TryParse(parts[0].Trim(), out storeCategoryId))
						{
							// Error reading record 
							//TODO : Report error
							continue;
						}
						int googleId = 0;
						if (!int.TryParse(parts[1].Trim(), out googleId))
						{
							// Error reading record 
							//TODO : Report error
							continue;
						}
						if (_categoryMapping.ContainsKey(storeCategoryId))
						{
							_categoryMapping[storeCategoryId] = new CategoryMappingItem(storeCategoryId, googleId, 0, CategoryMappingType.CategoryMapping);
						}
						else
						{
							_categoryMapping.Add(storeCategoryId, new CategoryMappingItem(storeCategoryId, googleId, 0, CategoryMappingType.CategoryMapping));
						}
					}

				}
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
