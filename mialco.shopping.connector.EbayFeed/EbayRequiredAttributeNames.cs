using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace mialco.shopping.connector.EbayFeed
{
	static class EbayRequiredAttributeNames
	{
		static List<string> _attributeList;
		static ReadOnlyCollection<string> _readOnlyAttributeList;

		public const string Brand = "Brand";
		public const string Model = "Model";
		public const string PowerSource = "Power Source";
		public const string Type = "Type";
		public const string Color = "Color";
		public const string Power = "Power";
		public const string EnergyStar = "Energy Star";
		public const string ManufacturerWarranty = "Manufacturer Warranty";
		public const string MPN = "MPN";
		public const string BundleDescription = "Bundle Description";
		public const string CustomBundle = "Custom Bundle";
		public const string VoltageCountry_RegionOfManufacture = "Voltage Country/Region of Manufacture";
		public const string Material = "Material";
		public const string ManufacturerColor = "Manufacturer Color";
		public const string CaliforniaProp65Warning = "California Prop 65 Warning";
		public const string ECRange = "EC Range";

		static EbayRequiredAttributeNames()
		{
			_attributeList = new List<string> { 
			Brand,Model,PowerSource,Type,Color,Power,EnergyStar,ManufacturerWarranty,
				MPN,BundleDescription,CustomBundle,VoltageCountry_RegionOfManufacture, 
				Material, ManufacturerColor, CaliforniaProp65Warning, ECRange
			};
			_readOnlyAttributeList = new ReadOnlyCollection<string>(_attributeList);
		}
		public static ReadOnlyCollection<string> AttributeList => _readOnlyAttributeList;
	}
}
