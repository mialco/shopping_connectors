using System;
using System.Text;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.StoreFront;
using mialco.shopping.connector.StoreFront.GoogleCategoryMapping;

namespace mialco.shopping.connector.RawFeed.StoreFront
{
	/// <summary>
	/// Class used to build a raw feed for StoreFront application
	/// </summary>
	public class RawFeedBuilder
	{
		GoogleCategoryMapping _googleCategoryMapping;

		//Create an initialize an instance of google category mapping

		public RawFeedBuilder()
		{

		}

		public GoogleCategoryMapping GoogleCategoryMapping
		{
			get => _googleCategoryMapping;
		}

		public void LoadCategories()
		{
			_googleCategoryMapping= new GoogleCategoryMapping();
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
		public string GetProductImage(int productID, Store1 store)
		{
			if (store == null)
				throw new Exception("GetProductLink() function received a null store");

			var result = string.Empty;
			var storeURI = store.ProductionURI;
			storeURI = storeURI ?? string.Empty;
			var url = new StringBuilder(storeURI);
			if (!storeURI.EndsWith("/")) url.Append("/");
			url.Append("images/product/");
			url.Append("large/");
			url.Append(productID);
			//url.Append("_1_.jpg");
			url.Append(".jpg");
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


	}
}
