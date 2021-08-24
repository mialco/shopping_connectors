using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.Entities
{
	public abstract class EntityBase
	{
		public  int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Created { get; set; }
		public DateTime ModifiedTime { get; set; }
		public string ModifiedBy { get; set; }

	}
}
