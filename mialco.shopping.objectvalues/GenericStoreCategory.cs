using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.objectvalues
{
	//TODO: this belongs to a a project where it is used not the generic object values
	/// <summary>
	/// A Class representing a store category. To be used with the Generic Tree
	/// </summary>
	public class GenericStoreCategory : MialcoValueObjectBase, ITreeNode
	{

		//public GenericStoreCategory()
		//{
		//}
		protected int _id;
		protected string _name;
		protected int _parentId;

		public GenericStoreCategory(int id, string name, int parentId)
		{
			_id = id;
			_name = name ?? string.Empty;
			_parentId = parentId;
		}

		public int Id => _id;

		public string Name => _name;

		public String FullCategoryPath { get; set; }

		public override bool Equals(object obj)
		{
			var result =false;
			if (obj == null) return false;
			
			try
			{
				var objAsCategory = obj as GenericStoreCategory;
				if (objAsCategory.Id == _id && objAsCategory.Name == _name) result = true;
			}
			catch (Exception)
			{
				return false;
				
			}

			return result;
		}

		public override int GetHashCode()
		{
			return (_name + _id.ToString()).GetHashCode();
		}

		public int GetId()
		{
			return _id;
		}

		public int GetParent()
		{
			return _parentId;
		}

		public override string ToString()
		{
			return _name;
		}

		
	}
}
