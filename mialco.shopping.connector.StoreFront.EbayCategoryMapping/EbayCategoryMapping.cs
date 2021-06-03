using System;
using System.Collections.Generic;
using System.IO;
using mialco.configuration;
using mialco.shopping.connector.shared;


namespace mialco.shopping.connector.StoreFront.EbayCategoryMapping
{
	public class EbayCategoryMapping
	{


		private bool _isDataLoaded;
		private string _ebayCategoriesFileName;
		private string _storeFrontGoogleCategoryMappingFileName;
		private string _storeFrontEbayCategoryMappingFileName;
		private string _connectionString;
		private readonly int _defaultEbayCategoryId;
		private ApplicationSettings _applicationSettings;
		private ApplicationInstanceSettings _applicationInstanceSettings;
		private Dictionary<int, CategoryMappingItem> _categoryMapping;

		public int DefaultEbayCategoryId => _defaultEbayCategoryId;

		public EbayCategoryMapping(string ebayCategoriesFileName, ApplicationSettings applicationSettings, ApplicationInstanceSettings applicationInstanceSettings)
		{
			_ebayCategoriesFileName = ebayCategoriesFileName;
			_applicationSettings = applicationSettings;
			_applicationInstanceSettings = applicationInstanceSettings;
			_defaultEbayCategoryId = applicationInstanceSettings.DefaultEbayCategoryId;
		}


		private void LoadFrontStoreToEbayMaping(string fileName)
		{
			_categoryMapping = new Dictionary<int, CategoryMappingItem>();

			try
			{
				using (var file = new StreamReader(fileName))
				{
					while (!file.EndOfStream)
					{
						var line = file.ReadLine().Trim();
						
						if (line.Length == 0) continue;
						if (line.StartsWith("#")) continue;

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
							_categoryMapping[storeCategoryId] = new CategoryMappingItem( googleId, 0, CategoryMappingType.CategoryMapping);
						}
						else
						{
							_categoryMapping.Add(storeCategoryId, new CategoryMappingItem( googleId, 0, CategoryMappingType.CategoryMapping));
						}
					}

				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		public int GetMappedEbayCategory(int sourceCategoryId)
		{
			var result = _defaultEbayCategoryId;
			if (!_isDataLoaded)
			{
				Console.WriteLine("Ebay Category mapping data was not loaded");
				return result;
			}
			
			if (!_categoryMapping.ContainsKey(sourceCategoryId))
			{
				return _defaultEbayCategoryId;
			}
			result  = _categoryMapping[sourceCategoryId].MappedCategoryId;
			
			return result;

		}

		public bool Initialize()
		{
			var result = false;
			try
			{

				if (_isDataLoaded) return true;
				result = InitializeAndValidateResourceExistence();
				if (!result)
				{
					Console.WriteLine("Could not initialize and validate the resources ofr Google Category Classes object ");
					return result;
				}
				//LoadStoreCategoriesFromDb();
				//LoadGoogleCategoriesToInternalData(_googleCategoriesFileName);
				LoadFrontStoreToEbayMaping(_storeFrontGoogleCategoryMappingFileName);
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
				result = true;
				if (!File.Exists(_ebayCategoriesFileName))
				{
					result = false;
					Console.WriteLine($"Cannot find {_ebayCategoriesFileName}");
				}
				if (!File.Exists(_ebayCategoriesFileName))
				{
					result = false;
					Console.WriteLine($"Cannot find {_storeFrontEbayCategoryMappingFileName}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				//todo: Handle exception
			}

			return result;
		}




	}
}
