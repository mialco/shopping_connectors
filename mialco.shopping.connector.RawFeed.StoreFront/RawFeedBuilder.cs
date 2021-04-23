using System;
using System.Text;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;
using mialco.shopping.connector.RawFeed.StoreFront.ImageLookup;
using mialco.configuration;

using System.IO;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	/// <summary>
	/// Class used to build a raw feed for StoreFront application
	/// </summary>
	public class RawFeedBuilder
	{
		const int ApplicationInstanceId = 0;
		private ApplicationInstanceSettings _applicationInstanceSettings;
		GoogleCategoryMapping _googleCategoryMapping;
		ImageLookup.StoreFrontImagesLookupUtility _imageLookupUtility;
		configuration.ShoppingConnectorConfiguration _shoppingConnectorConfiguration;

		//Create an initialize an instance of google category mapping

		public RawFeedBuilder(ApplicationInstanceSettings applicationInstanceSettings)
		{
			_applicationInstanceSettings = applicationInstanceSettings;
			_shoppingConnectorConfiguration = ShoppingConnectorConfiguration.GetConfiguration();
		}



		public GoogleCategoryMapping GoogleCategoryMapping
		{
			get => _googleCategoryMapping;
		}

		public StoreFrontImagesLookupUtility ImagesLookupUtility
		{
			get => _imageLookupUtility;
		}

		public void LoadCategories()
		{
			var googleCategoryMappingFileName = Path.Combine(_shoppingConnectorConfiguration.GetApplicationSettings().Folders.InputFolder,
				_applicationInstanceSettings.GoogleCategoryMappingFileName);
			var defaultCategory = _applicationInstanceSettings.DefaultGoogleCategory;
			_googleCategoryMapping = new GoogleCategoryMapping(_applicationInstanceSettings.ConnecttionString,googleCategoryMappingFileName,defaultCategory);
			_googleCategoryMapping.Initialize();
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

			var url = $"{storeURI}/{imageRelativePath}";
			additionalImage = $"{storeURI}/{additionalImageRelativePath}";

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

		public string GetSalePrice(ProductVariant variant )
		{
			var result = string.Empty;
			var defaultCurrency = _shoppingConnectorConfiguration.GetValue("");
			if (variant == null) return result;
			var salePrice = variant.SalePrice.HasValue ? variant.SalePrice.Value : variant.Price;
			if (salePrice <= 0) salePrice = variant.Price;
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


	}
}
