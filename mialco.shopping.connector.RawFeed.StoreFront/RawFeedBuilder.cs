using System;
using System.Text;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;
using mialco.shopping.connector.StoreFront.EbayCategoryMapping;
using mialco.shopping.connector.RawFeed.StoreFront.ImageLookup;
using mialco.configuration;
using System.IO;
using System.Collections.Generic;
using mialco.shopping.connector.shared;
using System.Linq;
using System.Text.RegularExpressions;


namespace mialco.shopping.connector.RawFeed.StoreFront
{
	/// <summary>
	/// Class used to build a raw feed for StoreFront application
	/// </summary>
	public class RawFeedBuilder
	{
		const int ApplicationInstanceId = 0;
		private ApplicationSettings _applicationSettings;
		private ApplicationInstanceSettings _applicationInstanceSettings;
		GoogleCategoryMapping _googleCategoryMapping;
		EbayCategoryMapping _ebayCategoryMapping;
		ImageLookup.StoreFrontImagesLookupUtility _imageLookupUtility;
		configuration.ShoppingConnectorConfiguration _shoppingConnectorConfiguration;
		private Store1 _store;
		private string _storeUrl;
		private List<GenericFeedRecord> _rawData;
		IdentifiersFilters _identifiersFilters;
		private readonly WebStoreDeploymentType _deploymentType;
		//Create an initialize an instance of google category mapping

		public RawFeedBuilder(Store1 store ,ApplicationSettings applicationSettings, ApplicationInstanceSettings applicationInstanceSettings, IdentifiersFilters identifiersFilters)
		{

			_applicationSettings = applicationSettings;
			_applicationInstanceSettings = applicationInstanceSettings;
			_shoppingConnectorConfiguration = ShoppingConnectorConfiguration.GetConfiguration();
			_store = store;
			_identifiersFilters = identifiersFilters;
			_deploymentType = ShoppingConnectorUtills.DeploymentTypeFromString(applicationInstanceSettings.DeploymentType);
			//Determining the store url
			switch (_applicationInstanceSettings.DeploymentType.ToLower())
			{
				case "production":
					_storeUrl = _store.ProductionURI;
					break;
				case "development":
					_storeUrl = _store.DevelopmentURI;
					break; ;
				case "staging":
					_storeUrl = _store.StagingURI;
					break;
			default:
					_storeUrl = "";
					break;
			}

		}

		public string StoreUrl
		{
			get => _storeUrl;
		}

		public GoogleCategoryMapping GoogleCategoryMapping
		{
			get => _googleCategoryMapping;
		}

		public EbayCategoryMapping EbayCategoryMapping
		{
			get => _ebayCategoryMapping;
		}


		public StoreFrontImagesLookupUtility ImagesLookupUtility
		{
			get => _imageLookupUtility;
		}


