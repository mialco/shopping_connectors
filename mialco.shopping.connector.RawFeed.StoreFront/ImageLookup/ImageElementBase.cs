using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{

	/// <summary>
	/// 
	/// </summary>
	abstract class ImageElementBase
	{
		protected int _highestPriority;
		private int _priority;
		protected string _name;
		protected bool _isHighestPriority;

		public ImageElementBase(string name,int priority , int highestPriority)
		{
			_name = name ?? string.Empty;
			_priority = priority;
			_highestPriority = highestPriority;
			_isHighestPriority = _priority <= _highestPriority;
		}

		public int HighestPriority { get => _highestPriority;}
		public string Name { get => _name; }
		public bool IsHighestPriority { get => _isHighestPriority; }
		public int Priority { get => _priority; }
	}
}
