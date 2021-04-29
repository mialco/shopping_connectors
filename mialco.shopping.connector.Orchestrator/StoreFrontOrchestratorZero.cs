using mialco.abstractions;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.StoreFront;
using System;
using System.Collections.Generic;
using System.Text;
using mialco.shopping.connector.shared;
using System.Text.RegularExpressions;
using mialco.shopping.connector.GoogleAdFeed;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;
using mialco.shopping.connector.RawFeed.StoreFront;
using mialco.utilities;
using System.IO;
using System.Linq;
using mialco.configuration;

namespace mialco.shopping.connector.Orchestrator
{
	public class StoreFrontOrchestratorZero : IRunable
	{
		private const string OutputFileName = "OutputFeed.xml";
		private IEnumerable<Product> _products;
		private readonly int _storeId;
		private readonly WebStoreDeploymentType _deploymentType;
		private readonly List<GenericFeedRecord> _rawData;
		private readonly ShoppingConnectorConfiguration _shoppingConnectorConfiguration;
		private readonly ApplicationInstanceSettings _applicationInstanceSettings;
		private readonly ApplicationSettings _applicationSettings;
		private readonly IdentifiersFilters _identifiersFilters;
		private Store1 _store;
		RawFeedBuilder _rawFeedBuilder;
		GoogleCategoryMapping _googleCategoryMapping;


		public StoreFrontOrchestratorZero(int storeId, ApplicationInstanceSettings applicationInstanceSettings)
		{
			_storeId = storeId;
			_applicationInstanceSettings = applicationInstanceSettings;
			_deploymentType = ShoppingConnectorUtills.DeploymentTypeFromString (applicationInstanceSettings.DeploymentType);
			_rawFeedBuilder = new RawFeedBuilder(_applicationInstanceSettings);
			_rawData = new List<GenericFeedRecord>();

			_googleCategoryMapping = _rawFeedBuilder.GoogleCategoryMapping;
		}


		/// <summary>
		/// Deprecated
		/// </summary>
		/// <param name="storeId"></param>
		/// <param name="deploymentType"></param>
		public StoreFrontOrchestratorZero(int storeId, WebStoreDeploymentType deploymentType)
		{
			_storeId = storeId;
			_deploymentType = deploymentType;
			_rawFeedBuilder = new RawFeedBuilder(_applicationInstanceSettings);
			_rawData = new List<GenericFeedRecord>();

			_googleCategoryMapping = _rawFeedBuilder.GoogleCategoryMapping;
		}


		/// <summary>
		/// Deprecated
		/// </summary>
		/// <param name="storeId"></param>
		/// <param name="shoppingConnectorConfiguration"></param>
		public StoreFrontOrchestratorZero(int storeId, ShoppingConnectorConfiguration shoppingConnectorConfiguration)
		{
			_storeId = storeId;
			//_deploymentType = deploymentType;
			_shoppingConnectorConfiguration = shoppingConnectorConfiguration;
			_rawFeedBuilder = new RawFeedBuilder(_applicationInstanceSettings);
			_rawData = new List<GenericFeedRecord>();
			_googleCategoryMapping = _rawFeedBuilder.GoogleCategoryMapping;
		}


		public StoreFrontOrchestratorZero(int storeId, ApplicationSettings applicationSettings, ApplicationInstanceSettings applicationInstanceSettings, IdentifiersFilters identifiersFilters)
		{
			_storeId = storeId;
			_deploymentType = ShoppingConnectorUtills.DeploymentTypeFromString(applicationInstanceSettings.DeploymentType); 
			_applicationSettings = applicationSettings;
			_applicationInstanceSettings = applicationInstanceSettings;
			_identifiersFilters = identifiersFilters;
			_rawFeedBuilder = new RawFeedBuilder(_applicationInstanceSettings);
			_rawData = new List<GenericFeedRecord>();
			_googleCategoryMapping = _rawFeedBuilder.GoogleCategoryMapping;

		}