		/// <summary>
		/// The main method used to create a raw feed 
		/// </summary>
		/// <param name="store"></param>
		/// <param name="products"></param>
		/// <param name="googleCategoryMapping"></param>
		/// <param name="defaultVariantOnly"></param>
		public List<GenericFeedRecord> BuildFeed(Store1 store, IEnumerable<Product> products, bool defaultVariantOnly)
		{
			

			_rawData = new List<GenericFeedRecord>();
			try
			{

				IEnumerable<int> categoryFilter = new List<int> { };
				if (_identifiersFilters != null)
				{
					categoryFilter = _identifiersFilters.GetFilter("Categories");
				}
				//_identifiersFilters


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
					foreach (var variant in p.ProductVariants)
					{
						if (categoryFilter != null && categoryFilter.Count() > 0)
						{
							var categoryFilteredCount = p.ProductCategories.Count(x => categoryFilter.Contains(x.CategoryID));
							if (categoryFilteredCount == 0)
								continue;
						}
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

						
						
						sizeOptions.Where(x=>x.Name=="1000").ToList().ForEach(size =>
						{
							if ((size.Name??"") != "1000")
							{
								//next;
							}
							else

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
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Id, productId);
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.ColorOptionCount, colorOptionCount.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SizeOptionCount, colorOptionCount.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Size, size.Name);
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SizePriority, size.Priority.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.ColorPriority, color.Priority.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Title, p.Name ?? "");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SETitle, p.SETitle ?? "");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Description, p.Description ?? "");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SEDescription, p.SEDescription ?? "");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.VariantDescription, variant.Description ?? "");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.VariantSEDescription, variant.SEDescription ?? "");
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
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.AgeGroup, "");
										rawFeedRecord.FeedRecord.Add( RawFeedFieldNames.ManufaturingPartNumber, variant.ManufacturerPartNumber ?? "");
										rawFeedRecord.FeedRecord.Add( RawFeedFieldNames.ItemGroupId, $"{p.ProductID}-{variant.VariantID}");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Weight, variant.Weight.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Link, GetProductLink(p, _store));  //todo: test method
										var additionalImage = string.Empty;
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.ImageLink, GetProductImage(p.ProductID, store, out additionalImage));//ToDo: Test Method
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.AdditionalImageLink, additionalImage);
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Condition, GetContition(variant)); //TODO: Improve method
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Availability, GetAvailability(variant)); //TODO: improve method -currently hard-coded value
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.AvailabilityDate, GetAvailabilityDate(p.ProductID));
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Inventory, variant.Inventory.ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SalePriceWithCurrency, GetSalePriceWithCurrency(variant)); //TODO: Hardcoded currency 
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SalePrice, GetSalePrice(variant)); //TODO: Hardcoded currency 
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.SalePriceEffectiveDate, GetSalePriceEffecctiveDate(variant).ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.PriceWithCurrency, (variant.Price + size.AddedPrice + color.AddedPrice).ToString() + " USD");
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Price, (variant.Price + size.AddedPrice + color.AddedPrice).ToString());
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Gtin, GetGtin(variant)); // TODO: Test data retrieval from the database
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Brand, GetBrand(store));
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.MPN, GetMpn(productId, variant));
										if(_applicationInstanceSettings.HasGoogleFeed)
											rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Category, GetGoogleProductCategory(p));
										if (_applicationInstanceSettings.HasEbayFeed)
											rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.EbayCategoryId, GetEbayProductCategory(p).ToString());

										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.ProductType, GetProductType(p)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingCountry", GetProductType(p.ProductID)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingService", GetProductType(p.ProductID)); //todo: implement method
																														//todo: rawFeedRecord.FeedRecord.Add("ShippingPrice", GetProductType(p.ProductID)); //todo: implement method
										rawFeedRecord.FeedRecord.Add(RawFeedFieldNames.Color, color.Name);



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





			return _rawData;
		}

		/// <summary>
		/// It loads the category classes passed on the type or merchant feed requested in the instance configuration 
		/// So far we are impementing mapping for Google and Ebay categories
		/// </summary>
		public void LoadCategories()
		{

			

			if (_applicationInstanceSettings.HasGoogleFeed)
			{
				LoadStoreFront_GoogleCategories();
			}
			if (_applicationInstanceSettings.HasEbayFeed)
			{
				LoadStorefront_EbayCategories();
			}


		}

		private  void LoadStoreFront_GoogleCategories()
		{

			var appDataFolder = _applicationSettings.Folders.InputFolder; 
			var googleCategoriesFileName = Path.Combine(appDataFolder, _applicationSettings.Files.GoogleCategoriesFile);
			//var storeFrontGoogleCategoryMappingFileName = Path.Combine(appDataFolder, _applicationInstanceSettings.GoogleCategoryMappingFileName);
			//var googleCategoryMappingFileName = Path.Combine(appDataFolder,_applicationInstanceSettings.GoogleCategoryMappingFileName);
			_googleCategoryMapping = new GoogleCategoryMapping(googleCategoriesFileName, _applicationSettings, _applicationInstanceSettings);
			_googleCategoryMapping.Initialize();

		}

		private void LoadStorefront_EbayCategories()
		{
			var appDataFolder = _applicationSettings.Folders.InputFolder;
			var ebayCategoriesFileName = Path.Combine(appDataFolder, _applicationSettings.Files.EbayCategoriesFile);
			_ebayCategoryMapping = new EbayCategoryMapping(ebayCategoriesFileName, _applicationSettings, _applicationInstanceSettings);
			_ebayCategoryMapping.Initialize();

		}



		/// <summary>
		/// Return the path of google category mapped to the first mapped product category 
		/// or the default Google category.
		/// Note hat a product may have 0, 1 or many categories 
		/// Current logic returns the first one mapped 
		/// 		/// This function is mapping the goolgle product categories 
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
		/// <param name="product"></param>
		/// <returns></returns>
		public string GetGoogleProductCategory(Product product)
		{
			//TODO: Implement more complex logic for selecting the mapped category
			var result = _googleCategoryMapping.DefaultCategory;

			if (product == null) return result;

			if(product.ProductCategories == null) return result;

			if (product.ProductCategories.Count == 0) return result;

			foreach (var cat in product.ProductCategories)
			{
				var catPath = _googleCategoryMapping.GetGooleCategory(cat.CategoryID);
				if (catPath != _googleCategoryMapping.DefaultCategory)
					return catPath;
			}
			return result;
		}

		public int GetEbayProductCategory(Product product)
		{
			//TODO: Implement more complex logic for selecting the mapped category
			var result = _ebayCategoryMapping.DefaultEbayCategoryId;

			//todo: Logging;
			if (product == null) return result;

			//todo: Logging
			if (product.ProductCategories == null) return result;

			//todo: Logging
			if (product.ProductCategories.Count == 0) return result;
			//For each cattegory that the product is assigned to,
			//we return the first id that is matched and is not the same with the default category
			foreach (var cat in product.ProductCategories)
			{
				var mappedCatId = _ebayCategoryMapping.GetMappedEbayCategory(cat.CategoryID);
				if (mappedCatId != _ebayCategoryMapping.DefaultEbayCategoryId)
					return mappedCatId;
			}
			return result;
		}


		public string GetProductType(Product p)
		{
			var result = string.Empty; //TODO = Nice to Have; A default product type 
			if (p == null) return result;
			if (p.ProductType == null) return result;
			result = p.ProductType.Name ?? result;
			return result;
		}

		/// <summary>
		/// The link is composed of :
		///		Store url
		///		letter "p"
		///		product id 
		///		SEName field of the product table
		/// </summary>
		/// <param name="product"></param>
		/// <param name="store"></param>
		/// <returns></returns>
		public string GetProductLink(Product product, Store1 store)
		{
			const string productMark = "p";
			const string ending = ".aspx";
			string result = string.Empty;
			if (product == null)
				throw new Exception("GetProductLink() function received a null product");
			if (store == null)
				throw new Exception("GetProductLink() function received a null store");

			string productid = product.ProductID.ToString();
			string seName = product.SEName ?? string.Empty;

			result = $"{store.ProductionURI}/{productMark}-{productid}-{seName}{ending}";
			return result;
		}

		/// <summary>
		/// This method builds the Image url based on product id and store url
		/// This may vary from one implementation of the stre to another
		/// Therefore may be suitable in the future to adda a plugin function that we could replace 
		/// based on the store implementation
		/// Sample image url: https://www.wickedteesofny.com/images/product/large/5413_1_.jpg
		/// {stoteUrl}/images/product/{large|medium|small}/{productid}_{picture_number}_.jpg
		/// TODO:
		/// On this version developed on July 2020 we do not know the rules of how the images url are build on the server
		/// We will need to come up with a way to store the image information in the database so we can return the accurate url
		/// as this method has chances to be incuarate
		/// </summary>
		/// <param name="productID"></param>
		/// <param name="storeURI"></param>
		/// <returns></returns>
		public string GetProductImage(int productID, Store1 store,  out string additionalImage)
		{
			if (store == null)
				throw new Exception("GetProductLink() function received a null store");

			if (_imageLookupUtility == null)
				throw new Exception("RawFeedBuilder::ImageLookupUtility is not loaded. Please make sure that you call first RawFeedBuilder.LoadImageLookup()");

			var result = string.Empty;
			var storeURI = store.ProductionURI;
			storeURI = storeURI ?? string.Empty;

			var additionalImageRelativePath = string.Empty;
			var imageRelativePath = _imageLookupUtility.GetImagePath(productID, out additionalImageRelativePath);
			imageRelativePath = imageRelativePath ?? string.Empty;
			additionalImageRelativePath = additionalImageRelativePath ?? string.Empty;

			//todo: Review adding the https artificially ASAP
			var https = storeURI.StartsWith("https://") ? string.Empty : "https://";

			var url = $"{https}{storeURI}/{imageRelativePath}";
			additionalImage = $"{https}{storeURI}/{additionalImageRelativePath}";

			result = url.ToString();

			return result;
		}

		/// <summary>
		/// Build the price for a product variant, including the currency code
		/// </summary>
		/// <param name="variant"></param>
		/// <returns></returns>
		public string GetVariantPrice(ProductVariant variant )
		{
			//var variantPrice= 

			return null;
		}

		public string GetSalePrice(ProductVariant variant)
		{
			var result = string.Empty;
			var defaultCurrency = _shoppingConnectorConfiguration.GetValue("");
			if (variant == null) return result;
			var salePrice = variant.SalePrice.HasValue ? variant.SalePrice.Value : variant.Price;
			if (salePrice <= 0) salePrice = variant.Price;
			result = salePrice.ToString();
			return result;
		}

		public string GetSalePriceWithCurrency(ProductVariant variant )
		{
			var result = string.Empty;
			var defaultCurrency = _shoppingConnectorConfiguration.GetValue("");
			var salePrice  =GetSalePrice(variant);
			result = $"{salePrice} {defaultCurrency}";
			return result;
		}

		/// <summary>
		/// Your product’s Global Trade Item Number (GTIN)
		/// Exclude dashes and spaces
		///Submit only valid GTINs as defined in the official GS1 validation guide, which includes these requirements:
		/// </summary>
		/// <param name="variant"></param>
		/// <returns></returns>
		public string GetGtin(ProductVariant variant)
		{
			return variant.GTIN ?? string.Empty; //TODO: Test if it gets a value 
		}

		/// <summary>
		/// Returns Manufacturing Part Number
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="variant"></param>
		/// <returns></returns>
		public string GetMpn(string productId, ProductVariant variant)
		{
			var result = string.Empty;
			if (variant == null)
				return result;
			if (!string.IsNullOrEmpty(variant.ManufacturerPartNumber))
			{
				result = variant.ManufacturerPartNumber;
			}
			else if (!string.IsNullOrEmpty(variant.Product.ManufacturerPartNumber))
			{
				result = variant.Product.ManufacturerPartNumber.Trim();
			}
			else
			{
				result = $"mfg_{productId}";
			}

			return result;
		}

		/// <summary>
		/// Loads in memory the list of all the images available to the application 
		/// The list is a flat file where each line is the file name of an image file with the relative path to the images folder of the application.
		/// </summary>
		/// <param name="imagesFileName"></param>
		/// <returns></returns>
		public bool LoadImagesLookup()
		{
			var result = false;
			var inputPath = _shoppingConnectorConfiguration.GetApplicationSettings().Folders.InputFolder;
			var imagesListFileName = Path.Combine(inputPath, _applicationInstanceSettings.ImagesListFileName);
			try
			{
				_imageLookupUtility = new StoreFrontImagesLookupUtility(_applicationInstanceSettings.ImagesListFileName);
				_imageLookupUtility.LoadImagesFromFile(imagesListFileName);
			}
			catch (Exception)
			{
				//todo: write error to the log
				result = false;
			}


			return result;
		}

		public string GetStoreURI(Store1 store, WebStoreDeploymentType deploymentType)
		{
			string result = string.Empty;
			var uriBuilder = new UriBuilder();
			(uriBuilder.Scheme,
					uriBuilder.Host,
					uriBuilder.Path,
					uriBuilder.Port) = GetStoreURIParts(deploymentType, store);
			//uriBuilder.
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
			scheme = string.IsNullOrEmpty(scheme) ? "https:" : scheme ;
			host = (host ?? string.Empty).Trim();
			return new Tuple<string, string, string, int>(scheme, host, path, port);
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

		public string GenerateProductId(int productId, int variantId, string sizeSkuModifier, string colorSkuModifier)
		{
			string result = string.Empty;
			sizeSkuModifier = string.IsNullOrEmpty(sizeSkuModifier) ? "NS" : sizeSkuModifier;
			colorSkuModifier = string.IsNullOrEmpty(colorSkuModifier) ? "NC" : colorSkuModifier;
			result = $"{productId}_{variantId}_{sizeSkuModifier}_{colorSkuModifier}";

			return result;
		}


		private string GetContition(ProductVariant variant)
		{
			//todo: Get the value from the database
			return "new";
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


	}
}
