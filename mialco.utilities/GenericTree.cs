using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.utilities
{
	/// <summary>
	/// Generic Tree to bu used to store taxonomies
	/// Inspired from: https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericTree<T>
	{
		private T _data;
		private LinkedList<GenericTree<T>> _children;

		public GenericTree(T data)
		{
			_data = data;
			_children = new LinkedList<GenericTree<T>>();
		}

		public void Add(GenericTree<T> child)
		{
			_children.AddLast(child);
		}

		public GenericTree<T> GetChild(int index)
		{
			GenericTree<T> result = null;
			if (index < 0 || index >= _children.Count) ;
			foreach (var gt in _children)
			{
				if (index == 0)
				{
					result = gt;
					break;
				}
				index--;
			}
			return result;
		}
	}
}
