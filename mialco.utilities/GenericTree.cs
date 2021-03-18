using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.utilities
{
	/// <summary>
	/// Generic Tree to bu used to store taxonomies
	/// Inspired from: https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
	/// </summary>
	/// <typeparam name="T">The type of the tree item object</typeparam>
	public class GenericTree<T> where T : MialcoValueObjectBase, ITreeNode
	{
		private T _data;
		private LinkedList<GenericTree<T>> _children;

		public GenericTree(T data)
		{
			_data = data;
			_children = new LinkedList<GenericTree<T>>();
		}

		/// <summary>
		/// Add the tree node GenericTree to its parent U
		/// </summary>
		/// <param name="child"></param>
		public GenericTree<T> Add(GenericTree<T> child)
		{
			if (child == null) return null ;
			if (child.Data.GetParent() == _data.GetId())
			{
				_children.AddLast(child);
				return this;
			}
			else
			{
				GenericTree<T> result = null;
				foreach (var item in _children)
				{
					result = item.Add(child);
					if (result != null)
						return result;
				}
					_children.AddLast(child);
				return this;
			}			
		}

		public T Data { get => _data; }

		public GenericTree<T> GetChild(int index)
		{
			GenericTree<T> result = null;
#pragma warning disable CS0642 // Possible mistaken empty statement
			if (index < 0 || index >= _children.Count) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
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

		public void Traverse(GenericTree<T> root, GenericTree<T> node, Func<string> action)
		{


		}

		/// <summary>
		/// It adds a node to the tree and returns the path to the node in a recurrent fashion
		/// </summary>
		/// <param name="root"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		public string AddWithPath(GenericTree<T>root,  GenericTree<T> node, int depth)
		{
			string result = null;
			if (root == null || node == null) return null;


			if (root.Data.GetId() == node.Data.GetParent())
			{
				root._children.AddLast(node);
				result = string.IsNullOrEmpty(root._data.ToString()) == true  ? node.Data.ToString()  : $"{root._data.ToString()} > {node.Data}";
				return result;
			}

			//if (index < 0 || index >= _children.Count) ;
			foreach (var gt in root._children)
			{
				result = AddWithPath(gt, node, depth + 1);
				if (result != null)
				{
					result = string.IsNullOrEmpty(root._data.ToString()) == true ? result : $"{root._data.ToString()} > {result}";
					return result;
				}
			}

			//If we did not find a parent for the current node then result will be null
			// If we are at level 0 we add the note to the root and return the name of the node from ToString() 
			// If the result is not null it means that we aready found its parent and it does not need to be added to the root
			if (result == null)
			{
				if (depth == 0)
				{
					if (result == null)
					{
						root._children.AddLast(node);
						//This is the root of the tree and we add the Node to the root
						result = node._data.ToString();
					}
					else
					{
						return result;
					}
				}
			}
			else
			{
				// If the result is not null it means that we aready found its parent and we are adding its name 
				// To the current nore but only if the depth is > 0
				if (depth > 0)
				{
					result = string.IsNullOrEmpty(root._data.ToString()) == true ? result : $"{root._data.ToString()} > {result}";
				}
			}
			return result;
		}
	}

}
