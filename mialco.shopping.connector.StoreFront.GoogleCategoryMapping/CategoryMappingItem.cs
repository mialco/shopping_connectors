using System;

namespace mialco.shopping.connector.StoreFront.GoogleCategoryMapping
{
	public class CategoryMappingItem
	{
		public CategoryMappingItem(int storeId, int gooleCategoryId, int categoryMappingId, CategoryMappingType mappingType)
		{
			MappingType = mappingType;
			GoogleCategoryId = gooleCategoryId;
			CategoryMappingId = categoryMappingId;
		}

		public int CategoryMappingId { get; set; }
		public int GoogleCategoryId { get; }
		public CategoryMappingType MappingType {get;}
	}
}