		/// <summary>
		/// This is executing the following steps  :
		/// * Executes ExtactData() Extracts data from the storefront database, process it  and saves it on some storage type 
		/// The processing is about combining the information from the database into relevat informatin for the google feed
		/// The output form this process is ain a generic kvp format 
		/// * Executes ExportRowData which takes the data in the RowFeed format and writes it as the GoogleFeed xml file
		/// </summary>
		/// <returns></returns>
		public int Run()
		{
			

			//RunAllActionsInOneBigLoop();
			RunAllActionsInOneBigLoop(_storeId,_applicationSettings,_applicationInstanceSettings ,_identifiersFilters);
			//ExtractData(33);
			// ToDo - Load Categories from the database 
			// ToDo - Load Categories Mapping from Goole 
			//ExportRawData();
			return 0;
		}

		/// <summary>
		/// It executes the extraction from the database, filteredByCategory if the filter exsits  then 
		/// loops through each record it directly to the Google feed xml file
		/// 
		/// </summary>
		/// <returns></returns>
		public int RunAllActionsInOneBigLoop(int storeId,ApplicationSettings applicationSettings, ApplicationInstanceSettings applicationInstanceSettings ,IdentifiersFilters categoryFilter = null)
		{

			//TODO: Add verification for all paths and folders of the application, be

			//Get Application settings
			var shoppingConnectorConfiguration = ShoppingConnectorConfiguration.GetConfiguration();
			var appSettings = shoppingConnectorConfiguration.GetApplicationSettings();


			int result = 0;
			// 1. We conect to the database
			// and extract the data into a list pt Product : _product
			var filterString=categoryFilter==null ? "Null Filter" : "Filter Exists";
			Console.WriteLine($"Category Filter : {filterString}");
			Console.WriteLine($"Connecting to database to extract the list of products\nStoreId: {+storeId} - ConnectionString {_applicationInstanceSettings.ConnecttionString}");

			ExtractData(_storeId, _applicationInstanceSettings.ConnecttionString, categoryFilter); ;

			//Create an initialize an instance of google category mapping
			//GoogleCategoryMapping googleCategoryMapping = new GoogleCategoryMapping();
			//googleCategoryMapping.Initialize();

			_rawFeedBuilder.LoadCategories();

			_rawFeedBuilder.LoadImagesLookup();
			var imageLookup = _rawFeedBuilder.ImagesLookupUtility;


			// Then we pass the products to the BuildFeed method, which for each product creates a RawFeed.GenericFeed Record
			// Here is happening the core of the data processing logic from the database to the daat that will be put in the data stream
			Console.WriteLine("Starting Building Feed from the product data");

			BuildFeed(_store, _products, _googleCategoryMapping, true);
			FeedGenerator feedGenerator = new FeedGenerator();


			Console.WriteLine("Starting writing the products to the output feed for Google ");
			//DEBT: The XML Generator writes one singe item for all of the products
			//Debt. Almost no matching to the mapping. Just few items
			// No Sizes or colors are discovered
			//var outputFile = Path.Combine(AppUtilities.GetApplicationDataPath(), OutputFileName);
			var timeStamp = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("00")}{DateTime.Now.Day.ToString("00")}{DateTime.Now.Hour.ToString("00")}{DateTime.Now.Minute.ToString("00")}{DateTime.Now.Second.ToString("00")}";
			var outputFileName =$"{_applicationInstanceSettings.Name}_{timeStamp}_{applicationSettings.Files.XmlOutputFeedBase}";
			var outputFile = Path.Combine( _applicationSettings.Folders.OutputFolder, outputFileName);




			feedGenerator.GenerateXmlFeed(outputFile, _rawData, new FeedProperties("RosePetalsStore", @"https://www.rosepatalestore.com", "The rose petalStore"));
			return result;
		}



