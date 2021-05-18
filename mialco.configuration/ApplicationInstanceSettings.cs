using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.configuration
{
	public class ApplicationInstanceSettings
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string ConnecttionString { get; set; }
		public string DeploymentType { get; set; }
		public string ImagesListFileName { get; set; }
		public string DefaultGoogleCategory { get; set; }
		public string DefaultCurrency { get; set; }
		public string GoogleCategoryMappingFileName { get; set; }
		public string GoogleFeedTitle { get; set; }
		public string GoogleFeedDescription { get; set; }
		/// <summary>
		/// The Ebay feed type could be Ebay (CSV or XML) and Amazon (CSV)
		/// </summary>
		public string EbayFeedType { get; set; }
		public string EbayOutputType { get; set; }
		/// <summary>
		/// Default Shipping policy name established on the ebay platform
		/// </summary>
		public string EbayShippingPolicy { get; set; }

		/// <summary>
		/// Default Payment policy name established on the ebay platform
		/// </summary>
		public string EbayPaymentPolicy { get; set; }
		/// <summary>
		/// Default Return policy name established on the ebay platform
		/// </summary>
		public string EbayReturnPolicy { get; set; }

		public string EbayLocale { get; set; }

		public IEnumerable<string> OutputTo { get; set; }
	}
}
