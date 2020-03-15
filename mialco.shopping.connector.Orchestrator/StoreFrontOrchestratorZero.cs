using mialco.abstractions;
using mialco.shopping.connector.RawFeed;
using mialco.shopping.connector.StoreFront;
using System;
using System.Collections.Generic;
using System.Text;
using mialco.shopping.connector.shared;
using System.Text.RegularExpressions;

namespace mialco.shopping.connector.Orchestrator
{
	public class StoreFrontOrchestratorZero : IRunable
	{
		private IEnumerable<Product> _products;
		private readonly int _storeId;
		private readonly WebStoreDeploymentType _deploymentType;
		private readonly List<GenericFeedRecord> _rawData;


		public StoreFrontOrchestratorZero(int storeId, WebStoreDeploymentType deploymentType)
		{
			_storeId = storeId;
			_deploymentType = deploymentType;
			_rawData = new List<GenericFeedRecord>();
		}


		//Extracts data from the storefront database
		private void ExtractData(int storeId)
		{
			StoreFrontStoreRepositoryEF sfsr = new StoreFrontStoreRepositoryEF();
			var store = sfsr.GetById(storeId);
			var prodrep = new StoreFrontProductRepositoryEF();
			_products = prodrep.GetAll(storeId);
		}

		//Creates the collection of Raw data
		private void BuildFeed(Store1 store, List<Product> products)
		{
			try
			{
				foreach (var p in products)
				{
					var sizes = GetProductSizes(p);
					var colors = GetProductColors(p);
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

					//We build products for each variant, color and size 
					foreach (var variant in p.ProductVariants)
					{
						variantNumber++;
						//id = GenerateProductId

						var sizeOptions = GetProductAttributes(variant.Sizes,variant.SizeSKUModifiers,typeof(SizeOption) );
						var colorOptions = GetProductAttributes(variant.Colors, variant.ColorSKUModifiers, typeof(ColorOption));

						var productImage = GetProductImage(p.ProductID, storeURI);
						var optionImages = GetOptionImages(colors, p.ProductID, storeURI, colorOptions);
						var colorOptionCount = 0;
						var sizeOptionCount = 0;
						//Size options 
						if (sizeOptions == null || sizeOptions.Count == 0)
							sizeOptions = new List<ProductAttribute> { new SizeOption { Name = String.Empty, AddedPrice = 0, SkuModifier = string.Empty } };
						if (colorOptions == null || colorOptions.Count == 0)
							colorOptions = new List<ProductAttribute> { new ColorOption { Name = string.Empty, AddedPrice = 0, SkuModifier = string.Empty } };
						sizeOptions.ForEach(size => {
							sizeOptionCount++;
							colorOptions.ForEach(color => {
								colorOptionCount++;
								// Create New Raw Product for each variant, size and color
								var rawFeedRecord = new GenericFeedRecord();
								id++; //The Id we are just incrementing the previous id
								var productId = GenerateProductId(p.ProductID, variant.VariantID, size.SkuModifier, color.SkuModifier);
								rawFeedRecord.ProductId = productId;
								rawFeedRecord.FeedRecord.Add( "ColorOptionCount",colorOptionCount.ToString());
								rawFeedRecord.FeedRecord.Add("SizeOptionCount", colorOptionCount.ToString());
								rawFeedRecord.FeedRecord.Add("Price", (variant.Price + size.AddedPrice + color.AddedPrice).ToString());
								rawFeedRecord.FeedRecord.Add("Size", size.Name);
								rawFeedRecord.FeedRecord.Add("Color", color.Name);
								rawFeedRecord.FeedRecord.Add("SizePriority", size.Priority.ToString());
								rawFeedRecord.FeedRecord.Add("ColorPriority", color.Priority.ToString());
								rawFeedRecord.FeedRecord.Add("Title", p.Name??"");
								rawFeedRecord.FeedRecord.Add("Description", p.Description ?? "");
								// The category from the store Front will be mapped to the
								// Google field product_type
								// The Value will be created by listing the entire path of a category with its 
								// parent categories delimited by ">" 
								// for Exampe
								// Parent Category > Category
								// Funny Crazy T-shirts > Fitness Gym T-shirts
								rawFeedRecord.FeedRecord.Add("Category", "");
								rawFeedRecord.FeedRecord.Add("Price", (variant.Price + size.AddedPrice + color.AddedPrice).ToString());
								rawFeedRecord.FeedRecord.Add("SalePrice", variant.SalePrice.ToString());
								//rawFeedRecord.FeedRecord.Add("SalePriceEffectiveDate",)
								rawFeedRecord.FeedRecord.Add("color", color.Name);
								rawFeedRecord.FeedRecord.Add("Size", size.Name);
								rawFeedRecord.FeedRecord.Add("AgeGroup", "");
								rawFeedRecord.FeedRecord.Add("ManufaturingPartNumber", variant.ManufacturerPartNumber ?? "");
								rawFeedRecord.FeedRecord.Add("ItemGroupId", $"{p.ProductID}-{variant.VariantID}");
								rawFeedRecord.FeedRecord.Add("Color", color.Name);
								rawFeedRecord.FeedRecord.Add("Weight", variant.Weight.ToString());
							}); });

					}
				}
			}
			catch (Exception)
			{

				throw;
			}

		}

