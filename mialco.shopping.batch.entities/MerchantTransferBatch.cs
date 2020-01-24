using mialco.shopping.entities.abstraction;
using mialco.shopping.objectvalues;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.batch.entities
{
	public class MerchantTransferBatch : Entity
	{
		public DateTime Created { get; set; }
		public ProductSelectionCriteria SelectionCriteria { get; set; }
    }
}
