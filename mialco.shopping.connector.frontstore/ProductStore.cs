﻿using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.frontstore
{
	public class ProductStore //: Entity
	{
		public int ProductID { get; set; }
		public Product Product {get;set;}
	 	public int StoreID { get; set; }
		public Store Store { get; set; }
	}
}
