using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.shared
{
	public class CategoryMappingItem
	{
		public CategoryMappingItem(int gooleCategoryId, int categoryMappingId, CategoryMappingType mappingType)
		{
			MappingType = mappingType;
			MappedCategoryId = gooleCategoryId;
			SourceCategoryId = categoryMappingId;
		}

		public int SourceCategoryId { get; set; }
		public int MappedCategoryId { get; }
		public CategoryMappingType MappingType { get; }
	}
}
