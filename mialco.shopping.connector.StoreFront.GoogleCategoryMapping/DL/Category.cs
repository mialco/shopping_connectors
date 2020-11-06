using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping.DL
{
	public class Category
	{

		public int CategoryID { get; set; }
		public string Name { get; set; }
		public string SEName { get; set; }
		public string Description { get; set; }
		public string Summary { get; set; }
		public int ParentCategoryID { get; set; }
		public byte Published { get; set; }
		public byte Deleted { get; set; }
	}
}