		/// <summary>
		/// It executes the extraction from the database in batches then 
		/// loops through each record of the batch and outputs it directly to the Google feed xml file
		/// 
		/// </summary>
		/// <returns></returns>
		public int RunAllActionsInOneBigLoop()
		{

			//Get Application settings
			//var shoppingConnectorConfiguration = ShoppingConnectorConfiguration.GetConfiguration();
			//var appSettings = shoppingConnectorConfiguration.GetApplicationSettings();
			

			int result = 0;
			// 1. We conect to the database
			// and extract the data into a list pt Product : _product
			Console.WriteLine("Connecting to database to extract the list of products");
			//ExtractData(_storeId);
			//Create an initialize an instance of google category mapping
			//GoogleCategoryMapping googleCategoryMapping = new GoogleCategoryMapping();
			//googleCategoryMapping.Initialize();

			_rawFeedBuilder.LoadCategories();
			_rawFeedBuilder.LoadImagesLookup();
			var imageLookup = _rawFeedBuilder.ImagesLookupUtility;


			// Then we pass the products to the BuildFeed method, which for each product creates a RawFeed.GenericFeed Record
			// Here is happening the core of the data processing logic from the database to the daat that will be put in the data stream
			Console.WriteLine("Starting Building Feed from the product data"); 

			BuildFeed(_store, _products, _googleCategoryMapping, true);
			FeedGenerator feedGenerator = new FeedGenerator();


			Console.WriteLine("Starting writing the products to the output feed for Google ");
			//DEBT: The XML Generator writes one singe item for all of the products
			//Debt. Almost no matching to the mapping. Just few items
			// No Sizes or colors are discovered
			var outputFile = Path.Combine(AppUtilities.GetApplicationDataPath(), OutputFileName);



			feedGenerator.GenerateXmlFeed(outputFile, _rawData, new FeedProperties("RosePetalsStore", @"https://www.rosepatalestore.com", "The rose petalStore"));
			return result;
		}

		/// <summary>
		/// Extracts data from the storefront database
		/// </summary>
		/// <param name="storeId"></param>
		//private void ExtractData(int storeId)
		//{
		//	StoreFrontStoreRepositoryEF sfsr = new StoreFrontStoreRepositoryEF();
		//	_store = sfsr.GetById(storeId);
		//	var prodrep = new StoreFrontProductRepositoryEF();
		//	_products = prodrep.GetAll(storeId);
		//	_products = prodrep.GetAllFilteredByCategory(storeId,_identifiersFilters.GetFilter("Categories"));
		//}

		private void ExtractData(int storeId, string connectionString)
		{
			StoreFrontStoreRepositoryEF sfsr = new StoreFrontStoreRepositoryEF(connectionString);
			_store = sfsr.GetById(storeId);
			var prodrep = new StoreFrontProductRepositoryEF(connectionString);
			_products = prodrep.GetAll(storeId);
		}

		private void ExtractData(int storeId,string connectionString, IDataFilterValues<int> filters)
		{
			StoreFrontStoreRepositoryEF sfsr = new StoreFrontStoreRepositoryEF(connectionString);
			_store = sfsr.GetById(storeId);
			var prodrep = new StoreFrontProductRepositoryEF(connectionString);
			_products = prodrep.GetAllFilteredByCategory(storeId, _identifiersFilters.GetFilter("Categories")); //todo: Fix Null filters
		}

