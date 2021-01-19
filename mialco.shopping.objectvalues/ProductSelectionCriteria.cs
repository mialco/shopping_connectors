using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.objectvalues
{
	//TODO: this belongs to a a project where it is used not the generic object values
    public class ProductSelectionCriteria: MialcoValueObject
    {
		bool ? IsActive { get; set; }
		bool ? IsPublished { get; set; }
		bool? SelectFamilyRootOnly { get; set; }
		string NameIs { get; set; }
		string NameContains { get; set; }

		public override string ToString()
		{
			var result = new StringBuilder();
			string strval = IsActive == null ? "False" : IsActive.HasValue ? IsActive.Value.ToString() : "False";
			result.Append($"Is Active : {strval}");
			//todo

			return result.ToString();
		}
	}
}
