using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	public class ProductAttributesEntity: Entity
	{
		int ProductID { get; set; }
		string Summary { get; set; }
		string SEKeywords { get; set; }
			
	}
}