		//TODO: DEBT
		//DEBT:  Move this method into its on class dedicated for building the Raw Feed from the database records
		//Creates the collection of Raw data
		private void BuildFeed(Store1 store, IEnumerable<Product> products, GoogleCategoryMapping googleCategoryMapping , bool defaultVariantOnly )
		{
			
			try
			{
				foreach (var p in products)
				{
					//var sizes = GetProductSizes(p);
					//var colors = GetProductColors(p);
					var rf = new GenericFeedRecord();
					var id = 0;
					var variantNumber = 0;
					var storeURI = GetStoreURI(store, _deploymentType);
					if (p.ProductVariants == null || p.ProductVariants.Count == 0)
					{
						//ToDo: Report that we skip the product because it doe not have varians
						Console.WriteLine($"Product Id: {p.ProductID} - {p.Summary} - does not have variants. Skipping");
						continue;
					}



					//DEBT remove if possible the try block from within the loop. It is indroducing a  lot of inneficiency
					//We build products for each variant, color and size 
					//var categoryFilter = new List<int> { 12,13,15, 17,19 , 25,28, 47,95,96};
					//var categoryFilter = new List<int> { 28, 129, 17, 47, 25, 59, 93 }; // 200Petals Wedding Petals Feed
					var categoryFilter = new List<int> { 217, 218 };
					foreach (var variant in p.ProductVariants)
					{
						var categoryFilteredCount =  p.ProductCategories.Count(x => categoryFilter.Contains(x.CategoryID));
						if (categoryFilteredCount == 0)
							continue;
						variantNumber++;
						if (defaultVariantOnly && variant.IsDefault != 1)
						{
							continue;
						}
						if (defaultVariantOnly && variantNumber > 1)
						{
							continue;
						}


						//id = GenerateProductId
						//DEBT: Correct the store url to remove the port 0
						var sizeOptions = GetProductAttributes(variant.Sizes, variant.SizeSKUModifiers, typeof(SizeOption));
						var colorOptions = GetProductAttributes(variant.Colors, variant.ColorSKUModifiers, typeof(ColorOption));
						// Need to fix the collors which was original designed to be exracted from product as a list of tupples
						// var optionImages = GetOptionImages(colors, p.ProductID, storeURI, colorOptions);
						var colorOptionCount = 0;
						var sizeOptionCount = 0;
						//Size options 
						if (sizeOptions == null || sizeOptions.Count == 0)
							sizeOptions = new List<ProductAttribute> { new SizeOption { Name = String.Empty, AddedPrice = 0, SkuModifier = string.Empty } };
						if (colorOptions == null || colorOptions.Count == 0)
							colorOptions = new List<ProductAttribute> { new ColorOption { Name = string.Empty, AddedPrice = 0, SkuModifier = string.Empty } };
						sizeOptions.ForEach(size =>
						{
							sizeOptionCount++;
							colorOptionCount = 0;
							colorOptions.ForEach(color =>
							{
								colorOptionCount++;
								if (!(defaultVariantOnly && (sizeOptionCount > 1 || colorOptionCount > 1)))
								{
									try
									{
										// Create New Raw Product for each variant, size and color
										var rawFeedRecord = new GenericFeedRecord();
										id++; //The Id we are just incrementing the previous id
										var productId = GenerateProductId(p.ProductID, variant.VariantID, size.SkuModifier, color.SkuModifier);
										rawFeedRecord.ProductId = productId;
										rawFeedRecord.FeedRecord.Add("Id", productId);
										rawFeedRecord.FeedRecord.Add("ColorOptionCount", colorOptionCount.ToString());
										rawFeedRecord.FeedRecord.Add("SizeOptionCount", colorOptionCount.ToString());
										rawFeedRecord.FeedRecord.Add("Size", size.Name);
										rawFeedRecord.FeedRecord.Add("SizePriority", size.Priority.ToString());
										rawFeedRecord.FeedRecord.Add("ColorPriority", color.Priority.ToString());
										rawFeedRecord.FeedRecord.Add("Title", p.Name ?? "");
										rawFeedRecord.FeedRecord.Add("Description", p.Description ?? "");
										// The category from the store Front will be mapped to the
										// Google field product_type
										// The Value will be created by listing the entire path of a category with its 
										// parent categories delimited by ">" 
										// for Exampe
										// Parent Category > Category
										// Funny Crazy T-shirts > Fitness Gym T-shirts
										//DEBT: Add category Logic
										//rawFeedRecord.FeedRecord.Add("SalePriceEffectiveDate",)
										//DEBT: AgeGroup
										rawFeedRecord.FeedRecord.Add("AgeGroup", "");
										rawFeedRecord.FeedRecord.Add("ManufaturingPartNumber", variant.ManufacturerPartNumber ?? "");
										rawFeedRecord.FeedRecord.Add("ItemGroupId", $"{p.ProductID}-{variant.VariantID}");
										rawFeedRecord.FeedRecord.Add("Weight", variant.Weight.ToString());
										rawFeedRecord.FeedRecord.Add("Link", _rawFeedBuilder.GetProductLink(p, _store));  //todo: test method
										var additionalImage = string.Empty;
										rawFeedRecord.FeedRecord.Add("ImageLink", _rawFeedBuilder.GetProductImage(p.ProductID, store, out additionalImage));//ToDo: Test Method
										rawFeedRecord.FeedRecord.Add("AdditionalImageLink", additionalImage);
										rawFeedRecord.FeedRecord.Add("Condition", GetContition(variant)); //TODO: Improve method
										rawFeedRecord.FeedRecord.Add("Availability", GetAvailability(variant)); //TODO: improve method -currently hard-coded value
										rawFeedRecord.FeedRecord.Add("AvailabilityDate", GetAvailabilityDate(p.ProductID));
										rawFeedRecord.FeedRecord.Add("SalePrice", _rawFeedBuilder.GetSalePrice(variant )); //TODO: Hardcoded currency 
										rawFeedRecord.FeedRecord.Add("SalePriceEffectiveDate", GetSalePriceEffecctiveDate(variant).ToString());
										rawFeedRecord.FeedRecord.Add("Price", (variant.Price + size.AddedPrice + color.AddedPrice).ToString() + " USD");
										rawFeedRecord.FeedRecord.Add("Gtin", _rawFeedBuilder.GetGtin(variant)); // TODO: Test data retrieval from the database
										rawFeedRecord.FeedRecord.Add("Brand", GetBrand(store));
										rawFeedRecord.FeedRecord.Add("MPN", _rawFeedBuilder.GetMpn(productId, variant));
										rawFeedRecord.FeedRecord.Add("Category", _rawFeedBuilder.GetGoogleProductCategory(p));
										rawFeedRecord.FeedRecord.Add("ProductType", _rawFeedBuilder.GetProductType(p)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingCountry", GetProductType(p.ProductID)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingService", GetProductType(p.ProductID)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingPrice", GetProductType(p.ProductID)); //todo: implement method
										rawFeedRecord.FeedRecord.Add("Color", color.Name);



										//Add Raw Feed Record to the collection
										_rawData.Add(rawFeedRecord);
									}
									catch (Exception ex)
									{
										//TODO: Log
										Console.WriteLine(ex);
									}
								}
								else
								{
									//Console.WriteLine("adasasd");
								}

							});
						});
					}
				}
			}
			catch (Exception)
			{

				throw;
			}

		}

		/// <summary>
		/// This method has no functionality at the time of this implementation. 
		/// It is put here as a plcaceholder for future development
		/// </summary>
		/// <param name="variant"></param>
		/// <returns></returns>
		private object GetSalePriceEffecctiveDate(ProductVariant variant)
		{
			return string.Empty;
		}



		/// <summary>
		/// At the implemenatonion time, the Brand was not a known value for the product 
		/// Becasue this is a rquired field, we will HARD-CODE the return 
		/// https://support.google.com/merchants/answer/6324351?hl=en&ref_topic=6324338
		/// max Size : 70 characters
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		private string GetBrand(Store1 store)
		{
			//TODO: Veify if it returns the proper value 
			var brand = string.Empty;
			if (store != null)
			{
				brand = store.Name.Trim();
				if (brand.Length > 70) brand = brand.Substring(0, 70);
			}
			// Possible that the current implementation of the sorefront engine is using Store.Name for this field
			return brand;
		}

		/// <summary>
		/// This is not a required fiedld so we return empty for now
		/// The field is defined for a preorder case
		/// https://support.google.com/merchants/answer/6324470
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		private string GetAvailabilityDate(int productID)
		{
			//todo: Implement 
			var result = string.Empty;
			return result;
		}

		/// <summary>
		/// This is a required field
		/// Supported values in Google: in_stock, out_of_stock, preorder
		/// We will return the vakue based in the inventory field
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		private string GetAvailability(ProductVariant variant)
		{
			//todo: Read the values from the configuration. These are values specific to google 
			const string InStock = "in_stock";
			const string OutOfStock = "out_of_stock";

			//TODO = Make this function return a generic integer 
			//and have the feed specific function interpred the code and retur the string approprite to the respective feed

			var result = string.Empty;
			if (variant == null) return OutOfStock;

			if (variant.Inventory > 0)
				result = InStock;
			else
				result = OutOfStock;
			return result;
		}

		private string GetContition(ProductVariant variant)
		{
			return "new";
		}

		/// <summary>
		/// This function is mapping the goolgle product categories 
		/// with the categories used by storefront 
		/// Mapped categories are publisher dependent so we will have a specific map for Google
		/// Implemented in the GoogleAdFeed Assembly
		/// Google Caegories are provided by google in either xml format or plain text format
		/// Example text entry: 
		///		2271
		//			or
		//		Apparel & Accessories > Clothing > Dresses
		// The user of the application will have to provide a mapping as well and also a default category in case the name is not matched
		// A separate appication and also  will give a chance the user to map either:
		// Map individual products to specific categories or
		// Map specifi paths on storefront categories with Google categories
		// Map each segment of a category path in store front to a path in Google 
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		private string GetProductCategory(Product product)
		{
			
			throw new NotImplementedException();
		}

		///// <summary>
		///// The link is composed of :
		/////		Store url
		/////		letter "p"
		/////		product id 
		/////		SEName field of the product table
		///// </summary>
		///// <param name="product"></param>
		///// <param name="store"></param>
		///// <returns></returns>
		//private string GetProductLink(Product product, Store1 store)
		//{
		//	const string  particle  = "p";
		//	const string ending = ".aspx";
		//	string result = string.Empty;
		//	if (product == null)
		//		throw new Exception("GetProductLink() function received a null product");
		//	if (store == null)
		//		throw new Exception("GetProductLink() function received a null store");

		//	string productid = product.ProductID.ToString();
		//	string seName = product.SEName ?? string.Empty;

		//	result = $"{store.ProductionURI}/{particle}-{seName}{ending}";
		//	return result;
		//}

		private object GetOptionImages(List<Tuple<string, decimal, string>> colors, int productID, object storeURI, object colorOptions)
		{
			throw new NotImplementedException();
		}

		///// <summary>
		///// This method builds the Image url based on product id and store url
		///// This may vary from one implementation of the stre to another
		///// Therefore may be suitable in the future to adda a plugin function that we could replace 
		///// based on the store implementation
		///// Sample image url: https://www.wickedteesofny.com/images/product/large/5413_1_.jpg
		///// {stoteUrl}/images/product/{large|medium|small}/{productid}_{picture_number}_.jpg
		///// TODO:
		///// On this version developed on July 2020 we do not know the rules of how the images url are build on the server
		///// We will need to come up with a way to store the image information in the database so we can return the accurate url
		///// as this method has chances to be incuarate
		///// </summary>
		///// <param name="productID"></param>
		///// <param name="storeURI"></param>
		///// <returns></returns>
		//private string GetProductImage(int productID, string storeURI)
		//{
		//	var result = string.Empty;
		//	storeURI = storeURI ?? string.Empty;
		//	var url = new StringBuilder(storeURI);
		//	if (!storeURI.EndsWith('/')) url.Append('/');
		//	url.Append("images/product/");
		//	url.Append("large/");
		//	url.Append(productID);
		//	url.Append("_1_.jpg");
		//	result = url.ToString();
		//	return result;
		//}

		private List<ColorOption> GetColorOptions(Product p)
		{
			throw new NotImplementedException();
		}

		public List<ProductAttribute> GetProductAttributes(string attributeOptions,
			string skuModifiers, Type attributeType)
		{
			if (attributeType == null)
			{
				var ex = new ArgumentNullException("The attribute type parameter cannot be null");
				throw ex;
			}

			ProductAttributeAbstractFactory fact = null;
			if (attributeType == typeof(ColorOption))
				fact = new ColorOptionsFactory();
			if (attributeType == typeof(SizeOption))
				fact = new SizeOptionsFactory();


			if (fact == null)
			{
				var ex = new NotImplementedException("There i no implementation for aatributes of type " + attributeType.FullName);
				throw ex;
			}


			var result = new List<ProductAttribute>();
			var addedprices = new List<string>();
			var modifiersList = new List<string>();
			var maxAttrCount = 0;
			var namesCount = 0;
			var skuModifiersCount = 0;

			var attrNames = new string[] { }; ;
			var skuModifiersCollection = new string[] { };

			//Split The attributes string 
			if (!string.IsNullOrEmpty(attributeOptions))
			{
				attrNames = attributeOptions.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				namesCount = attrNames.Length;
				maxAttrCount = namesCount;
			}

			if (!string.IsNullOrEmpty(skuModifiers))
			{
				skuModifiersCollection = skuModifiers.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				skuModifiersCount = skuModifiersCollection.Length;
				maxAttrCount = maxAttrCount > skuModifiersCount ? maxAttrCount : skuModifiersCount;
			}


			//DEBT: Finish this function to return the product attributes
			// We create a product attribute for each of the attrName and we add the sku mofifier from the 
			// Correspondent element from the sku modifiers list
			if (namesCount < 1)
				return result;

			for (int i = 0; i < namesCount; i++)
			{
				var attrb = fact.GetAttribute();
				(attrb.Name, attrb.AddedPrice) = GetPriceFromAttributeString(attrNames[i]).ToValueTuple();
				attrb.SkuModifier = i < skuModifiersCount ? skuModifiersCollection[i] : string.Empty;
				result.Add(attrb);
			}

			return result;
		}

		/// <summary>
		/// The attribute string may come in the format attributename[x.y]
		/// where x.y is the added price for the specific attribute
		/// </summary>
		/// <param name="attributeString"></param>
		/// <returns></returns>
		public Tuple<string, decimal> GetPriceFromAttributeString(string attributeString)
		{
			string name = string.Empty;
			decimal price = 0.0m;
			attributeString = attributeString ?? "";

			var rx = new Regex(@"\s*(?<attr>.*)\s*\[\s*(?<price>[[0-9]*\.{0,1}[0-9]*)\s*\]");
			Match match = rx.Match(attributeString);
			if (match.Success)
			{
				name = match.Groups["attr"].Value;
				var sPrice = (match.Groups["price"].Value).Trim();
				sPrice = sPrice == "." ? "0.0" : sPrice;
				if (!decimal.TryParse(sPrice, out price))
				{
					name = attributeString;
					price = 0.0m;
				}
			}
			else
			{
				name = attributeString;
				price = 0.0m;
			}
			name = name.Trim();
			return new Tuple<string, decimal>(name, price);
		}
		public string GetStoreURI(Store1 store, WebStoreDeploymentType deploymentType)
		{
			string result = string.Empty;
			var uriBuilder = new UriBuilder();
			(uriBuilder.Scheme,
					uriBuilder.Host,
					uriBuilder.Path,
					uriBuilder.Port) = GetStoreURIParts(deploymentType, store);

			return uriBuilder.ToString();

		}

		private Tuple<string, string, string, int> GetStoreURIParts(WebStoreDeploymentType deploymentType, Store1 store)
		{
			string scheme = string.Empty, host = string.Empty, path = string.Empty;
			int port = 0;
			switch (deploymentType)
			{
				case WebStoreDeploymentType.Production:
					path = store.ProductionDirectoryPath;
					host = store.ProductionURI;
					Int32.TryParse((store.ProductionPort ?? "0"), out port);
					break;
				case WebStoreDeploymentType.Staging:
					path = store.StagingDirectoryPath;
					host = store.StagingURI;
					Int32.TryParse((store.StagingPort ?? "0"), out port);
					break;
				case WebStoreDeploymentType.Development:
					path = store.DevelopmentDirectoryPath;
					host = store.DevelopmentURI;
					Int32.TryParse((store.DevelopmentPort ?? "0"), out port);
					break;
				default:
					break;
			}

			if (host.ToLower().StartsWith("http://"))
			{
				scheme = "http:";
				host = host.PadRight(8).Substring(7).Trim();
			}
			if (host.ToLower().StartsWith("https://"))
			{
				scheme = "http:";
				host = host.PadRight(9).Substring(8).Trim();
			}
			host = (host ?? string.Empty).Trim();
			return new Tuple<string, string, string, int>(scheme, host, path, port);
		}


		/// <summary>
		/// Returns a kist of colors from the comma delimited list in the field Colors
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private List<Tuple<string, decimal, string>> GetProductColors(Product p)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// From the product we extracct the sizes from the field sizes
		/// The field Sizes could come in the format: XSmall,Small[1],Medium[1],Large[1],XLarge[2],2X-Large[4],3X-Large[4],4X-Large[5],5X-Large[5]
		/// Where each element of the coma delimited list is a value for the size. 
		/// The numbers in the brackets represent the cost added to to he base product, which is the one with no value or zero value
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private List<Tuple<string, decimal>> GetProductSizes(Product p)
		{
			// This may not be needed at all
			throw new NotImplementedException();
		}

		//private object GenerateId(Product p)
		//{
		//	int result = 0;

		//	return result;
		//}

		/// <summary>
		/// This is the identifier for the product - Unique across a set of data
		/// it is made of the product ProductId from the database, variantId, 
		/// color sku modifier , size sku modifier, if any of these are empty ,
		/// we replace them with random numbers prefixed by C(from color) and 
		/// </summary>
		/// <param name="product"></param>
		/// <param name="StoreId"></param>
		/// <returns></returns>
		public string GenerateProductId(int productId, int variantId, string sizeSkuModifier, string colorSkuModifier)
		{
			string result = string.Empty;
			sizeSkuModifier = string.IsNullOrEmpty(sizeSkuModifier) ? "NS" : sizeSkuModifier;
			colorSkuModifier = string.IsNullOrEmpty(colorSkuModifier) ? "NC" : colorSkuModifier;
			result = $"{productId}_{variantId}_{sizeSkuModifier}_{colorSkuModifier}";

			return result;
		}

		private void ExportRawData()
		{
		}


		public abstract class ProductAttributeAbstractFactory
		{
			abstract public ProductAttribute GetAttribute();
		}

		public class ColorOptionsFactory : ProductAttributeAbstractFactory
		{
			public override ProductAttribute GetAttribute()
			{
				return new ColorOption();
			}
		}

		public class SizeOptionsFactory : ProductAttributeAbstractFactory
		{
			public override ProductAttribute GetAttribute()
			{
				return new SizeOption();
			}
		}

		public abstract class ProductAttribute
		{
			public int Priority { get; }
			public string Name { get; set; }
			public decimal AddedPrice { get; set; }
			public string SkuModifier { get; set; }

			public override bool Equals(object obj)
			{
				if (obj == null)
					return false;
				var obj1 = obj as ProductAttribute;
				return (obj1.Name == this.Name
					&& obj1.Priority == this.Priority
					&& obj1.SkuModifier == this.SkuModifier
					&& obj1.AddedPrice == this.AddedPrice
					);
			}

			public override int GetHashCode()
			{
				const int p = 29;
				int hash = p;
				hash = hash * p + AddedPrice.GetHashCode();
				hash = hash * p + (Name ?? "").GetHashCode();
				hash = hash * p + (SkuModifier ?? "").GetHashCode();
				hash = hash * p + Priority.GetHashCode();
				return hash;
			}
		}

		public class SizeOption : ProductAttribute
		{
		}

		public class ColorOption : ProductAttribute
		{
		}

	}
}