		private object GetOptionImages(List<Tuple<string, decimal, string>> colors, int productID, object storeURI, object colorOptions)
		{
			throw new NotImplementedException();
		}

		private object GetProductImage(int productID, object storeURI)
		{
			throw new NotImplementedException();
		}

		private List<ColorOption> GetColorOptions(Product p)
		{
			throw new NotImplementedException();
		}

		public List<ProductAttribute> GetProductAttributes(string attributeOptions, 
			string skuModifiers, Type attributeType)
		{
			ProductAttributeAbstractFactory fact;
			if (attributeType == typeof (ColorOption))
				fact = new ColorOptionsFactory();
			if (attributeType == typeof(SizeOption))
				fact = new SizeOptionsFactory();


			var result = new List<ProductAttribute>();
			var addedprices = new List<string>();
			var modifiersList = new List<string>();
			var maxAttrCount = 0;

			//Split The attributes string 
			if (!string.IsNullOrEmpty(attributeOptions))
			{
				var attrNames =attributeOptions.Trim().Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
				maxAttrCount = attrNames.Length;

			}
			if (!string.IsNullOrEmpty(skuModifiers))
			{
				var skuModifiersCollection = skuModifiers.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				maxAttrCount = maxAttrCount > skuModifiersCollection.Length ? maxAttrCount : skuModifiersCollection.Length;
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
			string name=string.Empty;
			decimal price = 0.0m;
			attributeString = attributeString ??"";

			var rx = new Regex(@"\s*(?<attr>.*)\s*\[\s*(?<price>[[0-9]*\.{0,1}[0-9]*)\s*\]");
			Match match =  rx.Match(attributeString);
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

		private List<Tuple<string, decimal>> GetProductSizes(Product p)
		{
			throw new NotImplementedException();
		}

		private object GenerateId(Product p)
		{
			int result = 0;

			return result;
		}

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

		public int Run()
		{
			ExtractData(33);
			ExportRawData();
			return 0;
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
				var obj1 =  obj as ProductAttribute;
				return (obj1.Name == this.Name
					&& obj1.Priority == this.Priority
					&& obj1.SkuModifier == this.SkuModifier
					&& obj1.AddedPrice == this.AddedPrice
					);
			}

			public override int GetHashCode()
			{
				const int p  = 29;
				int hash = p;
				hash = hash * p + AddedPrice.GetHashCode();
				hash = hash * p + (Name ?? "").GetHashCode();
				hash = hash * p + (SkuModifier ?? "").GetHashCode();
				hash = hash * p + Priority.GetHashCode();
				return hash; 
			}
		}

		public class SizeOption:ProductAttribute
		{
		}

		public class ColorOption: ProductAttribute
		{
		}

	}
}
