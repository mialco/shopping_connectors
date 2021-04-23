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
	}
}
